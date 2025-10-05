using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HOTEL_MINI.DAL
{
    /// <summary>
    /// Repository cho bảng Payments.
    /// - Tiền tệ chuẩn DECIMAL(18,2)
    /// - Chuẩn hoá Payment.Method/Status theo enum để không vỡ FK
    /// - AddPaymentSafe chạy trong transaction, đồng bộ trạng thái Invoices/Bookings/Rooms
    /// </summary>
    public class PaymentRepository
    {
        private readonly string _cs;

        // Chuẩn decimal: DECIMAL(18,2)
        private const byte PREC = 18;
        private const byte SCALE = 2;

        private static readonly HashSet<string> AllowedStatus =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "Paid", "Unpaid", "PartiallyPaid" };

        private static readonly HashSet<string> AllowedMethod =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "Cash", "Card", "Transfer" };

        public PaymentRepository()
        {
            _cs = ConfigHelper.GetConnectionString();
        }

        // ===== Helpers =====
        private static void AddDec(SqlCommand cmd, string name, decimal value)
        {
            var p = cmd.Parameters.Add(name, SqlDbType.Decimal);
            p.Precision = PREC;
            p.Scale = SCALE;
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

        // ============================================================
        //                           CRUD
        // ============================================================

        /// <summary>Thêm payment đơn lẻ (không cập nhật trạng thái hóa đơn).</summary>
        public List<Payment> GetPaymentsByInvoice(int invoiceId)
        {
            var list = new List<Payment>();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(@"
SELECT PaymentID, InvoiceID, Amount, PaymentDate, Method, Status
FROM   Payments
WHERE  InvoiceID=@I
ORDER BY PaymentDate", conn))
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
            using (var cmd = new SqlCommand(@"
INSERT INTO Payments(InvoiceID, Amount, PaymentDate, Method, Status)
VALUES(@I,@A,@D,@M,@S)", conn))
            {
                cmd.Parameters.AddWithValue("@I", p.InvoiceID);
                cmd.Parameters.AddWithValue("@A", p.Amount);
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
            using (var cmd = new SqlCommand(@"
SELECT p.PaymentID, p.InvoiceID, p.Amount, p.PaymentDate, p.Method, p.Status
FROM   Payments p
JOIN   Invoices i ON i.InvoiceID = p.InvoiceID
WHERE  i.BookingID = @bid
ORDER BY p.PaymentDate ASC", conn))
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
                            Status = rd.IsDBNull(5) ? "" : rd.GetString(5),
                        });
                    }
                }
            }
            return list;
        }

        /// <summary>Tổng đã trả (chỉ tính Payment.Status='Paid') theo Booking header.</summary>
        public decimal GetPaidAmountByHeaderId(int headerBookingId)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(@"
SELECT ISNULL(SUM(p.Amount), 0)
FROM   Payments p
JOIN   Invoices i ON i.InvoiceID = p.InvoiceID
WHERE  i.BookingID = @bid
  AND  p.Status = 'Paid'", conn))
            {
                cmd.Parameters.Add("@bid", SqlDbType.Int).Value = headerBookingId;

                conn.Open();
                var o = cmd.ExecuteScalar();
                return o == null || o == DBNull.Value ? 0m : Convert.ToDecimal(o);
            }
        }

        /// <summary>Tổng đã trả (chỉ tính Payment.Status='Paid') theo InvoiceID.</summary>
        public decimal GetPaidAmountByInvoiceId(int invoiceId)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(@"
SELECT ISNULL(SUM(Amount), 0)
FROM   Payments
WHERE  InvoiceID = @I AND Status='Paid';", conn))
            {
                cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;

                conn.Open();
                var o = cmd.ExecuteScalar();
                return o == null || o == DBNull.Value ? 0m : Convert.ToDecimal(o);
            }
        }

        // ============================================================
        //                   Transactional Insert (Safe)
        // ============================================================

        /// <summary>
        /// Thêm payment trong transaction và:
        ///  - Cập nhật trạng thái Invoices theo tổng đã trả (chỉ tính 'Paid').
        ///  - Nếu hóa đơn đã trả đủ: Checkout booking, set BookingRooms='CheckedOut',
        ///    trả phòng về 'Available' nếu không còn booking khác chiếm chỗ, và Booking.Status='CheckedOut'.
        /// </summary>
        public int AddPaymentSafe(int invoiceId, decimal amount, string method, string status, int issuedByIfPaid)
        {
            if (amount <= 0) throw new Exception("Số tiền phải > 0.");

            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    // 1) Khóa + lấy tổng tiền & đã trả
                    decimal total, paid;
                    using (var cmd = new SqlCommand(@"
SELECT  i.TotalAmount,
        ISNULL(SUM(CASE WHEN p.Status='Paid' THEN p.Amount ELSE 0 END), 0)
FROM    Invoices i WITH (UPDLOCK, ROWLOCK)
LEFT JOIN Payments p ON p.InvoiceID = i.InvoiceID
WHERE   i.InvoiceID = @I
GROUP BY i.TotalAmount", conn, tran))
                    {
                        cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                        using (var rd = cmd.ExecuteReader())
                        {
                            if (!rd.Read()) throw new Exception("Invoice không tồn tại.");
                            total = rd.GetDecimal(0);
                            paid = rd.GetDecimal(1);
                        }
                    }

                    var remain = total - paid;
                    if (amount > remain + 0.0001m)
                        throw new Exception("Số tiền thanh toán vượt quá số còn lại.");

                    var normMethod = NormalizeMethod(method);
                    var normStatus = NormalizeStatus(status);

                    // 2) Ghi nhận payment
                    int paymentId;
                    using (var ins = new SqlCommand(@"
INSERT INTO Payments(InvoiceID, Amount, PaymentDate, Method, Status)
OUTPUT INSERTED.PaymentID
VALUES (@I, @A, GETDATE(), @M, @S);", conn, tran))
                    {
                        ins.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                        AddDec(ins, "@A", amount);
                        ins.Parameters.Add("@M", SqlDbType.VarChar, 20).Value = normMethod;
                        ins.Parameters.Add("@S", SqlDbType.VarChar, 20).Value = normStatus;
                        paymentId = Convert.ToInt32(ins.ExecuteScalar());
                    }

                    // 3) Cập nhật trạng thái hóa đơn
                    decimal newPaid = paid;
                    if (string.Equals(normStatus, "Paid", StringComparison.OrdinalIgnoreCase))
                        newPaid += amount;

                    var newRemain = Math.Max(0m, total - newPaid);
                    var newStatus = (newPaid <= 0m) ? "Unpaid"
                                   : (newRemain > 0m ? "PartiallyPaid" : "Paid");

                    using (var upd = new SqlCommand(
                        (newStatus == "Paid")
                            ? "UPDATE Invoices SET Status=@S, IssuedAt=GETDATE(), IssuedBy=@U WHERE InvoiceID=@I"
                            : "UPDATE Invoices SET Status=@S WHERE InvoiceID=@I",
                        conn, tran))
                    {
                        upd.Parameters.Add("@S", SqlDbType.VarChar, 20).Value = newStatus;
                        upd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                        if (newStatus == "Paid")
                            upd.Parameters.Add("@U", SqlDbType.Int).Value = issuedByIfPaid;
                        upd.ExecuteNonQuery();
                    }

                    // 4) Nếu đã thanh toán đủ → checkout booking + trả phòng
                    if (newStatus == "Paid")
                    {
                        // Lấy BookingID
                        int bookingId;
                        using (var g = new SqlCommand(
                            "SELECT BookingID FROM Invoices WHERE InvoiceID=@I", conn, tran))
                        {
                            g.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                            bookingId = Convert.ToInt32(g.ExecuteScalar());
                        }

                        // a) Checkout các dòng phòng còn Booked/CheckedIn
                        using (var br = new SqlCommand(@"
UPDATE BookingRooms
   SET Status='CheckedOut',
       CheckOutDate = ISNULL(CheckOutDate, GETDATE())
WHERE BookingID=@B AND Status IN ('CheckedIn','Booked');", conn, tran))
                        {
                            br.Parameters.Add("@B", SqlDbType.Int).Value = bookingId;
                            br.ExecuteNonQuery();
                        }

                        // b) Trả trạng thái phòng về Available nếu không còn booking khác chiếm
                        using (var r = new SqlCommand(@"
UPDATE r SET r.Status = 'Available'
FROM Rooms r
JOIN BookingRooms br ON br.RoomID = r.RoomID
WHERE br.BookingID = @B
  AND NOT EXISTS (
      SELECT 1 FROM BookingRooms x
      WHERE x.RoomID = r.RoomID
        AND x.Status IN ('Booked','CheckedIn')
        AND x.BookingID <> @B
  );", conn, tran))
                        {
                            r.Parameters.Add("@B", SqlDbType.Int).Value = bookingId;
                            r.ExecuteNonQuery();
                        }

                        // c) Đặt Booking header = CheckedOut
                        using (var b = new SqlCommand(
                            "UPDATE Bookings SET Status='CheckedOut' WHERE BookingID=@B;", conn, tran))
                        {
                            b.Parameters.Add("@B", SqlDbType.Int).Value = bookingId;
                            b.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                    return paymentId;
                }
            }
        }
    }
}
