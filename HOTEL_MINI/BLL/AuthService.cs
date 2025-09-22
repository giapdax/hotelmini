using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;

namespace HOTEL_MINI.BLL
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;

        public AuthService()
        {
            _userRepository = new UserRepository();
        }

        public LoginResult Login(string username, string password)
        {
            try
            {
                username = username?.Trim();
                password = password?.Trim();

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    return new LoginResult
                    {
                        Success = false,
                        Message = "Tên đăng nhập và mật khẩu không được để trống"
                    };
                }

                var user = _userRepository.GetUserByUsername(username);
                if (user == null)
                {
                    return new LoginResult
                    {
                        Success = false,
                        Message = "Tên đăng nhập không tồn tại"
                    };
                }

                if (user.Status == UserStatus.Inactive || user.Status == UserStatus.Blocked)
                {
                    return new LoginResult
                    {
                        Success = false,
                        Message = "Tài khoản của bạn đã bị vô hiệu hóa hoặc bị khóa."
                    };
                }

                if (string.IsNullOrEmpty(user.PasswordHash) || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    return new LoginResult
                    {
                        Success = false,
                        Message = "Mật khẩu không đúng"
                    };
                }

                return new LoginResult
                {
                    Success = true,
                    Message = "Đăng nhập thành công",
                    User = user
                };
            }
            catch (Exception ex)
            {
                return new LoginResult
                {
                    Success = false,
                    Message = $"Lỗi hệ thống: {ex.Message}"
                };
            }
        }
    }
}
