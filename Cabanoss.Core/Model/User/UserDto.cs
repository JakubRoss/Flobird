namespace Cabanoss.Core.Model.User
{
    public class UserDto
    {
        public string Login { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
