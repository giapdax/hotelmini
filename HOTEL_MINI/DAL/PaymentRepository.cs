using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;

namespace HOTEL_MINI.DAL
{
    public class PaymentRepository
    {
        private readonly string _cs;
        private const byte PREC = 18;
        private const byte SCALE = 2;

        private static readonly HashSet<string> AllowedStatus =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Paid", "Unpaid", "PartiallyPaid" };

        private static readonly HashSet<string> AllowedMethod =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Cash", "Card", "Transfer" };

        public PaymentRepository() { _cs = ConfigHelper.GetConnectionString(); }

        private static void AddDec(SqlCommand cmd, string name, decimal value)
        {
            var p = cmd.Parameters.Add(name, SqlDbType.Decimal);
            p.Precision = PREC; p.Scale = SCALE;
            p.Value = decimal.Round(value, SCALE, MidpointRounding.AwayFromZero);
        }

        private static string NormalizeStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status)) return "Paid";
            if (AllowedStatus.Contains(status)) return status;
            var s = status.Trim().ToLowerInvariant();
            if (s == "completed" || s == "success" || s == "successful") return "Paid";
            if (s == "partial" || s == "partially" || s == "partiallypaid") return "PartiallyPaid";
            if (s == "unpaid" || s == "pending" || s == "awaiting") return "Unpaid";
            return "Paid";
        }

        private static string NormalizeMethod(string method)
        {
            if (string.IsNullOrWhiteSpace(method)) return "Cash";
            if (AllowedMethod.Contains(method)) return method;
            var m = method.Trim().ToLowerInvariant();
            if (m == "visa" || m == "master" || m == "credit" || m == "debit") return "Card";
            if (m == "bank" || m == "wire" || m == "transfer") return "Transfer";
            if (m == "cash" || m == "tiền mặt" || m == "tien mat") return "Cash";
            return "Cash";
        }

        public List<Payment> GetPaymentsByInvoice(int invoiceId)
        {
            var list = new List<Payment>();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("SELECT PaymentID, InvoiceID, Amount, PaymentDate, Method, Status FROM Payments WHERE InvoiceID=@I ORDER BY PaymentDate", conn))
            {
                cmd.Parameters.AddWithValue("@I", invoiceId);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new Payment
                        {
                            PaymentID = rd.GetInt32(0),
                            InvoiceID = rd.GetInt32(1),
                            Amount = rd.GetDecimal(2),
                            PaymentDate = rd.GetDateTime(3),
                            Method = rd.GetString(4),
                            Status = rd.GetString(5)
                        });
                    }
                }
            }
            return list;
        }

        public void AddPayment(Payment p)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("INSERT INTO Payments(InvoiceID, Amount, PaymentDate, Method, Status) VALUES(@I,@A,@D,@M,@S)", conn))
            {
                cmd.Parameters.AddWithValue("@I", p.InvoiceID);
                AddDec(cmd, "@A", p.Amount);
                cmd.Parameters.AddWithValue("@D", p.PaymentDate);
                cmd.Parameters.AddWithValue("@M", p.Method ?? "Cash");
                cmd.Parameters.AddWithValue("@S", p.Status ?? "Paid");
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Payment> GetPaymentsByHeaderId(int headerBookingId)
        {
            var list = new List<Payment>();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("SELECT p.PaymentID, p.InvoiceID, p.Amount, p.PaymentDate, p.Method, p.Status FROM Payments p JOIN Invoices i ON i.InvoiceID = p.InvoiceID WHERE i.BookingID = @bid ORDER BY p.PaymentDate ASC", conn))
            {
                cmd.Parameters.Add("@bid", SqlDbType.Int).Value = headerBookingId;
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new Payment
                        {
                            PaymentID = rd.GetInt32(0),
                            InvoiceID = rd.GetInt32(1),
                            Amount = rd.GetDecimal(2),
                            PaymentDate = rd.GetDateTime(3),
                            Method = rd.IsDBNull(4) ? "" : rd.GetString(4),
                            Status = rd.IsDBNull(5) ? "" : rd.GetString(5)
                        });
                    }
                }
            }
            return list;
        }

        public decimal GetPaidAmountByHeaderId(int headerBookingId)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("SELECT ISNULL(SUM(p.Amount), 0) FROM Payments p JOIN Invoices i ON i.InvoiceID = p.InvoiceID WHERE i.BookingID = @bid AND p.Status = 'Paid'", conn))
            {
                cmd.Parameters.Add("@bid", SqlDbType.Int).Value = headerBookingId;
                conn.Open();
                var o = cmd.ExecuteScalar();
                return o == null || o == DBNull.Value ? 0m : Convert.ToDecimal(o);
            }
        }

        public int AddPaymentSafe(int invoiceId, decimal amount, string method, string status, int issuedByIfPaid, List<int> bookingRoomIdsToCheckout)
        {
            if (amount <= 0) throw new Exception("Số tiền phải > 0.");
            if (bookingRoomIdsToCheckout == null || bookingRoomIdsToCheckout.Count == 0) throw new Exception("Danh sách phòng cần checkout không được rỗng.");

            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    using (var chk = new SqlCommand("SELECT COUNT(1) FROM Payments WHERE InvoiceID=@I", conn, tran))
                    {
                        chk.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                        if (Convert.ToInt32(chk.ExecuteScalar() ?? 0) > 0) throw new Exception("Hóa đơn đã có thanh toán trước. Hệ thống chỉ cho phép thanh toán một lần.");
                    }

                    decimal total;
                    using (var cmd = new SqlCommand("SELECT TotalAmount FROM Invoices WITH (UPDLOCK, ROWLOCK) WHERE InvoiceID=@I", conn, tran))
                    {
                        cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                        var o = cmd.ExecuteScalar();
                        if (o == null || o == DBNull.Value) throw new Exception("Không tìm thấy hóa đơn.");
                        total = Convert.ToDecimal(o);
                    }

                    if (Math.Abs(amount - total) > 0.0001m) throw new Exception("Số tiền phải đúng bằng tổng hóa đơn (chỉ cho phép thanh toán 1 lần).");

                    var normMethod = NormalizeMethod(method);
                    var normStatus = NormalizeStatus(status);

                    int paymentId;
                    using (var ins = new SqlCommand("INSERT INTO Payments(InvoiceID, Amount, PaymentDate, Method, Status) VALUES (@I, @A, GETDATE(), @M, @S); SELECT CAST(SCOPE_IDENTITY() AS INT);", conn, tran))
                    {
                        ins.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                        AddDec(ins, "@A", amount);
                        ins.Parameters.Add("@M", SqlDbType.VarChar, 20).Value = normMethod;
                        ins.Parameters.Add("@S", SqlDbType.VarChar, 20).Value = normStatus;
                        paymentId = Convert.ToInt32(ins.ExecuteScalar());
                    }

                    using (var upd = new SqlCommand("UPDATE Invoices SET Status='Paid', IssuedAt=GETDATE(), IssuedBy=@U WHERE InvoiceID=@I", conn, tran))
                    {
                        upd.Parameters.Add("@U", SqlDbType.Int).Value = issuedByIfPaid;
                        upd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                        upd.ExecuteNonQuery();
                    }

                    foreach (var bookingRoomId in bookingRoomIdsToCheckout)
                    {
                        using (var cmd = new SqlCommand("UPDATE BookingRooms SET Status='CheckedOut', CheckOutDate = ISNULL(CheckOutDate, GETDATE()) WHERE BookingRoomID=@Id AND Status IN ('Booked','CheckedIn')", conn, tran))
                        {
                            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = bookingRoomId;
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd2 = new SqlCommand("DECLARE @RoomID INT=(SELECT RoomID FROM BookingRooms WHERE BookingRoomID=@Id); UPDATE Rooms SET Status = CASE WHEN EXISTS(SELECT 1 FROM BookingRooms WHERE RoomID=@RoomID AND Status='CheckedIn') THEN 'Occupied' WHEN EXISTS(SELECT 1 FROM BookingRooms WHERE RoomID=@RoomID AND Status='Booked') THEN 'Booked' ELSE 'Available' END WHERE RoomID=@RoomID", conn, tran))
                        {
                            cmd2.Parameters.Add("@Id", SqlDbType.Int).Value = bookingRoomId;
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                    return paymentId;
                }
            }
        }
    }
}
