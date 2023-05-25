namespace Cabanoss.Core.Model.User
{
    public class UserDto
    {
        public string Login { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; }
        public string? AvatarPath { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
