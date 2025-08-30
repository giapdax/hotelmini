using System;
using System.Configuration;
using System.Windows.Forms;

namespace HOTEL_MINI.Common
{
    public static class ConfigHelper
    {
        public static string GetConnectionString(string name = "HotelMiniContext")
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings[name];

                if (connectionString == null || string.IsNullOrEmpty(connectionString.ConnectionString))
                {
                    MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu. Vui lòng kiểm tra cấu hình.",
                                  "Lỗi kết nối",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                    throw new ConfigurationErrorsException($"Connection string '{name}' không tồn tại hoặc trống trong file cấu hình.");
                }

                return connectionString.ConnectionString;
            }
            catch (ConfigurationErrorsException ex)
            {
                MessageBox.Show($"Lỗi cấu hình: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}