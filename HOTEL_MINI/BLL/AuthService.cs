using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.BLL
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        public AuthService()
        {
            _userRepository = new UserRepository();
        }
        public LoginResult login(string username, string password)
        {
            var user = _userRepository.getUserbyUsername(username);
            if (user == null)
            {
                return new LoginResult { Success = false, Message = "Username không tồn tại" };
            }
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return new LoginResult { Success = false, Message = "Password không đúng" };
            }
            return new LoginResult { Success = true, Message = "Đăng nhập thành công", User = user };
        }
    }
}
