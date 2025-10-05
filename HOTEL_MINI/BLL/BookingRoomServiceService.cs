using HOTEL_MINI.Common;
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HOTEL_MINI.BLL
{
    /// <summary>
    /// Orchestrator cho dịch vụ theo line:
    /// - Multi-room add: reserve stock tổng -> upsert từng line -> commit.
    /// - Reduce: giảm line -> release stock -> commit.
    /// </summary>
    public class BookingRoomServiceService
    {
        private readonly BookingRoomServiceRepository _lineRepo;
        private readonly ServicesRepository _svcRepo;

        public BookingRoomServiceService()
        {
            _lineRepo = new BookingRoomServiceRepository();
            _svcRepo = new ServicesRepository();
        }

        private static void Ensure(bool ok, string msg)
        {
            if (!ok) throw new ArgumentException(msg);
        }

        private static List<int> DistinctPos(IEnumerable<int> xs)
        {
            var rs = new List<int>();
            if (xs == null) return rs;
            var set = new HashSet<int>();
            foreach (var x in xs)
                if (x > 0 && set.Add(x)) rs.Add(x);
            return rs;
        }

        // Reads
        public List<UsedServiceDto> GetUsedServicesByBookingRoomId(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _lineRepo.GetUsedServicesByBookingRoomId(bookingRoomId);
        }

        public int GetCurrentQuantity(int bookingRoomId, int serviceId)
        {
            Ensure(bookingRoomId > 0 && serviceId > 0, "Tham số không hợp lệ.");
            return _lineRepo.GetCurrentQuantity(bookingRoomId, serviceId);
        }

        public int GetAvailableServiceQuantity(int bookingRoomId, int serviceId)
        {
            Ensure(bookingRoomId > 0 && serviceId > 0, "Tham số không hợp lệ.");
            return _lineRepo.GetAvailableServiceQuantity(bookingRoomId, serviceId);
        }

        public DataTable GetServiceMenuWithStock()
            => _lineRepo.GetServiceMenuWithStock();

        // ====== Chính: Add nhiều phòng (atomic) ======
        public (int appliedRooms, int totalAdded, int remainingAfter)
            AddServiceToRooms(IEnumerable<int> bookingRoomIds, int serviceId, int qtyPerRoom)
        {
            var ids = DistinctPos(bookingRoomIds);
            Ensure(ids.Count > 0, "Chưa chọn phòng.");
            Ensure(serviceId > 0, "ServiceID không hợp lệ.");
            Ensure(qtyPerRoom > 0, "Số lượng phải > 0.");

            // Check line tồn tại
            for (int i = 0; i < ids.Count; i++)
                if (!_lineRepo.BookingRoomExists(ids[i]))
                    throw new InvalidOperationException("Không tìm thấy BookingRoomID=" + ids[i]);

            using (var conn = new SqlConnection(ConfigHelper.GetConnectionString()))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        int totalNeed = qtyPerRoom * ids.Count;

                        // 1) Trừ kho tổng
                        if (!_svcRepo.TryReserveStock(conn, tran, serviceId, totalNeed))
                            throw new InvalidOperationException("Tồn kho không đủ.");

                        // 2) Ghi line từng phòng
                        for (int i = 0; i < ids.Count; i++)
                            _lineRepo.AddOrIncrease(conn, tran, ids[i], serviceId, qtyPerRoom);

                        // 3) Commit
                        tran.Commit();

                        // 4) Lấy tồn còn lại (ngoài tran)
                        int left = _svcRepo.GetQuantity(serviceId);
                        return (ids.Count, totalNeed, left);
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        // ====== Giảm 1 phòng (release kho đúng số bớt) ======
        public int ReduceServiceQuantity(int bookingRoomId, int serviceId, int delta)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            Ensure(serviceId > 0, "ServiceID không hợp lệ.");
            Ensure(delta > 0, "Số lượng bớt phải > 0.");

            using (var conn = new SqlConnection(ConfigHelper.GetConnectionString()))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        int removed = _lineRepo.ReduceQuantity(conn, tran, bookingRoomId, serviceId, delta);
                        if (removed > 0)
                            _svcRepo.ReleaseStock(conn, tran, serviceId, removed);

                        tran.Commit();
                        return removed;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        // ====== Optional: Reduce nhiều phòng (mỗi phòng reducePerRoom) ======
        public int ReduceServiceFromRooms(IEnumerable<int> bookingRoomIds, int serviceId, int reducePerRoom)
        {
            var ids = DistinctPos(bookingRoomIds);
            Ensure(ids.Count > 0, "Chưa chọn phòng.");
            Ensure(serviceId > 0, "ServiceID không hợp lệ.");
            Ensure(reducePerRoom > 0, "Số lượng bớt phải > 0.");

            using (var conn = new SqlConnection(ConfigHelper.GetConnectionString()))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        int totalRemoved = 0;
                        for (int i = 0; i < ids.Count; i++)
                            totalRemoved += _lineRepo.ReduceQuantity(conn, tran, ids[i], serviceId, reducePerRoom);

                        if (totalRemoved > 0)
                            _svcRepo.ReleaseStock(conn, tran, serviceId, totalRemoved);

                        tran.Commit();
                        return totalRemoved;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        // ====== Set absolute qty (để tương thích) ======
        public bool SetServiceQuantityForRoom(int bookingRoomId, int serviceId, int newQty)
        {
            // Nếu bạn dùng nhiều case set tăng/giảm tuyệt đối, có thể viết phiên bản transactional để
            // reserve/release phần chênh lệch. Tạm thời giữ nguyên API cũ cho tương thích.
            Ensure(bookingRoomId > 0 && serviceId > 0, "Tham số không hợp lệ.");
            Ensure(newQty >= 0, "Số lượng không thể âm.");
            return _lineRepo.AddOrUpdateServiceForBooking(bookingRoomId, serviceId, newQty);
        }
    }
}
