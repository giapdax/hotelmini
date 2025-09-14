using System.Text.RegularExpressions;

namespace HOTEL_MINI.Common
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Yêu cầu: >= 8 ký tự và có ít nhất 2/3 nhóm: chữ, số, ký tự đặc biệt.
        /// </summary>
        public static bool Validate(string password, out string message)
        {
            message = null;
            if (string.IsNullOrWhiteSpace(password))
            {
                message = "Mật khẩu không được để trống.";
                return false;
            }
            if (password.Length < 8)
            {
                message = "Mật khẩu phải có ít nhất 8 ký tự.";
                return false;
            }

            bool hasLetter = Regex.IsMatch(password, "[A-Za-z]");
            bool hasDigit = Regex.IsMatch(password, "\\d");
            bool hasSpec = Regex.IsMatch(password, "[^A-Za-z0-9]");

            int groups = (hasLetter ? 1 : 0) + (hasDigit ? 1 : 0) + (hasSpec ? 1 : 0);
            if (groups < 2)
            {
                message = "Mật khẩu phải có ít nhất 2 trong 3 nhóm: chữ, số, ký tự đặc biệt.";
                return false;
            }
            return true;
        }

        public static string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        public static bool Verify(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
