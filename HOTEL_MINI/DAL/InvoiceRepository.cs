using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HOTEL_MINI.DAL
{
    /// <summary>
    /// Repository thuần cho HÓA ĐƠN + BÁO CÁO + PAYMENT (1–1 với invoice).
    /// KHÔNG xử lý gắn phòng – dùng InvoiceRoomRepository.
    /// </summary>
    public class InvoiceRepository
    {
        private readonly string _cs;
        private const byte PREC = 18;
        private const byte SCALE = 2;

        public InvoiceRepository()
        {
            _cs = ConfigHelper.GetConnectionString();
        }

        // ========= Helpers =========
        private static void AddDec(SqlCommand cmd, string name, decimal value)
        {
            var p = cmd.Parameters.Add(name, SqlDbType.Decimal);
            p.Precision = PREC; p.Scale = SCALE;
            p.Value = decimal.Round(value, SCALE, MidpointRounding.AwayFromZero);
        }

        private static T SafeGet<T>(SqlDataReader rd, int ordinal)
            => rd.IsDBNull(ordinal) ? default : (T)rd.GetValue(ordinal);

        private static bool Exists(SqlConnection conn, string sql, int id)
        {
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                var o = cmd.ExecuteScalar();
                return (o != null && o != DBNull.Value && Convert.ToInt32(o) > 0);
            }
        }
        // Get invoice totals + paid + remain + computed status
        public (decimal Total, decimal Paid, decimal Remain, string Status) GetInvoiceTotals(int invoiceId)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();

                decimal total = 0m; string statusFromDb = "Unpaid";
                using (var cmd = new SqlCommand("SELECT TotalAmount, Status FROM Invoices WHERE InvoiceID=@I", conn))
                {
                    cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (!rd.Read()) throw new Exception("Invoice không tồn tại.");
                        total = rd.GetDecimal(0);
                        statusFromDb = rd.IsDBNull(1) ? "Unpaid" : rd.GetString(1);
                    }
                }

                decimal paid = 0m;
                using (var cmd = new SqlCommand(@"
SELECT ISNULL(SUM(Amount),0)
FROM Payments
WHERE InvoiceID=@I AND Status IN ('Completed','Paid','Success','Succeeded');", conn))
                {
                    cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                    paid = Convert.ToDecimal(cmd.ExecuteScalar() ?? 0m);
                }

                var remain = Math.Max(0, total - paid);
                var status = paid <= 0m ? "Unpaid" : (remain > 0m ? "PartiallyPaid" : "Paid");
                return (total, paid, remain, status);
            }
        }


        // List payments of an invoice (most recent first)
        public List<Payment> GetPaymentsByInvoiceId(int invoiceId)
        {
            var list = new List<Payment>();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(@"
SELECT PaymentID, InvoiceID, Amount, PaymentDate, Method, Status
FROM Payments
WHERE InvoiceID = @I
ORDER BY PaymentDate DESC, PaymentID DESC", conn))
            {
                cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
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

        // Get the latest invoice by BookingID (header ID)
        public Invoice GetInvoiceByBookingID(int bookingID)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(@"
SELECT TOP 1 InvoiceID, BookingID, RoomCharge, ServiceCharge, Surcharge, Discount, TotalAmount, IssuedAt, IssuedBy, Status, Note
FROM Invoices 
WHERE BookingID = @BookingID 
ORDER BY InvoiceID DESC", conn))
            {
                cmd.Parameters.Add("@BookingID", SqlDbType.Int).Value = bookingID;
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new Invoice
                    {
                        InvoiceID = rd.GetInt32(0),
                        BookingID = rd.GetInt32(1),
                        RoomCharge = rd.GetDecimal(2),
                        ServiceCharge = rd.GetDecimal(3),
                        Surcharge = rd.GetDecimal(4),
                        Discount = rd.GetDecimal(5),
                        TotalAmount = rd.GetDecimal(6),
                        IssuedAt = rd.GetDateTime(7),
                        IssuedBy = rd.GetInt32(8),
                        Status = rd.IsDBNull(9) ? "" : rd.GetString(9),
                        Note = rd.IsDBNull(10) ? null : rd.GetString(10)
                    };
                }
            }
        }

        // Get Issuer's full name for a given invoice
        public string getFullNameByInvoiceID(int invoiceID)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(@"
SELECT u.FullName 
FROM Users u
JOIN Invoices i ON u.UserID = i.IssuedBy
WHERE i.InvoiceID = @InvoiceID", conn))
            {
                cmd.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = invoiceID;
                conn.Open();
                var result = cmd.ExecuteScalar();
                return result == null ? "N/A" : result.ToString();
            }
        }

        private static void EnsureBookingExists(SqlConnection conn, int bookingId)
        {
            if (!Exists(conn, "SELECT COUNT(1) FROM Bookings WHERE BookingID=@id", bookingId))
                throw new InvalidOperationException($"BookingID {bookingId} không tồn tại.");
        }

        private static void EnsureUserExists(SqlConnection conn, int userId)
        {
            if (!Exists(conn, "SELECT COUNT(1) FROM Users WHERE UserID=@id", userId))
                throw new InvalidOperationException($"UserID {userId} không tồn tại.");
        }

        // ========= Invoices =========
        public int AddInvoice(Invoice inv)
        {
            if (inv == null) throw new ArgumentNullException(nameof(inv));

            // Chuẩn hoá số tiền
            var rc = decimal.Round(inv.RoomCharge, SCALE, MidpointRounding.AwayFromZero);
            var sc = decimal.Round(inv.ServiceCharge, SCALE, MidpointRounding.AwayFromZero);
            var ds = decimal.Round(inv.Discount, SCALE, MidpointRounding.AwayFromZero);
            var su = decimal.Round(inv.Surcharge, SCALE, MidpointRounding.AwayFromZero);

            // Tính total ở app (nếu DB để computed thì ta không gán vào câu lệnh INSERT)
            var total = rc + sc + su - ds;
            if (total < 0) total = 0;

            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();

                EnsureBookingExists(conn, inv.BookingID);
                if (inv.IssuedBy.HasValue && inv.IssuedBy.Value > 0) EnsureUserExists(conn, inv.IssuedBy.Value);

                bool totalIsComputed = IsTotalAmountComputed(conn);

                string sql = totalIsComputed
                    ? @"
INSERT INTO Invoices
(BookingID, RoomCharge, ServiceCharge, Discount, Surcharge, IssuedAt, IssuedBy, Status, Note)
VALUES (@B, @RC, @SC, @D, @S, @At, @By, @St, @Note);
SELECT CAST(SCOPE_IDENTITY() AS INT);"
                    : @"
INSERT INTO Invoices
(BookingID, RoomCharge, ServiceCharge, Discount, Surcharge, TotalAmount, IssuedAt, IssuedBy, Status, Note)
VALUES (@B, @RC, @SC, @D, @S, @T, @At, @By, @St, @Note);
SELECT CAST(SCOPE_IDENTITY() AS INT);";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@B", SqlDbType.Int).Value = inv.BookingID;
                    AddDec(cmd, "@RC", rc);
                    AddDec(cmd, "@SC", sc);
                    AddDec(cmd, "@D", ds);
                    AddDec(cmd, "@S", su);
                    if (!totalIsComputed) AddDec(cmd, "@T", total);

                    cmd.Parameters.Add("@At", SqlDbType.DateTime).Value =
                        inv.IssuedAt == default ? DateTime.Now : inv.IssuedAt;

                    cmd.Parameters.Add("@By", SqlDbType.Int).Value =
                        (inv.IssuedBy.HasValue && inv.IssuedBy.Value > 0) ? (object)inv.IssuedBy.Value : DBNull.Value;

                    cmd.Parameters.Add("@St", SqlDbType.VarChar, 20).Value =
                        string.IsNullOrWhiteSpace(inv.Status) ? "Unpaid" : inv.Status.Trim();

                    cmd.Parameters.Add("@Note", SqlDbType.NVarChar, -1).Value =
                        (object)inv.Note ?? DBNull.Value;

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }



        public bool UpdateInvoiceTotals(Invoice inv)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(@"
UPDATE Invoices SET
    RoomCharge    = @RC,
    ServiceCharge = @SC,
    Discount      = @D,
    Surcharge     = @S
WHERE InvoiceID = @I;", conn))
            {
                cmd.Parameters.Add("@I", SqlDbType.Int).Value = inv.InvoiceID;
                AddDec(cmd, "@RC", inv.RoomCharge);
                AddDec(cmd, "@SC", inv.ServiceCharge);
                AddDec(cmd, "@D", inv.Discount);
                AddDec(cmd, "@S", inv.Surcharge);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        public void UpdateInvoiceStatus(int invoiceId, string status, int? issuedByIfPaid = null)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                string sql = (status == "Paid" && issuedByIfPaid.HasValue)
                    ? @"UPDATE Invoices SET Status=@s, IssuedBy=@by, IssuedAt=GETDATE() WHERE InvoiceID=@i"
                    : @"UPDATE Invoices SET Status=@s WHERE InvoiceID=@i";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@s", SqlDbType.VarChar, 20).Value = status;
                    cmd.Parameters.Add("@i", SqlDbType.Int).Value = invoiceId;
                    if (status == "Paid" && issuedByIfPaid.HasValue)
                    {
                        EnsureUserExists(conn, issuedByIfPaid.Value);
                        cmd.Parameters.Add("@by", SqlDbType.Int).Value = issuedByIfPaid.Value;
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Invoice GetInvoiceById(int invoiceId)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(@"
SELECT InvoiceID, BookingID, RoomCharge, ServiceCharge, Surcharge, Discount, TotalAmount, IssuedAt, IssuedBy, Status, Note
FROM Invoices WHERE InvoiceID=@I", conn))
            {
                cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new Invoice
                    {
                        InvoiceID = rd.GetInt32(0),
                        BookingID = rd.GetInt32(1),
                        RoomCharge = rd.GetDecimal(2),
                        ServiceCharge = rd.GetDecimal(3),
                        Surcharge = rd.GetDecimal(4),
                        Discount = rd.GetDecimal(5),
                        TotalAmount = rd.GetDecimal(6),
                        IssuedAt = rd.GetDateTime(7),
                        IssuedBy = rd.IsDBNull(8) ? 0 : rd.GetInt32(8),
                        Status = SafeGet<string>(rd, 9) ?? "Unpaid",
                        Note = SafeGet<string>(rd, 10)
                    };
                }
            }
        }

        public Invoice GetLatestInvoiceByBookingID(int bookingId)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(@"
SELECT TOP 1 InvoiceID, BookingID, RoomCharge, ServiceCharge, Surcharge, Discount, TotalAmount, IssuedAt, IssuedBy, Status, Note
FROM Invoices WHERE BookingID=@B ORDER BY InvoiceID DESC", conn))
            {
                cmd.Parameters.Add("@B", SqlDbType.Int).Value = bookingId;
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new Invoice
                    {
                        InvoiceID = rd.GetInt32(0),
                        BookingID = rd.GetInt32(1),
                        RoomCharge = rd.GetDecimal(2),
                        ServiceCharge = rd.GetDecimal(3),
                        Surcharge = rd.GetDecimal(4),
                        Discount = rd.GetDecimal(5),
                        TotalAmount = rd.GetDecimal(6),
                        IssuedAt = rd.GetDateTime(7),
                        IssuedBy = rd.IsDBNull(8) ? 0 : rd.GetInt32(8),
                        Status = SafeGet<string>(rd, 9) ?? "Unpaid",
                        Note = SafeGet<string>(rd, 10)
                    };
                }
            }
        }

        public List<Invoice> GetAllInvoices()
        {
            var list = new List<Invoice>();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(@"
SELECT InvoiceID, BookingID, RoomCharge, ServiceCharge, Surcharge, Discount, TotalAmount, IssuedAt, IssuedBy, Status, Note
FROM Invoices ORDER BY InvoiceID DESC", conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new Invoice
                        {
                            InvoiceID = rd.GetInt32(0),
                            BookingID = rd.GetInt32(1),
                            RoomCharge = rd.GetDecimal(2),
                            ServiceCharge = rd.GetDecimal(3),
                            Surcharge = rd.GetDecimal(4),
                            Discount = rd.GetDecimal(5),
                            TotalAmount = rd.GetDecimal(6),
                            IssuedAt = rd.GetDateTime(7),
                            IssuedBy = rd.IsDBNull(8) ? 0 : rd.GetInt32(8),
                            Status = SafeGet<string>(rd, 9) ?? "Unpaid",
                            Note = SafeGet<string>(rd, 10)
                        });
                    }
                }
            }
            return list;
        }

        // ========= Payment (1–1: 1 invoice có tối đa 1 payment) =========

        public Payment GetPaymentForInvoice(int invoiceId)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(@"
SELECT TOP 1 PaymentID, InvoiceID, Amount, PaymentDate, Method, Status
FROM Payments WHERE InvoiceID=@I ORDER BY PaymentID DESC", conn))
            {
                cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new Payment
                    {
                        PaymentID = rd.GetInt32(0),
                        InvoiceID = rd.GetInt32(1),
                        Amount = rd.GetDecimal(2),
                        PaymentDate = rd.GetDateTime(3),
                        Method = SafeGet<string>(rd, 4) ?? "Cash",
                        Status = SafeGet<string>(rd, 5) ?? "Paid"
                    };
                }
            }
        }

        /// <summary>
        /// Upsert 1 payment cho 1 invoice. Mặc định enforce thanh toán 1 lần đủ tiền:
        /// - Nếu amount != TotalAmount => lỗi (có thể bỏ enforce bằng allowPartial=true).
        /// </summary>
        public int UpsertPaymentForInvoice(int invoiceId, decimal amount, DateTime paidAt, string method, string status, bool allowPartial = false)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();

                decimal total;
                using (var cmdT = new SqlCommand("SELECT TotalAmount FROM Invoices WHERE InvoiceID=@I", conn))
                {
                    cmdT.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                    var o = cmdT.ExecuteScalar();
                    if (o == null || o == DBNull.Value) throw new InvalidOperationException("Invoice không tồn tại.");
                    total = Convert.ToDecimal(o);
                }

                if (!allowPartial && amount != total)
                    throw new InvalidOperationException("Số tiền thanh toán phải bằng tổng hóa đơn (thanh toán 1 lần).");

                // nếu đã có payment -> update, chưa có -> insert
                var old = GetPaymentForInvoice(invoiceId);
                if (old == null)
                {
                    using (var ins = new SqlCommand(@"
INSERT INTO Payments(InvoiceID, Amount, PaymentDate, Method, Status)
OUTPUT INSERTED.PaymentID
VALUES(@I,@A,@D,@M,@S);", conn))
                    {
                        ins.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                        AddDec(ins, "@A", amount);
                        ins.Parameters.Add("@D", SqlDbType.DateTime).Value = paidAt == default ? DateTime.Now : paidAt;
                        ins.Parameters.Add("@M", SqlDbType.VarChar, 20).Value = string.IsNullOrWhiteSpace(method) ? "Cash" : method.Trim();
                        ins.Parameters.Add("@S", SqlDbType.VarChar, 20).Value = string.IsNullOrWhiteSpace(status) ? "Paid" : status.Trim();
                        return Convert.ToInt32(ins.ExecuteScalar());
                    }
                }
                else
                {
                    using (var upd = new SqlCommand(@"
UPDATE Payments SET Amount=@A, PaymentDate=@D, Method=@M, Status=@S
WHERE PaymentID=@P", conn))
                    {
                        upd.Parameters.Add("@P", SqlDbType.Int).Value = old.PaymentID;
                        AddDec(upd, "@A", amount);
                        upd.Parameters.Add("@D", SqlDbType.DateTime).Value = paidAt == default ? DateTime.Now : paidAt;
                        upd.Parameters.Add("@M", SqlDbType.VarChar, 20).Value = string.IsNullOrWhiteSpace(method) ? "Cash" : method.Trim();
                        upd.Parameters.Add("@S", SqlDbType.VarChar, 20).Value = string.IsNullOrWhiteSpace(status) ? "Paid" : status.Trim();
                        upd.ExecuteNonQuery();
                        return old.PaymentID;
                    }
                }
            }
        }

        public void DeletePaymentForInvoice(int invoiceId)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("DELETE FROM Payments WHERE InvoiceID=@I", conn))
            {
                cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void RefreshInvoiceStatusFromPayment(int invoiceId, int? issuedByIfPaid = null)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                decimal total = 0m;
                using (var c = new SqlCommand("SELECT TotalAmount FROM Invoices WHERE InvoiceID=@I", conn))
                {
                    c.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                    var o = c.ExecuteScalar();
                    if (o == null || o == DBNull.Value) return;
                    total = Convert.ToDecimal(o);
                }

                var pay = GetPaymentForInvoice(invoiceId);
                var status = (pay == null || pay.Status == "Unpaid" || pay.Amount <= 0m) ? "Unpaid"
                           : (pay.Amount < total ? "PartiallyPaid" : "Paid");

                string sql = (status == "Paid" && issuedByIfPaid.HasValue)
                    ? "UPDATE Invoices SET Status=@S, IssuedBy=@U, IssuedAt=GETDATE() WHERE InvoiceID=@I"
                    : "UPDATE Invoices SET Status=@S WHERE InvoiceID=@I";

                using (var u = new SqlCommand(sql, conn))
                {
                    u.Parameters.Add("@S", SqlDbType.VarChar, 20).Value = status;
                    u.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                    if (status == "Paid" && issuedByIfPaid.HasValue)
                    {
                        EnsureUserExists(conn, issuedByIfPaid.Value);
                        u.Parameters.Add("@U", SqlDbType.Int).Value = issuedByIfPaid.Value;
                    }
                    u.ExecuteNonQuery();
                }
            }
        }

        // ========= Reports =========

        public List<RevenueRoomDTO> GetRevenueByRoom(int month, int year)
        {
            var rs = new List<RevenueRoomDTO>();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(@"
SELECT r.RoomNumber,
       MONTH(i.IssuedAt) AS [Month],
       YEAR(i.IssuedAt)  AS [Year],
       CAST(SUM(i.RoomCharge)    AS DECIMAL(18,2)) AS RoomRevenue,
       CAST(SUM(i.ServiceCharge) AS DECIMAL(18,2)) AS ServiceRevenue,
       CAST(SUM(i.TotalAmount)   AS DECIMAL(18,2)) AS TotalRevenue
FROM Invoices i
JOIN Bookings b      ON b.BookingID = i.BookingID
JOIN InvoiceRooms ir ON ir.InvoiceID = i.InvoiceID
JOIN BookingRooms br ON br.BookingRoomID = ir.BookingRoomID
JOIN Rooms r         ON r.RoomID = br.RoomID
WHERE MONTH(i.IssuedAt)=@M AND YEAR(i.IssuedAt)=@Y
  AND i.Status IN ('Paid','PartiallyPaid')
GROUP BY r.RoomNumber, MONTH(i.IssuedAt), YEAR(i.IssuedAt)
ORDER BY r.RoomNumber;", conn))
            {
                cmd.Parameters.Add("@M", SqlDbType.Int).Value = month;
                cmd.Parameters.Add("@Y", SqlDbType.Int).Value = year;
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        rs.Add(new RevenueRoomDTO
                        {
                            RoomNumber = rd.IsDBNull(0) ? "" : rd.GetString(0),
                            Month = rd.GetInt32(1),
                            Year = rd.GetInt32(2),
                            RoomRevenue = rd.GetDecimal(3),
                            ServiceRevenue = rd.GetDecimal(4),
                            TotalRevenue = rd.GetDecimal(5)
                        });
                    }
                }
            }
            return rs;
        }

        public DataTable GetRevenueLast6Months()
        {
            const string sql = @"
;WITH Last6 AS (
    SELECT DATEADD(MONTH,-5, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()),1)) AS StartDate
), Series AS(
    SELECT DATEADD(MONTH,n,(SELECT StartDate FROM Last6)) AS MonthDate
    FROM (VALUES(0),(1),(2),(3),(4),(5))v(n)
)
SELECT 
    FORMAT(MonthDate,'MM/yyyy') AS PeriodKey,
    DATENAME(MONTH,MonthDate)+' '+FORMAT(MonthDate,'yyyy') AS DisplayPeriod,
    CAST(ISNULL(SUM(i.TotalAmount),0) AS DECIMAL(18,2)) AS TotalRevenue
