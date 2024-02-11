using Application.Model.User;

namespace Application.Common
{
    public class LoginResult
    {
        public LoginResult(string token, ResponseUserDto user)
        {
            Token = token;
            User = user;
        }

        public string Token { get; set; }
        public ResponseUserDto User { get; set; }
    }
}
