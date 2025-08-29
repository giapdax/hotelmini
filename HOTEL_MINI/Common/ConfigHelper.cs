using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HOTEL_MINI.Common
{
    public static class ConfigHelper
    {
        public static string GetConnectionString(string name = "HotelMiniContext")
        {
            var conn = ConfigurationManager.ConnectionStrings[name];
            if(conn == null)
            {
                MessageBox.Show("Khong ket noi duoc toi database");
                throw new Exception($"Connection string {name} is not found in App.config");
            }
            return conn.ConnectionString;
        }
    }
}
