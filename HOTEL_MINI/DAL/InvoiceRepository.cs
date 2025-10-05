using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Documents;

namespace HOTEL_MINI.DAL
{
    public class InvoiceRepository
    {
        private readonly string _stringConnection;

        public InvoiceRepository()
        {
            _stringConnection = ConfigHelper.GetConnectionString();
        }
        public List<RevenueRoomDTO> GetRevenueByRoom(int month, int year)
        {
            using (var connection = new SqlConnection(_stringConnection))
            {
                connection.Open();
                string sql = @"
            SELECT r.RoomNumber,
                   MONTH(i.IssuedAt) AS Month,
                   YEAR(i.IssuedAt) AS Year,
                   SUM(i.RoomCharge) AS RoomRevenue,
                   SUM(i.ServiceCharge) AS ServiceRevenue,
                   SUM(i.TotalAmount) AS TotalRevenue
            FROM Invoices i
            INNER JOIN Bookings b ON i.BookingID = b.BookingID
            INNER JOIN Rooms r ON b.RoomID = r.RoomID
            WHERE MONTH(i.IssuedAt) = @Month AND YEAR(i.IssuedAt) = @Year
            GROUP BY r.RoomNumber, MONTH(i.IssuedAt), YEAR(i.IssuedAt)
            ORDER BY r.RoomNumber;
        ";

                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@Year", year);

                    var reader = cmd.ExecuteReader();
                    var result = new List<RevenueRoomDTO>();

                    while (reader.Read())
                    {
                        result.Add(new RevenueRoomDTO
                        {
                            RoomNumber = reader["RoomNumber"].ToString(),
                            Month = (int)reader["Month"],
                            Year = (int)reader["Year"],
                            RoomRevenue = (decimal)reader["RoomRevenue"],
                            ServiceRevenue = (decimal)reader["ServiceRevenue"],
                            TotalRevenue = (decimal)reader["TotalRevenue"]
                        });
                    }

                    return result;
                }
            }
        }

