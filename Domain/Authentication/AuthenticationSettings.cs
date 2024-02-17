namespace Domain.Authentication
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; } = null!;
        public int JwtExpireDays { get; set; } = default!;
        public string JwtIssuer { get; set; } = null!;
    }
}
