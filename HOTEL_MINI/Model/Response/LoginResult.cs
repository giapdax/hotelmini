using HOTEL_MINI.Model.Entity;

namespace HOTEL_MINI.Model.Response
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
    }
}