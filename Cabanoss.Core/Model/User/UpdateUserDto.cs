using System.ComponentModel.DataAnnotations;

namespace Cabanoss.Core.Model.User
{
    public class UpdateUserDto
    {
        public string? Login { get; set; }
        public string? Email { get; set; }
        [MinLength(6)]
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
