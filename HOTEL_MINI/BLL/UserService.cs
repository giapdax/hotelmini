using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.BLL
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService()
        {
            _userRepository = new UserRepository();
        }
        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
        public bool AddUser(User user, string password)
        {
            if(string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = hashedPassword;
            return _userRepository.AddUser(user);
        }
        public bool DeleteUser(int userId)
        {
            return _userRepository.DeleteUser(userId);
        }
        public bool UpdateUser(User user, string newPassword = null)
        {
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                user.PasswordHash = hashedPassword;
            }
            return _userRepository.UpdateUser(user);
        }
    }
}
