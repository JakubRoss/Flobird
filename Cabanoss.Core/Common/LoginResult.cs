using Cabanoss.Core.Model.User;

namespace Cabanoss.Core.Common
{
    public class LoginResult
    {
        public string Token { get; set; }
        public ResponseUserDto User { get; set; }
    }
}
