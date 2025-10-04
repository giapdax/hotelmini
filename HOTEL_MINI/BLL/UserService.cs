using HOTEL_MINI.Common;
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

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

        public User GetById(int userId) => _userRepository.GetUserById(userId);


        private static readonly Regex UsernameRegex = new Regex(@"^[A-Za-z0-9_.-]{3,50}$", RegexOptions.Compiled);
        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
        private static readonly Regex PhoneRegex = new Regex(@"^(\+?\d{9,15}|0\d{9,10})$", RegexOptions.Compiled);

        public struct ValidationResult
        {
            public bool IsValid;
            public string Message;
            public static ValidationResult Ok() => new ValidationResult { IsValid = true, Message = "" };
            public static ValidationResult Fail(string msg) => new ValidationResult { IsValid = false, Message = msg };
        }
        public ValidationResult TryValidateUser(User user, bool isUpdate, string plainPasswordIfProvided = null)
        {
            if (user == null) return ValidationResult.Fail("User không hợp lệ.");

            if (string.IsNullOrWhiteSpace(user.Username))
                return ValidationResult.Fail("Username không được để trống.");
            var existed = _userRepository.GetUserByUsername(user.Username.Trim());
            if (!isUpdate)
            {
                if (existed != null) return ValidationResult.Fail("Username đã tồn tại.");
            }
            else
            {
                if (existed != null && existed.UserID != user.UserID)
                    return ValidationResult.Fail("Username đã được người khác sử dụng.");
            }
            if (!UsernameRegex.IsMatch(user.Username.Trim()))
                return ValidationResult.Fail("Username chỉ chứa chữ/số/._- và dài 3-50 ký tự.");
            if (!string.IsNullOrWhiteSpace(plainPasswordIfProvided))
            {
                if (!PasswordHelper.Validate(plainPasswordIfProvided, out var pwMsg))
                    return ValidationResult.Fail(pwMsg ?? "Mật khẩu không đạt yêu cầu.");
            }
            else
            {
                if (!isUpdate)
                    return ValidationResult.Fail("Vui lòng nhập mật khẩu.");
            }

            if (!string.IsNullOrEmpty(user.FullName) && user.FullName.Trim().Length > 100)
                return ValidationResult.Fail("Họ tên tối đa 100 ký tự.");
            if (!string.IsNullOrWhiteSpace(user.Email) && !EmailRegex.IsMatch(user.Email.Trim()))
                return ValidationResult.Fail("Email không hợp lệ.");
            if (!string.IsNullOrWhiteSpace(user.Phone) && !PhoneRegex.IsMatch(user.Phone.Trim()))
                return ValidationResult.Fail("Số điện thoại không hợp lệ.");
            if (user.Role <= 0)
                return ValidationResult.Fail("Role không hợp lệ.");
            return ValidationResult.Ok();
        }


        public bool AddUser(User user, string password)
        {
            var vr = TryValidateUser(user, isUpdate: false, plainPasswordIfProvided: password);
            if (!vr.IsValid) return false;
            user.PasswordHash = PasswordHelper.Hash(password);
            return _userRepository.AddUser(user);
        }

        public bool UpdateUser(User user, string newPassword = null)
        {
            var vr = TryValidateUser(user, isUpdate: true, plainPasswordIfProvided: newPassword);
            if (!vr.IsValid) return false;

            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                user.PasswordHash = PasswordHelper.Hash(newPassword);
            }
            return _userRepository.UpdateUser(user);
        }

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

        public ChangePasswordResult ChangePassword(int userId, string currentPassword, string newPassword)
        {
            var u = _userRepository.GetUserById(userId);
            if (u == null) return ChangePasswordResult.Error;

            if (!PasswordHelper.Validate(newPassword, out var _))
                return ChangePasswordResult.NewPasswordTooWeak;

            if (!PasswordHelper.Verify(currentPassword, u.PasswordHash))
                return ChangePasswordResult.WrongCurrentPassword;

            if (PasswordHelper.Verify(newPassword, u.PasswordHash))
                return ChangePasswordResult.SameAsOld;

            var newHash = PasswordHelper.Hash(newPassword);
            var ok = _userRepository.UpdatePassword(userId, newHash);
            if (!ok) return ChangePasswordResult.Error;

            var after = _userRepository.GetUserById(userId);
            if (after == null || !PasswordHelper.Verify(newPassword, after.PasswordHash))
                return ChangePasswordResult.Error;

            return ChangePasswordResult.Success;
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                return _userRepository.DeleteUser(userId);
            }
            catch (InvalidOperationException ex)
            {

                throw new ArgumentException(ex.Message);
            }
            catch (SqlException ex) // phòng khi DAL không wrap
            {
                if (ex.Number == 547)
                    throw new ArgumentException("Không thể xóa user vì đang được tham chiếu ở nơi khác (đơn/phiếu/hóa đơn...).");
                throw;
            }
        }
    }
}
