namespace Domain.Authentication
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; } = null!;
        public int JwtExpireTime { get; set; } = default!;
        public string JwtIssuer { get; set; } = null!;
        public string JwtAudience { get; set; } = null!;
    }
}
