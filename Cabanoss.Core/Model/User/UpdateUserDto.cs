namespace Cabanoss.Core.Model.User
{
    public class UpdateUserDto
    {
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
