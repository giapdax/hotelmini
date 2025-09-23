// File: HOTEL_MINI/Program.cs
using HOTEL_MINI.Forms;
using HOTEL_MINI.DAL;
using System;
using System.Windows.Forms;

namespace HOTEL_MINI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // (Tuỳ chọn) Bắt lỗi UI thread chung
            Application.ThreadException += (s, e) =>
            {
                MessageBox.Show($"Lỗi không xử lý: {e.Exception.Message}",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            try
            {
                // Seed tài khoản admin nếu chưa có
                var repo = new UserRepository();
                var user = repo.GetUserById(1);
                {
                    repo.CreateAdminUserIfNotExist();
                }

                // Mở form đăng nhập
                Application.Run(new frmLogin());

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Khởi động ứng dụng thất bại: {ex.Message}",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