        public List<Invoice> getAllInvoices()
        {
            var invoices = new List<Invoice>();
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = "SELECT * FROM Invoices";
                SqlCommand cmd = new SqlCommand(sql, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        invoices.Add(new Invoice
                        {
                            InvoiceID = Convert.ToInt32(reader["InvoiceID"]),
                            BookingID = Convert.ToInt32(reader["BookingID"]),
                            RoomCharge = Convert.ToDecimal(reader["RoomCharge"]),
                            ServiceCharge = Convert.ToDecimal(reader["ServiceCharge"]),
                            Surcharge = Convert.ToDecimal(reader["Surcharge"]),
                            Discount = Convert.ToDecimal(reader["Discount"]),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                            IssuedAt = Convert.ToDateTime(reader["IssuedAt"]),
                            IssuedBy = Convert.ToInt32(reader["IssuedBy"]),
                            Status = reader["Status"].ToString(),
                            Note = reader["Note"] == DBNull.Value ? null : reader["Note"].ToString()
                        });
                    }
                }
            }
            return invoices;
        }
        public int AddInvoice(Invoice invoice)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = @"INSERT INTO Invoices (BookingID, RoomCharge, ServiceCharge, Discount, Surcharge, TotalAmount, IssuedBy, IssuedAt, Status, Note)
                           VALUES (@BookingID, @RoomCharge, @ServiceCharge, @Discount, @Surcharge, @TotalAmount, @IssuedBy, @IssuedAt, @Status, @Note);
                           SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@BookingID", invoice.BookingID);
                cmd.Parameters.AddWithValue("@RoomCharge", invoice.RoomCharge);
                cmd.Parameters.AddWithValue("@ServiceCharge", invoice.ServiceCharge);
                cmd.Parameters.AddWithValue("@Discount", invoice.Discount);
                cmd.Parameters.AddWithValue("@Surcharge", invoice.Surcharge);
                cmd.Parameters.AddWithValue("@TotalAmount", invoice.TotalAmount);
                cmd.Parameters.AddWithValue("@IssuedBy", invoice.IssuedBy);
                cmd.Parameters.AddWithValue("@IssuedAt", invoice.IssuedAt);
                cmd.Parameters.AddWithValue("@Status", invoice.Status);
                cmd.Parameters.AddWithValue("@Note", invoice.Note ?? (object)DBNull.Value);

                var result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }
        public DataTable GetRevenueLast6Months()
        {
            string query = @"
            ;WITH Last6Months AS (
                SELECT DATEADD(MONTH, -5, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)) as StartDate
            ),
            MonthSeries AS (
                SELECT DATEADD(MONTH, n, (SELECT StartDate FROM Last6Months)) as MonthDate
                FROM (SELECT 0 UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5) numbers(n)
            )
            SELECT 
                FORMAT(ms.MonthDate, 'MM/yyyy') as PeriodKey,
                DATENAME(MONTH, ms.MonthDate) + ' ' + FORMAT(ms.MonthDate, 'yyyy') as DisplayPeriod,
                ISNULL(SUM(i.TotalAmount), 0) as TotalRevenue
            FROM MonthSeries ms
            LEFT JOIN Invoices i ON 
                i.IssuedAt >= ms.MonthDate AND 
                i.IssuedAt < DATEADD(MONTH, 1, ms.MonthDate) AND
                i.Status IN ('Paid', 'PartiallyPaid')
            GROUP BY ms.MonthDate
            ORDER BY ms.MonthDate";

            return ExecuteQuery(query);
        }
        public DataTable GetRevenueByMonth(int year)
        {
            string query = $@"
            ;WITH MonthSeries AS (
                SELECT DATEFROMPARTS({year}, n, 1) as MonthDate
                FROM (SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 
                      UNION SELECT 7 UNION SELECT 8 UNION SELECT 9 UNION SELECT 10 UNION SELECT 11 UNION SELECT 12) numbers(n)
            )
            SELECT 
                FORMAT(ms.MonthDate, 'MM/yyyy') as PeriodKey,
                DATENAME(MONTH, ms.MonthDate) + ' ' + FORMAT(ms.MonthDate, 'yyyy') as DisplayPeriod,
                ISNULL(SUM(i.TotalAmount), 0) as TotalRevenue
            FROM MonthSeries ms
            LEFT JOIN Invoices i ON 
                YEAR(i.IssuedAt) = {year} AND 
                MONTH(i.IssuedAt) = MONTH(ms.MonthDate) AND
                i.Status IN ('Paid', 'PartiallyPaid')
            GROUP BY ms.MonthDate
            ORDER BY ms.MonthDate";

            return ExecuteQuery(query);
        }
        public DataTable GetRevenueByCurrentWeek()
        {
            // Tính ngày bắt đầu và kết thúc của tuần hiện tại (Thứ 2 đến Chủ nhật)
            DateTime today = DateTime.Today;
            int daysSinceMonday = ((int)today.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            DateTime startOfWeek = today.AddDays(-daysSinceMonday);
            DateTime endOfWeek = startOfWeek.AddDays(6);

            string query = @"
        ;WITH DaySeries AS (
            SELECT DATEADD(DAY, n, @StartDate) as DayDate
            FROM (SELECT 0 UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6) numbers(n)
        )
        SELECT 
            FORMAT(ds.DayDate, 'dd/MM/yyyy') as PeriodKey,
            CASE DATEPART(WEEKDAY, ds.DayDate)
                WHEN 1 THEN N'Chủ nhật'
                WHEN 2 THEN N'Thứ 2' 
                WHEN 3 THEN N'Thứ 3'
                WHEN 4 THEN N'Thứ 4'
                WHEN 5 THEN N'Thứ 5'
                WHEN 6 THEN N'Thứ 6'
                WHEN 7 THEN N'Thứ 7'
            END + ' ' + FORMAT(ds.DayDate, 'dd/MM') as DisplayPeriod,
            ISNULL(SUM(i.TotalAmount), 0) as TotalRevenue
        FROM DaySeries ds
        LEFT JOIN Invoices i ON 
            CAST(i.IssuedAt AS DATE) = ds.DayDate AND
            i.Status IN ('Paid', 'PartiallyPaid')
        WHERE ds.DayDate <= @EndDate
        GROUP BY ds.DayDate
        ORDER BY ds.DayDate";

            DataTable dt = new DataTable();

            using (var conn = new SqlConnection(_stringConnection))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StartDate", startOfWeek);
                    cmd.Parameters.AddWithValue("@EndDate", endOfWeek);

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }
        public RevenueSummary GetRevenueSummary()
        {
            string query = @"
            SELECT 
                ISNULL(SUM(RoomCharge), 0) as RoomCharge,
                ISNULL(SUM(ServiceCharge), 0) as ServiceCharge,
                ISNULL(SUM(Discount), 0) as Discount,
                ISNULL(SUM(Surcharge), 0) as Surcharge,
                ISNULL(SUM(TotalAmount), 0) as TotalAmount
            FROM Invoices 
            WHERE Status IN ('Paid', 'PartiallyPaid') AND
                  IssuedAt >= DATEADD(MONTH, -6, GETDATE())";

            using (var conn = new SqlConnection(_stringConnection))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new RevenueSummary
                            {
                                RoomCharge = reader.GetDecimal(0),
                                ServiceCharge = reader.GetDecimal(1),
                                Discount = reader.GetDecimal(2),
                                Surcharge = reader.GetDecimal(3),
                                TotalAmount = reader.GetDecimal(4)
                            };
                        }
                    }
                }
            }

            return new RevenueSummary();
        }
        private DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();

            using (var conn = new SqlConnection(_stringConnection))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        
        public Invoice GetInvoiceByBookingID(int bookingID)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = "SELECT * FROM Invoices WHERE BookingID = @BookingID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@BookingID", bookingID);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Invoice
                        {
                            InvoiceID = Convert.ToInt32(reader["InvoiceID"]),
                            BookingID = Convert.ToInt32(reader["BookingID"]),
                            RoomCharge = Convert.ToDecimal(reader["RoomCharge"]),
                            ServiceCharge = Convert.ToDecimal(reader["ServiceCharge"]),
                            Surcharge = Convert.ToDecimal(reader["Surcharge"]),
                            Discount = Convert.ToDecimal(reader["Discount"]),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                            IssuedAt = Convert.ToDateTime(reader["IssuedAt"]),
                            IssuedBy = Convert.ToInt32(reader["IssuedBy"]),
                            Status = reader["Status"].ToString(),
                            Note = reader["Note"] == DBNull.Value ? null : reader["Note"].ToString()
                        };
                    }
                }
                return null;
            }
        }
        public string GetPaymentByInvoice(int invoiceID)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = "SELECT TOP 1 Method FROM Payments WHERE InvoiceID = @InvoiceID ORDER BY PaymentDate DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);
                var result = cmd.ExecuteScalar();
                return result?.ToString() ?? "N/A";
            }
        }
        public string getFullNameByInvoiceID(int invoiceID)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = @"SELECT u.FullName 
                               FROM Users u
                               JOIN Invoices i ON u.UserID = i.IssuedBy
                               WHERE i.InvoiceID = @InvoiceID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);
                var result = cmd.ExecuteScalar();
                return result?.ToString() ?? "N/A";
            }
        }
        public Invoice GetInvoiceByHeaderBookingID(int headerBookingId)
        {
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand("SELECT TOP 1 * FROM Invoices WHERE BookingID=@id", conn))
            {
                cmd.Parameters.AddWithValue("@id", headerBookingId);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new Invoice
                    {
                        InvoiceID = (int)rd["InvoiceID"],
                        BookingID = (int)rd["BookingID"],
                        RoomCharge = (decimal)rd["RoomCharge"],
                        ServiceCharge = (decimal)rd["ServiceCharge"],
                        Discount = (decimal)rd["Discount"],
                        Surcharge = (decimal)rd["Surcharge"],
                        TotalAmount = (decimal)rd["TotalAmount"],
                        IssuedAt = (DateTime)rd["IssuedAt"],
                        IssuedBy = (int)rd["IssuedBy"],
                        Status = rd["Status"].ToString(),
                        Note = rd["Note"] == DBNull.Value ? null : rd["Note"].ToString()
                    };
                }
            }
        }

        public int UpsertInvoiceTotals(Invoice header)
        {
            // Nếu chưa có -> tạo mới; nếu có -> cập nhật tổng.
            var existed = GetInvoiceByHeaderBookingID(header.BookingID);
            if (existed == null)
            {
                return AddInvoice(new Invoice
                {
                    BookingID = header.BookingID,
                    RoomCharge = header.RoomCharge,
                    ServiceCharge = header.ServiceCharge,
                    Discount = header.Discount,
                    Surcharge = header.Surcharge,
                    TotalAmount = header.TotalAmount,
                    IssuedBy = header.IssuedBy,
                    IssuedAt = header.IssuedAt == default ? DateTime.Now : header.IssuedAt,
                    Status = string.IsNullOrWhiteSpace(header.Status) ? "Issued" : header.Status,
                    Note = header.Note
                });
            }
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand(@"
UPDATE Invoices SET 
    RoomCharge=@RoomCharge, ServiceCharge=@ServiceCharge,
    Discount=@Discount, Surcharge=@Surcharge, TotalAmount=@TotalAmount
WHERE InvoiceID=@Id", conn))
            {
                cmd.Parameters.AddWithValue("@RoomCharge", header.RoomCharge);
                cmd.Parameters.AddWithValue("@ServiceCharge", header.ServiceCharge);
                cmd.Parameters.AddWithValue("@Discount", header.Discount);
                cmd.Parameters.AddWithValue("@Surcharge", header.Surcharge);
                cmd.Parameters.AddWithValue("@TotalAmount", header.TotalAmount);
                cmd.Parameters.AddWithValue("@Id", existed.InvoiceID);
                conn.Open(); cmd.ExecuteNonQuery();
                return existed.InvoiceID;
            }
        }

        public decimal GetPaidAmount(int invoiceId)
        {
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand(
                "SELECT ISNULL(SUM(Amount),0) FROM Payments WHERE InvoiceID=@id AND Status IN ('Completed','Success')", conn))
            {
                cmd.Parameters.AddWithValue("@id", invoiceId);
                conn.Open();
                var o = cmd.ExecuteScalar();
                return o == null || o == DBNull.Value ? 0m : Convert.ToDecimal(o);
            }
        }

        public void UpdateInvoiceStatus(int invoiceId, string status)
        {
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand("UPDATE Invoices SET Status=@s WHERE InvoiceID=@id", conn))
            {
                cmd.Parameters.AddWithValue("@s", status);
                cmd.Parameters.AddWithValue("@id", invoiceId);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

    }
}