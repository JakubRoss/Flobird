using Application.Model.User;

namespace Application.Common
{
    public class LoginResult
    {
        public string Token { get; set; }
        public ResponseUserDto User { get; set; }
    }
}
