using System.Collections.Generic;
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Common;

namespace HOTEL_MINI.BLL
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public List<User> GetAllUsers() => _userRepository.GetAllUsers();
        public User GetByUsername(string username) => _userRepository.GetUserByUsername(username);
        public User GetById(int userId) => _userRepository.GetUserById(userId);

        // Admin thêm user
        public bool AddUser(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(user?.Username)) return false;

            if (!PasswordHelper.Validate(password, out var msg))
                return false;

            user.PasswordHash = PasswordHelper.Hash(password);
            return _userRepository.AddUser(user);
        }

        // Admin sửa user (có thể đổi mật khẩu)
        public bool UpdateUser(User user, string newPassword = null)
        {
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                if (!PasswordHelper.Validate(newPassword, out var _)) return false;
                user.PasswordHash = PasswordHelper.Hash(newPassword);
            }
            // nếu PasswordHash rỗng => repo sẽ không update field này
            return _userRepository.UpdateUser(user);
        }

        // Login
        public User VerifyLogin(string username, string password)
        {
            var u = _userRepository.GetUserByUsername(username);
            if (u == null) return null;
            return PasswordHelper.Verify(password, u.PasswordHash) ? u : null;
        }

        public enum ChangePasswordResult
        {
            Success,
            WrongCurrentPassword,
            NewPasswordTooWeak,
            SameAsOld,
            Error
        }

        // Đổi mật khẩu (dành cho FrmChangePass)
        public ChangePasswordResult ChangePassword(int userId, string currentPassword, string newPassword)
        {
            var u = _userRepository.GetUserById(userId);
            if (u == null) return ChangePasswordResult.Error;

            if (!PasswordHelper.Validate(newPassword, out var _))
                return ChangePasswordResult.NewPasswordTooWeak;

            if (!PasswordHelper.Verify(currentPassword, u.PasswordHash))
                return ChangePasswordResult.WrongCurrentPassword;

            // Không cho trùng y hệt (theo plain text)
            if (PasswordHelper.Verify(newPassword, u.PasswordHash))
                return ChangePasswordResult.SameAsOld;

            var newHash = PasswordHelper.Hash(newPassword);
            var ok = _userRepository.UpdatePassword(userId, newHash);
            if (!ok) return ChangePasswordResult.Error;

            // XÁC NHẬN từ DB
            var after = _userRepository.GetUserById(userId);
            if (after == null || !PasswordHelper.Verify(newPassword, after.PasswordHash))
                return ChangePasswordResult.Error;

            return ChangePasswordResult.Success;
        }

        public bool DeleteUser(int userId) => _userRepository.DeleteUser(userId);

        public void CreateAdminUserIfNotExist() => _userRepository.CreateAdminUserIfNotExist();
    }
}
