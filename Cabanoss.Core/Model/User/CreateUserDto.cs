using System.ComponentModel.DataAnnotations;

namespace Cabanoss.Core.Model.User
{
    public class CreateUserDto
    {
        [Required]
        public string Login { get; set; }
        public string Email { get; set; }
        [MinLength(6)]
        [Required]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
