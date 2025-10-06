using HOTEL_MINI.Common;
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HOTEL_MINI.BLL
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepo;
        private readonly BookingRoomRepository _lineRepo;
        private readonly RoomPricingRepository _pricingRepo;
        private readonly BookingRoomServiceRepository _brsRepo;
        private readonly ServicesRepository _servicesRepo;

        public BookingService()
        {
            _bookingRepo = new BookingRepository();
            _lineRepo = new BookingRoomRepository();
            _pricingRepo = new RoomPricingRepository();
            _brsRepo = new BookingRoomServiceRepository();
            _servicesRepo = new ServicesRepository();
        }

        private static void Ensure(bool cond, string msg)
        {
            if (!cond) throw new ArgumentException(msg);
        }

   
        public List<BookingDisplay> GetBookingDisplaysByCustomerNumber(string idNumber)
        {
            Ensure(!string.IsNullOrWhiteSpace(idNumber), "Thiếu CCCD/ID.");
            return _bookingRepo.GetBookingDisplaysByCustomerNumber(idNumber.Trim());
        }

  

        public BookingRoom GetBookingById(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _lineRepo.GetBookingById(bookingRoomId);
        }

        public int? GetHeaderIdByBookingRoomId(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _lineRepo.GetHeaderIdByBookingRoomId(bookingRoomId);
        }

        public Customer GetCustomerByHeaderId(int headerBookingId)
        {
            Ensure(headerBookingId > 0, "Header BookingID không hợp lệ.");
            return _bookingRepo.GetCustomerByHeaderId(headerBookingId);
        }

        public List<UsedServiceDto> GetUsedServicesByBookingID(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _brsRepo.GetUsedServicesByBookingRoomId(bookingRoomId);
        }

        public List<string> GetPaymentMethods() => _bookingRepo.GetPaymentMethods();
        public List<string> getPaymentMethods() => GetPaymentMethods();

        public bool CheckInBookingRoom(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            var line = _lineRepo.GetBookingById(bookingRoomId);
            if (line == null) throw new ArgumentException("Không tìm thấy booking line.");
            if (!string.Equals(line.Status, "Booked", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Chỉ nhận phòng khi line đang 'Booked'.");
            return _lineRepo.CheckInBooking(bookingRoomId, DateTime.Now);
        }

        public bool CancelBooking(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _lineRepo.CancelBooking(bookingRoomId);
        }


        public bool SetLinesCheckedOutByHeader(int headerBookingId, DateTime? checkoutAt = null)
        {
            Ensure(headerBookingId > 0, "Header BookingID không hợp lệ.");
            return _lineRepo.SetLinesCheckedOutByHeader(headerBookingId, checkoutAt ?? DateTime.Now);
        }

        public decimal GetRoomCharge(BookingRoom line)
        {
            Ensure(line != null, "Line rỗng.");
            Ensure(line.PricingID > 0, "PricingID không hợp lệ.");
            Ensure(line.CheckInDate.HasValue, "Thiếu Check-in.");

            var pricing = _pricingRepo.GetPricingTypeById(line.PricingID);
            Ensure(pricing != null, "Không thấy thông tin đơn giá.");

            if (pricing.PricingType.Equals("Hourly", StringComparison.OrdinalIgnoreCase))
            {
                Ensure(line.CheckOutDate.HasValue, "Thiếu Check-out để tính giờ.");
                var hours = (line.CheckOutDate.Value - line.CheckInDate.Value).TotalHours;
                if (hours <= 0) return 0m;
                return (decimal)Math.Ceiling(hours) * pricing.Price;
            }

            return pricing.Price;
        }

        private static Dictionary<int, int> GroupServiceNeeds(DataTable usedServices)
        {
            var dict = new Dictionary<int, int>();
            if (usedServices == null || usedServices.Rows.Count == 0) return dict;
            foreach (DataRow r in usedServices.Rows)
            {
                var sid = Convert.ToInt32(r["ServiceID"]);
                var qty = Convert.ToInt32(r["Quantity"]);
                if (qty <= 0) continue;
                if (dict.ContainsKey(sid)) dict[sid] += qty; else dict[sid] = qty;
            }
            return dict;
        }

        private bool TryReserveAllOrRollback(SqlConnection conn, SqlTransaction tran, Dictionary<int, int> groupedNeeds)
        {
            var reserved = new List<KeyValuePair<int, int>>();
            foreach (var kv in groupedNeeds.OrderBy(k => k.Key))
            {
                if (!_servicesRepo.TryReserveStock(conn, tran, kv.Key, kv.Value))
                {
                    for (int i = reserved.Count - 1; i >= 0; i--)
                        _servicesRepo.ReleaseStock(conn, tran, reserved[i].Key, reserved[i].Value);
                    return false;
                }
                reserved.Add(kv);
            }
            return true;
        }

        private void UpsertServices(DataTable usedServices, Dictionary<int, int> roomToLine, SqlConnection conn, SqlTransaction tran)
        {
            const string sql =
                @"MERGE BookingRoomServices AS target
                  USING (SELECT @BR AS BookingRoomID, @SID AS ServiceID) AS src
                  ON target.BookingRoomID = src.BookingRoomID AND target.ServiceID = src.ServiceID
                  WHEN MATCHED THEN UPDATE SET Quantity = target.Quantity + @Q
                  WHEN NOT MATCHED THEN INSERT (BookingRoomID, ServiceID, Quantity) VALUES (@BR, @SID, @Q);";

            foreach (DataRow row in usedServices.Rows)
            {
                var roomId = Convert.ToInt32(row["RoomID"]);
                int bookingRoomId;
                if (!roomToLine.TryGetValue(roomId, out bookingRoomId)) continue;

                var serviceId = Convert.ToInt32(row["ServiceID"]);
                var qty = Convert.ToInt32(row["Quantity"]);

                using (var cmd = new SqlCommand(sql, conn, tran))
                {
                    cmd.Parameters.Add("@BR", SqlDbType.Int).Value = bookingRoomId;
                    cmd.Parameters.Add("@SID", SqlDbType.Int).Value = serviceId;
                    cmd.Parameters.Add("@Q", SqlDbType.Int).Value = qty;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Dictionary<int, int> AddBookingGroup(int customerId, int createdBy, List<BookingRoom> roomBookings, DataTable usedServices = null)
        {
            Ensure(customerId > 0, "CustomerID không hợp lệ.");
            Ensure(createdBy > 0, "UserID không hợp lệ.");
            Ensure(roomBookings != null && roomBookings.Count > 0, "Chưa có phòng để đặt.");

            foreach (var b in roomBookings)
            {
                Ensure(b.RoomID > 0, "RoomID không hợp lệ.");
                Ensure(b.PricingID > 0, "PricingID không hợp lệ.");
                Ensure(b.CheckInDate.HasValue && b.CheckOutDate.HasValue, "Thiếu thời gian Check-in/out.");
                if (b.CheckOutDate.Value <= b.CheckInDate.Value) throw new ArgumentException("Check-out phải lớn hơn Check-in.");
                if (_lineRepo.IsRoomOverlapped(b.RoomID, b.CheckInDate.Value, b.CheckOutDate.Value))
                    throw new InvalidOperationException("Phòng " + b.RoomID + " đã có lịch trong khoảng thời gian chọn.");
            }

            using (var conn = new SqlConnection(ConfigHelper.GetConnectionString()))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        var notesHeader = roomBookings.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.Note))?.Note ?? string.Empty;
                        var headerId = _bookingRepo.AddHeader(conn, tran, customerId, createdBy, DateTime.Now, "CheckedIn", notesHeader);

                        var normalized = roomBookings.Select(b => new BookingRoom
                        {
                            RoomID = b.RoomID,
                            PricingID = b.PricingID,
                            CheckInDate = b.CheckInDate,
                            CheckOutDate = b.CheckOutDate,
                            Status = string.IsNullOrWhiteSpace(b.Status) ? "Booked" : b.Status,
                            Note = b.Note
                        }).ToList();

                        var mapRoomToLine = _lineRepo.AddLinesForHeader(conn, tran, headerId, normalized);

                        if (usedServices != null && usedServices.Rows.Count > 0)
                        {
                            var needs = GroupServiceNeeds(usedServices);
                            if (needs.Count > 0)
                            {
                                var ok = TryReserveAllOrRollback(conn, tran, needs);
                                if (!ok) throw new InvalidOperationException("Không đủ tồn kho dịch vụ.");
                                UpsertServices(usedServices, mapRoomToLine, conn, tran);
                            }
                        }

                        tran.Commit();
                        return mapRoomToLine;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<BookingFlatDisplay> GetAllBookingFlatDisplays() => _bookingRepo.GetAllBookingFlatDisplays();

        public BookingRoom GetBookingRoomById(int bookingRoomId)
    => _bookingRepo.GetBookingById(bookingRoomId);
    }
}
