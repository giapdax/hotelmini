using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HOTEL_MINI.DAL
{
    public class InvoiceRepository
    {
        private readonly string _stringConnection;
        private const byte PRECISION = 18;
        private const byte SCALE = 2;

        public InvoiceRepository()
        {
            _stringConnection = ConfigHelper.GetConnectionString();
        }

        private static void AddDec(SqlCommand cmd, string name, decimal value)
        {
            var p = cmd.Parameters.Add(name, SqlDbType.Decimal);
            p.Precision = PRECISION;
            p.Scale = SCALE;
            p.Value = decimal.Round(value, SCALE, MidpointRounding.AwayFromZero);
        }

        private static bool Exists(SqlConnection conn, string sql, int id)
        {
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                var o = cmd.ExecuteScalar();
                if (o == null || o == DBNull.Value) return false;
                return Convert.ToInt32(o) > 0;
            }
        }

        private static void EnsureBookingExists(SqlConnection conn, int bookingId)
        {
            if (!Exists(conn, "SELECT COUNT(1) FROM Bookings WHERE BookingID=@id", bookingId))
                throw new InvalidOperationException($"BookingID {bookingId} không tồn tại – không thể tạo hóa đơn.");
        }

        private static void EnsureUserExists(SqlConnection conn, int userId)
        {
            if (!Exists(conn, "SELECT COUNT(1) FROM Users WHERE UserID=@id", userId))
                throw new InvalidOperationException($"UserID {userId} (IssuedBy) không tồn tại.");
        }

        private static bool InvoiceExistsForBooking(SqlConnection conn, int bookingId)
        {
            return Exists(conn, "SELECT COUNT(1) FROM Invoices WHERE BookingID=@id", bookingId);
        }

        private static int GetBookingCreatedBy(SqlConnection conn, int bookingId)
        {
            using (var cmd = new SqlCommand("SELECT CreatedBy FROM Bookings WHERE BookingID=@id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = bookingId;
                var o = cmd.ExecuteScalar();
                return (o == null || o == DBNull.Value) ? 0 : Convert.ToInt32(o);
            }
        }

        public List<RevenueRoomDTO> GetRevenueByRoom(int month, int year)
        {
            var result = new List<RevenueRoomDTO>();
            using (var connection = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand(@"
SELECT r.RoomNumber,
       MONTH(i.IssuedAt) AS [Month],
       YEAR(i.IssuedAt)  AS [Year],
       CAST(SUM(i.RoomCharge)    AS DECIMAL(18,2)) AS RoomRevenue,
       CAST(SUM(i.ServiceCharge) AS DECIMAL(18,2)) AS ServiceRevenue,
       CAST(SUM(i.TotalAmount)   AS DECIMAL(18,2)) AS TotalRevenue
FROM Invoices i
JOIN Bookings b        ON b.BookingID = i.BookingID
JOIN BookingRooms br   ON br.BookingID = b.BookingID
JOIN Rooms r           ON r.RoomID = br.RoomID
WHERE MONTH(i.IssuedAt) = @Month 
  AND YEAR(i.IssuedAt)  = @Year
  AND i.Status IN ('Paid','PartiallyPaid')
GROUP BY r.RoomNumber, MONTH(i.IssuedAt), YEAR(i.IssuedAt)
ORDER BY r.RoomNumber;", connection))
            {
                cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month;
                cmd.Parameters.Add("@Year", SqlDbType.Int).Value = year;

                connection.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        result.Add(new RevenueRoomDTO
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
            return result;
        }

        public DataTable GetRevenueLast6Months()
        {
            const string query = @"
;WITH Last6Months AS (
    SELECT DATEADD(MONTH, -5, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)) AS StartDate
),
MonthSeries AS (
    SELECT DATEADD(MONTH, n, (SELECT StartDate FROM Last6Months)) AS MonthDate
    FROM (VALUES(0),(1),(2),(3),(4),(5)) v(n)
)
SELECT 
    FORMAT(ms.MonthDate, 'MM/yyyy') AS PeriodKey,
    DATENAME(MONTH, ms.MonthDate) + ' ' + FORMAT(ms.MonthDate, 'yyyy') AS DisplayPeriod,
    CAST(ISNULL(SUM(i.TotalAmount), 0) AS DECIMAL(18,2)) AS TotalRevenue
FROM MonthSeries ms
LEFT JOIN Invoices i 
  ON i.IssuedAt >= ms.MonthDate 
 AND i.IssuedAt < DATEADD(MONTH, 1, ms.MonthDate)
 AND i.Status IN ('Paid', 'PartiallyPaid')
GROUP BY ms.MonthDate
ORDER BY ms.MonthDate";
            return ExecuteQuery(query);
        }

        public DataTable GetRevenueByMonth(int year)
        {
            const string query = @"
;WITH MonthSeries AS (
    SELECT DATEFROMPARTS(@Y, n, 1) AS MonthDate
    FROM (VALUES(1),(2),(3),(4),(5),(6),(7),(8),(9),(10),(11),(12)) v(n)
)
SELECT 
    FORMAT(ms.MonthDate, 'MM/yyyy') AS PeriodKey,
    DATENAME(MONTH, ms.MonthDate) + ' ' + FORMAT(ms.MonthDate, 'yyyy') AS DisplayPeriod,
    CAST(ISNULL(SUM(i.TotalAmount), 0) AS DECIMAL(18,2)) AS TotalRevenue
FROM MonthSeries ms
LEFT JOIN Invoices i 
  ON YEAR(i.IssuedAt) = @Y 
 AND MONTH(i.IssuedAt) = MONTH(ms.MonthDate)
 AND i.Status IN ('Paid', 'PartiallyPaid')
GROUP BY ms.MonthDate
ORDER BY ms.MonthDate";
            var dt = new DataTable();
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@Y", SqlDbType.Int).Value = year;
                conn.Open();
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        public DataTable GetRevenueByCurrentWeek()
        {
            DateTime today = DateTime.Today;
            int daysSinceMonday = ((int)today.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            DateTime startOfWeek = today.AddDays(-daysSinceMonday);
            DateTime endOfWeek = startOfWeek.AddDays(6);

            const string query = @"
;WITH DaySeries AS (
    SELECT DATEADD(DAY, n, @StartDate) AS DayDate
    FROM (VALUES(0),(1),(2),(3),(4),(5),(6)) v(n)
)
SELECT 
    FORMAT(ds.DayDate, 'dd/MM/yyyy') AS PeriodKey,
    CASE DATEPART(WEEKDAY, ds.DayDate)
        WHEN 1 THEN N'Chủ nhật'
        WHEN 2 THEN N'Thứ 2' 
        WHEN 3 THEN N'Thứ 3'
        WHEN 4 THEN N'Thứ 4'
        WHEN 5 THEN N'Thứ 5'
        WHEN 6 THEN N'Thứ 6'
        WHEN 7 THEN N'Thứ 7'
    END + ' ' + FORMAT(ds.DayDate, 'dd/MM') AS DisplayPeriod,
    CAST(ISNULL(SUM(i.TotalAmount), 0) AS DECIMAL(18,2)) AS TotalRevenue
FROM DaySeries ds
LEFT JOIN Invoices i 
  ON CAST(i.IssuedAt AS DATE) = ds.DayDate
 AND i.Status IN ('Paid', 'PartiallyPaid')
WHERE ds.DayDate <= @EndDate
GROUP BY ds.DayDate
ORDER BY ds.DayDate";
            var dt = new DataTable();
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = startOfWeek;
                cmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = endOfWeek;
                conn.Open();
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        public RevenueSummary GetRevenueSummary()
        {
            const string query = @"
SELECT 
    CAST(ISNULL(SUM(RoomCharge),   0) AS DECIMAL(18,2)) AS RoomCharge,
    CAST(ISNULL(SUM(ServiceCharge),0) AS DECIMAL(18,2)) AS ServiceCharge,
    CAST(ISNULL(SUM(Discount),     0) AS DECIMAL(18,2)) AS Discount,
    CAST(ISNULL(SUM(Surcharge),    0) AS DECIMAL(18,2)) AS Surcharge,
    CAST(ISNULL(SUM(TotalAmount),  0) AS DECIMAL(18,2)) AS TotalAmount
FROM Invoices 
WHERE Status IN ('Paid', 'PartiallyPaid')
  AND IssuedAt >= DATEADD(MONTH, -6, GETDATE())";
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
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
            return new RevenueSummary();
        }

        private DataTable ExecuteQuery(string query)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        public List<Invoice> getAllInvoices()
        {
            var list = new List<Invoice>();
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand(@"
SELECT InvoiceID, BookingID, RoomCharge, ServiceCharge, Surcharge, Discount, TotalAmount, IssuedAt, IssuedBy, Status, Note
FROM Invoices", conn))
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
                            IssuedBy = rd.GetInt32(8),
                            Status = rd.GetString(9),
                            Note = rd.IsDBNull(10) ? null : rd.GetString(10)
                        });
                    }
                }
            }
            return list;
        }

        public int AddInvoice(Invoice invoice)
        {
            using (var conn = new SqlConnection(_stringConnection))
            {
                conn.Open();

                EnsureBookingExists(conn, invoice.BookingID);
                EnsureUserExists(conn, invoice.IssuedBy);
                if (InvoiceExistsForBooking(conn, invoice.BookingID))
                    throw new InvalidOperationException($"BookingID {invoice.BookingID} đã có hóa đơn. Không thể tạo trùng.");

                using (var cmd = new SqlCommand(@"
INSERT INTO Invoices
(BookingID, RoomCharge, ServiceCharge, Discount, Surcharge, TotalAmount, IssuedBy, IssuedAt, Status, Note)
OUTPUT INSERTED.InvoiceID
VALUES
(@BookingID, @RoomCharge, @ServiceCharge, @Discount, @Surcharge, @TotalAmount, @IssuedBy, @IssuedAt, @Status, @Note);", conn))
                {
                    cmd.Parameters.Add("@BookingID", SqlDbType.Int).Value = invoice.BookingID;
                    AddDec(cmd, "@RoomCharge", invoice.RoomCharge);
                    AddDec(cmd, "@ServiceCharge", invoice.ServiceCharge);
                    AddDec(cmd, "@Discount", invoice.Discount);
                    AddDec(cmd, "@Surcharge", invoice.Surcharge);
                    AddDec(cmd, "@TotalAmount", invoice.TotalAmount);
                    cmd.Parameters.Add("@IssuedBy", SqlDbType.Int).Value = invoice.IssuedBy;
                    cmd.Parameters.Add("@IssuedAt", SqlDbType.DateTime).Value =
                        invoice.IssuedAt == default(DateTime) ? DateTime.Now : invoice.IssuedAt;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = invoice.Status ?? "Unpaid";
                    cmd.Parameters.Add("@Note", SqlDbType.NVarChar, -1).Value = (object)invoice.Note ?? DBNull.Value;

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public Invoice GetInvoiceByBookingID(int bookingID)
        {
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand(@"
SELECT TOP 1 InvoiceID, BookingID, RoomCharge, ServiceCharge, Surcharge, Discount, TotalAmount, IssuedAt, IssuedBy, Status, Note
FROM Invoices WHERE BookingID = @BookingID ORDER BY InvoiceID DESC", conn))
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
                        Status = rd.GetString(9),
                        Note = rd.IsDBNull(10) ? null : rd.GetString(10)
                    };
                }
            }
        }

        public string GetPaymentByInvoice(int invoiceID)
        {
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand(
                "SELECT TOP 1 Method FROM Payments WHERE InvoiceID = @InvoiceID ORDER BY PaymentDate DESC", conn))
            {
                cmd.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = invoiceID;
                conn.Open();
                var result = cmd.ExecuteScalar();
                return result == null ? "N/A" : result.ToString();
            }
        }

        public string getFullNameByInvoiceID(int invoiceID)
        {
            using (var conn = new SqlConnection(_stringConnection))
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

        public Invoice GetInvoiceByHeaderBookingID(int headerBookingId)
        {
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand(@"
SELECT TOP 1 InvoiceID, BookingID, RoomCharge, ServiceCharge, Discount, Surcharge, TotalAmount, IssuedAt, IssuedBy, Status, Note
FROM Invoices WHERE BookingID=@id ORDER BY InvoiceID DESC", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = headerBookingId;
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
                        Discount = rd.GetDecimal(4),
                        Surcharge = rd.GetDecimal(5),
                        TotalAmount = rd.GetDecimal(6),
                        IssuedAt = rd.GetDateTime(7),
                        IssuedBy = rd.GetInt32(8),
                        Status = rd.GetString(9),
                        Note = rd.IsDBNull(10) ? null : rd.GetString(10)
                    };
                }
            }
        }

        public int UpsertInvoiceTotals(Invoice header)
        {
            using (var conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                EnsureBookingExists(conn, header.BookingID);

                var existed = GetInvoiceByHeaderBookingID(header.BookingID);
                if (existed == null)
                {
                    var issuer = header.IssuedBy > 0 ? header.IssuedBy : GetBookingCreatedBy(conn, header.BookingID);
                    if (issuer <= 0) issuer = 1;
                    EnsureUserExists(conn, issuer);

                    return AddInvoice(new Invoice
                    {
                        BookingID = header.BookingID,
                        RoomCharge = header.RoomCharge,
                        ServiceCharge = header.ServiceCharge,
                        Discount = header.Discount,
                        Surcharge = header.Surcharge,
                        TotalAmount = header.TotalAmount,
                        IssuedBy = issuer,
                        IssuedAt = header.IssuedAt == default(DateTime) ? DateTime.Now : header.IssuedAt,
                        Status = string.IsNullOrWhiteSpace(header.Status) ? "Unpaid" : header.Status,
                        Note = header.Note
                    });
                }

                using (var cmd = new SqlCommand(@"
UPDATE Invoices SET 
    RoomCharge   = @RoomCharge,
    ServiceCharge= @ServiceCharge,
    Discount     = @Discount,
    Surcharge    = @Surcharge,
    TotalAmount  = @TotalAmount
WHERE InvoiceID = @Id", conn))
                {
                    AddDec(cmd, "@RoomCharge", header.RoomCharge);
                    AddDec(cmd, "@ServiceCharge", header.ServiceCharge);
                    AddDec(cmd, "@Discount", header.Discount);
                    AddDec(cmd, "@Surcharge", header.Surcharge);
                    AddDec(cmd, "@TotalAmount", header.TotalAmount);
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = existed.InvoiceID;

                    cmd.ExecuteNonQuery();
                    return existed.InvoiceID;
                }
            }
        }

        public decimal GetPaidAmount(int invoiceId)
        {
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand(
                "SELECT ISNULL(SUM(Amount),0) FROM Payments WHERE InvoiceID=@I AND Status='Paid'", conn))
            {
                cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                conn.Open();
                var o = cmd.ExecuteScalar();
                return (o == null || o == DBNull.Value) ? 0m : Convert.ToDecimal(o);
            }
        }

        public void UpdateInvoiceStatus(int invoiceId, string status)
        {
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand("UPDATE Invoices SET Status=@s WHERE InvoiceID=@id", conn))
            {
                cmd.Parameters.Add("@s", SqlDbType.VarChar, 20).Value = status;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = invoiceId;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int CreateOrGetOpenInvoice(
            int bookingId,
            decimal roomCharge,
            decimal serviceCharge,
            decimal discount,
            decimal surcharge,
            int issuedByUserIfPaid = 0)
        {
            using (var conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                bookingId = ResolveHeaderBookingId(conn, bookingId);
                EnsureBookingExists(conn, bookingId);

                using (var findAny = new SqlCommand(
                    "SELECT TOP 1 InvoiceID FROM Invoices WITH (UPDLOCK, ROWLOCK) WHERE BookingID=@B ORDER BY InvoiceID DESC",
                    conn))
                {
                    findAny.Parameters.Add("@B", SqlDbType.Int).Value = bookingId;
                    var any = findAny.ExecuteScalar();
                    if (any != null && any != DBNull.Value)
                        return Convert.ToInt32(any);
                }

                var total = roomCharge + serviceCharge + surcharge - discount;
                if (total < 0) total = 0;

                var issuer = issuedByUserIfPaid > 0 ? issuedByUserIfPaid : GetBookingCreatedBy(conn, bookingId);
                if (issuer <= 0) issuer = 1;
                EnsureUserExists(conn, issuer);

                using (var insert = new SqlCommand(@"
INSERT INTO Invoices
(BookingID, RoomCharge, ServiceCharge, Discount, Surcharge, TotalAmount, IssuedAt, IssuedBy, Status, Note)
OUTPUT INSERTED.InvoiceID
VALUES(@B, @RC, @SC, @D, @S, @T, GETDATE(), @U, 'Unpaid', NULL);", conn))
                {
                    insert.Parameters.Add("@B", SqlDbType.Int).Value = bookingId;
                    AddDec(insert, "@RC", roomCharge);
                    AddDec(insert, "@SC", serviceCharge);
                    AddDec(insert, "@D", discount);
                    AddDec(insert, "@S", surcharge);
                    AddDec(insert, "@T", total);
                    insert.Parameters.Add("@U", SqlDbType.Int).Value = issuer;

                    return Convert.ToInt32(insert.ExecuteScalar());
                }
            }
        }

        public (decimal Total, decimal Paid, decimal Remain, string Status) GetInvoiceTotals(int invoiceId)
        {
            using (var conn = new SqlConnection(_stringConnection))
            {
                conn.Open();

                decimal total;
                string statusFromDb;
                using (var cmd = new SqlCommand("SELECT TotalAmount, Status FROM Invoices WHERE InvoiceID=@I", conn))
                {
                    cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (!rd.Read()) throw new Exception("Invoice không tồn tại.");
                        total = rd.GetDecimal(0);
                        statusFromDb = rd.GetString(1);
                    }
                }

                decimal paid;
                using (var cmd = new SqlCommand(
                    "SELECT ISNULL(SUM(Amount),0) FROM Payments WHERE InvoiceID=@I AND Status='Paid'", conn))
                {
                    cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                    paid = Convert.ToDecimal(cmd.ExecuteScalar() ?? 0m);
                }

                var remain = Math.Max(0, total - paid);
                var status = (paid <= 0m) ? "Unpaid" : (remain > 0m ? "PartiallyPaid" : "Paid");
                return (total, paid, remain, status);
            }
        }

        public void UpdateInvoiceStatusIfNeeded(int invoiceId, int issuedByUserIdIfPaid = 0)
        {
            using (var conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                decimal total = 0m, paid = 0m;

                using (var cmd = new SqlCommand(@"
SELECT i.TotalAmount, ISNULL(SUM(CASE WHEN p.Status='Paid' THEN p.Amount ELSE 0 END),0)
FROM Invoices i
LEFT JOIN Payments p ON p.InvoiceID = i.InvoiceID
WHERE i.InvoiceID=@I
GROUP BY i.TotalAmount", conn))
                {
                    cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (!rd.Read()) return;
                        total = rd.GetDecimal(0);
                        paid = rd.GetDecimal(1);
                    }
                }

                var remain = total - paid;
                var status = (paid <= 0m) ? "Unpaid" : (remain > 0m ? "PartiallyPaid" : "Paid");

                var sql = (status == "Paid")
                    ? "UPDATE Invoices SET Status=@S, IssuedAt=GETDATE(), IssuedBy=@U WHERE InvoiceID=@I"
                    : "UPDATE Invoices SET Status=@S WHERE InvoiceID=@I";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@S", SqlDbType.VarChar, 20).Value = status;
                    cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                    if (status == "Paid")
                        cmd.Parameters.Add("@U", SqlDbType.Int).Value = issuedByUserIdIfPaid;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Payment> GetPaymentsByInvoiceId(int invoiceId)
        {
            var list = new List<Payment>();
            using (var conn = new SqlConnection(_stringConnection))
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
    c.FullName AS CustomerName, c.IDNumber AS CustomerIDNumber
FROM Invoices i
JOIN Bookings b  ON b.BookingID = i.BookingID
JOIN Customers c ON c.CustomerID = b.CustomerID
ORDER BY i.InvoiceID DESC;";

            var list = new List<InvoiceListItem>();
            using (var conn = new SqlConnection(_stringConnection))
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
                            IssuedBy = rd.GetInt32(8),
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
    c.FullName AS CustomerName, c.IDNumber AS CustomerIDNumber
FROM Customers c
JOIN Bookings b  ON b.CustomerID = c.CustomerID
JOIN Invoices i  ON i.BookingID = b.BookingID
WHERE c.IDNumber = @ID
ORDER BY i.InvoiceID DESC;";

            var list = new List<InvoiceListItem>();
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@ID", SqlDbType.NVarChar, 50).Value = (object)idNumber ?? DBNull.Value;
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
                            IssuedBy = rd.GetInt32(8),
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
        private static int ResolveHeaderBookingId(SqlConnection conn, int maybeHeaderOrLineId)
        {
            using (var c1 = new SqlCommand("SELECT COUNT(1) FROM Bookings WHERE BookingID=@id", conn))
            {
                c1.Parameters.Add("@id", SqlDbType.Int).Value = maybeHeaderOrLineId;
                if (Convert.ToInt32(c1.ExecuteScalar() ?? 0) > 0) return maybeHeaderOrLineId;
            }
            using (var c2 = new SqlCommand("SELECT BookingID FROM BookingRooms WHERE BookingRoomID=@id", conn))
            {
                c2.Parameters.Add("@id", SqlDbType.Int).Value = maybeHeaderOrLineId;
                var o = c2.ExecuteScalar();
                if (o != null && o != DBNull.Value) return Convert.ToInt32(o);
            }
            throw new InvalidOperationException($"ID {maybeHeaderOrLineId} không tồn tại ở Bookings/BookingRooms.");
        }
        public InvoiceListItem GetInvoiceWithCustomerByInvoiceId(int invoiceId)
        {
            const string sql = @"
SELECT 
    i.InvoiceID, i.BookingID, i.RoomCharge, i.ServiceCharge, i.Surcharge, i.Discount, i.TotalAmount,
    i.IssuedAt, i.IssuedBy, i.Status, i.Note,
    c.FullName AS CustomerName, c.IDNumber AS CustomerIDNumber
FROM Invoices i
JOIN Bookings b  ON b.BookingID = i.BookingID
JOIN Customers c ON c.CustomerID = b.CustomerID
WHERE i.InvoiceID = @I;";

            using (var conn = new SqlConnection(_stringConnection))
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
                        IssuedBy = rd.GetInt32(8),
                        Status = rd.IsDBNull(9) ? "" : rd.GetString(9),
                        Note = rd.IsDBNull(10) ? null : rd.GetString(10),
                        CustomerName = rd.IsDBNull(11) ? "" : rd.GetString(11),
                        CustomerIDNumber = rd.IsDBNull(12) ? "" : rd.GetString(12),
                    };
                }
            }
        }
    }
}
