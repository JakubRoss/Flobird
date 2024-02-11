namespace Application.Common
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public int JwtExpiredays { get; set; }
        public string JwtIssuer { get; set; }
    }
}
