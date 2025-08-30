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

            try
            {
                Application.Run(new frmLogin());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Khởi động ứng dụng thất bại: {ex.Message}",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}