FROM Series s
LEFT JOIN Invoices i
  ON i.IssuedAt>=s.MonthDate AND i.IssuedAt<DATEADD(MONTH,1,s.MonthDate)
 AND i.Status IN ('Paid','PartiallyPaid')
GROUP BY MonthDate
ORDER BY MonthDate";
            return Fill(sql, null);
        }

        public DataTable GetRevenueByMonth(int year)
        {
            const string sql = @"
;WITH Series AS(
  SELECT DATEFROMPARTS(@Y,n,1) AS MonthDate
  FROM (VALUES(1),(2),(3),(4),(5),(6),(7),(8),(9),(10),(11),(12))v(n)
)
SELECT 
  FORMAT(MonthDate,'MM/yyyy') AS PeriodKey,
  DATENAME(MONTH,MonthDate)+' '+FORMAT(MonthDate,'yyyy') AS DisplayPeriod,
  CAST(ISNULL(SUM(i.TotalAmount),0) AS DECIMAL(18,2)) AS TotalRevenue
FROM Series s
LEFT JOIN Invoices i
  ON YEAR(i.IssuedAt)=@Y AND MONTH(i.IssuedAt)=MONTH(s.MonthDate)
 AND i.Status IN ('Paid','PartiallyPaid')
GROUP BY MonthDate
ORDER BY MonthDate";
            var dt = new DataTable();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@Y", SqlDbType.Int).Value = year;
                conn.Open();
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        public DataTable GetRevenueByCurrentWeek()
        {
            var today = DateTime.Today;
            int delta = ((int)today.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            var start = today.AddDays(-delta);
            var end = start.AddDays(6);

            const string sql = @"
;WITH D AS(
  SELECT DATEADD(DAY,n,@S) AS DayDate
  FROM (VALUES(0),(1),(2),(3),(4),(5),(6))v(n)
)
SELECT 
  FORMAT(D.DayDate,'dd/MM/yyyy') AS PeriodKey,
  CASE DATEPART(WEEKDAY, D.DayDate)
        WHEN 1 THEN N'Chủ nhật'
        WHEN 2 THEN N'Thứ 2'
        WHEN 3 THEN N'Thứ 3'
        WHEN 4 THEN N'Thứ 4'
        WHEN 5 THEN N'Thứ 5'
        WHEN 6 THEN N'Thứ 6'
        WHEN 7 THEN N'Thứ 7'
  END + ' ' + FORMAT(D.DayDate,'dd/MM') AS DisplayPeriod,
  CAST(ISNULL(SUM(i.TotalAmount),0) AS DECIMAL(18,2)) AS TotalRevenue
FROM D
LEFT JOIN Invoices i
  ON CAST(i.IssuedAt AS DATE)=D.DayDate
 AND i.Status IN ('Paid','PartiallyPaid')
WHERE D.DayDate<=@E
GROUP BY D.DayDate
ORDER BY D.DayDate";
            var dt = new DataTable();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@S", SqlDbType.Date).Value = start;
                cmd.Parameters.Add("@E", SqlDbType.Date).Value = end;
                conn.Open();
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        public RevenueSummary GetRevenueSummary()
        {
            const string sql = @"
SELECT 
  CAST(ISNULL(SUM(RoomCharge),0) AS DECIMAL(18,2)),
  CAST(ISNULL(SUM(ServiceCharge),0) AS DECIMAL(18,2)),
  CAST(ISNULL(SUM(Discount),0) AS DECIMAL(18,2)),
  CAST(ISNULL(SUM(Surcharge),0) AS DECIMAL(18,2)),
  CAST(ISNULL(SUM(TotalAmount),0) AS DECIMAL(18,2))
FROM Invoices
WHERE Status IN ('Paid','PartiallyPaid')
  AND IssuedAt >= DATEADD(MONTH,-6,GETDATE())";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return new RevenueSummary();
                    return new RevenueSummary
                    {
                        RoomCharge = rd.GetDecimal(0),
                        ServiceCharge = rd.GetDecimal(1),
                        Discount = rd.GetDecimal(2),
                        Surcharge = rd.GetDecimal(3),
                        TotalAmount = rd.GetDecimal(4)
                    };
                }
            }
        }

        // ========= Joins for list =========
        public class InvoiceListItem
        {
            public int InvoiceID { get; set; }
            public int BookingID { get; set; }
            public decimal RoomCharge { get; set; }
            public decimal ServiceCharge { get; set; }
            public decimal Surcharge { get; set; }
            public decimal Discount { get; set; }
            public decimal TotalAmount { get; set; }
            public DateTime IssuedAt { get; set; }
            public int IssuedBy { get; set; }
            public string Status { get; set; }
            public string Note { get; set; }
            public string CustomerName { get; set; }
            public string CustomerIDNumber { get; set; }
        }

        public List<InvoiceListItem> GetAllInvoicesWithCustomer()
        {
            const string sql = @"
SELECT 
  i.InvoiceID, i.BookingID, i.RoomCharge, i.ServiceCharge, i.Surcharge, i.Discount, i.TotalAmount,
  i.IssuedAt, i.IssuedBy, i.Status, i.Note,
  c.FullName, c.IDNumber
FROM Invoices i
JOIN Bookings b  ON b.BookingID = i.BookingID
JOIN Customers c ON c.CustomerID = b.CustomerID
ORDER BY i.InvoiceID DESC;";
            var list = new List<InvoiceListItem>();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new InvoiceListItem
                        {
                            InvoiceID = rd.GetInt32(0),
                            BookingID = rd.GetInt32(1),
                            RoomCharge = rd.GetDecimal(2),
                            ServiceCharge = rd.GetDecimal(3),
                            Surcharge = rd.GetDecimal(4),
                            Discount = rd.GetDecimal(5),
                            TotalAmount = rd.GetDecimal(6),
                            IssuedAt = rd.GetDateTime(7),
                            IssuedBy = rd.IsDBNull(8) ? 0 : rd.GetInt32(8),
                            Status = rd.IsDBNull(9) ? "" : rd.GetString(9),
                            Note = rd.IsDBNull(10) ? null : rd.GetString(10),
                            CustomerName = rd.IsDBNull(11) ? "" : rd.GetString(11),
                            CustomerIDNumber = rd.IsDBNull(12) ? "" : rd.GetString(12),
                        });
                    }
                }
            }
            return list;
        }

        public List<InvoiceListItem> GetInvoicesByCustomerNumber(string idNumber)
        {
            const string sql = @"
SELECT 
  i.InvoiceID, i.BookingID, i.RoomCharge, i.ServiceCharge, i.Surcharge, i.Discount, i.TotalAmount,
  i.IssuedAt, i.IssuedBy, i.Status, i.Note,
  c.FullName, c.IDNumber
FROM Customers c
JOIN Bookings b ON b.CustomerID = c.CustomerID
JOIN Invoices i ON i.BookingID = b.BookingID
WHERE c.IDNumber = @ID
ORDER BY i.InvoiceID DESC;";
            var list = new List<InvoiceListItem>();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@ID", SqlDbType.NVarChar, 50).Value = idNumber;
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new InvoiceListItem
                        {
                            InvoiceID = rd.GetInt32(0),
                            BookingID = rd.GetInt32(1),
                            RoomCharge = rd.GetDecimal(2),
                            ServiceCharge = rd.GetDecimal(3),
                            Surcharge = rd.GetDecimal(4),
                            Discount = rd.GetDecimal(5),
                            TotalAmount = rd.GetDecimal(6),
                            IssuedAt = rd.GetDateTime(7),
                            IssuedBy = rd.IsDBNull(8) ? 0 : rd.GetInt32(8),
                            Status = rd.IsDBNull(9) ? "" : rd.GetString(9),
                            Note = rd.IsDBNull(10) ? null : rd.GetString(10),
                            CustomerName = rd.IsDBNull(11) ? "" : rd.GetString(11),
                            CustomerIDNumber = rd.IsDBNull(12) ? "" : rd.GetString(12),
                        });
                    }
                }
            }
            return list;
        }

        public InvoiceListItem GetInvoiceWithCustomerByInvoiceId(int invoiceId)
        {
            const string sql = @"
SELECT 
  i.InvoiceID, i.BookingID, i.RoomCharge, i.ServiceCharge, i.Surcharge, i.Discount, i.TotalAmount,
  i.IssuedAt, i.IssuedBy, i.Status, i.Note,
  c.FullName, c.IDNumber
FROM Invoices i
JOIN Bookings b  ON b.BookingID = i.BookingID
JOIN Customers c ON c.CustomerID = b.CustomerID
WHERE i.InvoiceID = @I;";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new InvoiceListItem
                    {
                        InvoiceID = rd.GetInt32(0),
                        BookingID = rd.GetInt32(1),
                        RoomCharge = rd.GetDecimal(2),
                        ServiceCharge = rd.GetDecimal(3),
                        Surcharge = rd.GetDecimal(4),
                        Discount = rd.GetDecimal(5),
                        TotalAmount = rd.GetDecimal(6),
                        IssuedAt = rd.GetDateTime(7),
                        IssuedBy = rd.IsDBNull(8) ? 0 : rd.GetInt32(8),
                        Status = rd.IsDBNull(9) ? "" : rd.GetString(9),
                        Note = rd.IsDBNull(10) ? null : rd.GetString(10),
                        CustomerName = rd.IsDBNull(11) ? "" : rd.GetString(11),
                        CustomerIDNumber = rd.IsDBNull(12) ? "" : rd.GetString(12),
                    };
                }
            }
        }
        private static bool IsTotalAmountComputed(SqlConnection conn, SqlTransaction tran = null)
        {
            using (var cmd = new SqlCommand(@"
SELECT CASE WHEN c.is_computed = 1 THEN 1 ELSE 0 END
FROM sys.columns c
WHERE c.object_id = OBJECT_ID(N'[dbo].[Invoices]')
  AND c.name = N'TotalAmount';", conn, tran))
            {
                var o = cmd.ExecuteScalar();
                return (o != null && o != DBNull.Value && Convert.ToInt32(o) == 1);
            }
        }
        // Thêm vào class InvoiceRepository
        public int CreateOrGetOpenInvoice(
    int bookingId,
    decimal roomCharge,
    decimal serviceCharge,
    decimal discount,
    decimal surcharge,
    int issuedByUserIfPaid = 0)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();

                // 1) Booking phải tồn tại
                EnsureBookingExists(conn, bookingId);

                // 2) Nếu đã có hoá đơn mở → trả về luôn (khóa để tránh race)
                using (var find = new SqlCommand(@"
SELECT TOP(1) InvoiceID
FROM Invoices WITH (UPDLOCK, ROWLOCK)
WHERE BookingID=@B AND Status IN ('Unpaid','PartiallyPaid')
ORDER BY InvoiceID DESC;", conn))
                {
                    find.Parameters.Add("@B", SqlDbType.Int).Value = bookingId;
                    var o = find.ExecuteScalar();
                    if (o != null && o != DBNull.Value) return Convert.ToInt32(o);
                }

                // 3) Xác định IssuedBy: ưu tiên tham số; nếu không hợp lệ → NULL
                int? issuer = null;
                if (issuedByUserIfPaid > 0)
                {
                    try { EnsureUserExists(conn, issuedByUserIfPaid); issuer = issuedByUserIfPaid; }
                    catch { issuer = null; }
                }
                if (!issuer.HasValue)
                {
                    // fallback: dùng CreatedBy của Booking nếu hợp lệ
                    using (var cmdU = new SqlCommand("SELECT CreatedBy FROM Bookings WHERE BookingID=@B", conn))
                    {
                        cmdU.Parameters.Add("@B", SqlDbType.Int).Value = bookingId;
                        var u = cmdU.ExecuteScalar();
                        if (u != null && u != DBNull.Value)
                        {
                            int tryId = Convert.ToInt32(u);
                            try { EnsureUserExists(conn, tryId); issuer = tryId; } catch { issuer = null; }
                        }
                    }
                }

                // 4) Chuẩn hoá số tiền & tính Total
                var rc = decimal.Round(roomCharge, SCALE, MidpointRounding.AwayFromZero);
                var sc = decimal.Round(serviceCharge, SCALE, MidpointRounding.AwayFromZero);
                var ds = decimal.Round(discount, SCALE, MidpointRounding.AwayFromZero);
                var su = decimal.Round(surcharge, SCALE, MidpointRounding.AwayFromZero);

                var total = rc + sc + su - ds;
                if (total < 0) total = 0;

                bool totalIsComputed = IsTotalAmountComputed(conn);

                string sql = totalIsComputed
                    ? @"
INSERT INTO Invoices
(BookingID, RoomCharge, ServiceCharge, Discount, Surcharge, IssuedAt, IssuedBy, Status, Note)
VALUES(@B, @RC, @SC, @D, @S, GETDATE(), @U, 'Unpaid', NULL);
SELECT CAST(SCOPE_IDENTITY() AS INT);"
                    : @"
INSERT INTO Invoices
(BookingID, RoomCharge, ServiceCharge, Discount, Surcharge, TotalAmount, IssuedAt, IssuedBy, Status, Note)
VALUES(@B, @RC, @SC, @D, @S, @TT, GETDATE(), @U, 'Unpaid', NULL);
SELECT CAST(SCOPE_IDENTITY() AS INT);";

                using (var ins = new SqlCommand(sql, conn))
                {
                    ins.Parameters.Add("@B", SqlDbType.Int).Value = bookingId;
                    AddDec(ins, "@RC", rc);
                    AddDec(ins, "@SC", sc);
                    AddDec(ins, "@D", ds);
                    AddDec(ins, "@S", su);
                    if (!totalIsComputed) AddDec(ins, "@TT", total);
                    ins.Parameters.Add("@U", SqlDbType.Int).Value = (object)issuer ?? DBNull.Value;

                    return Convert.ToInt32(ins.ExecuteScalar());
                }
            }
        }


        // ========= util =========
        private DataTable Fill(string sql, Action<SqlCommand> onParam)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                onParam?.Invoke(cmd);
                conn.Open();
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }
    }
}
