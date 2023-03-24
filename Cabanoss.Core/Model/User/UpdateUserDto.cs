namespace Cabanoss.Core.Model.User
{
    public class UpdateUserDto
    {
        public string Login { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
