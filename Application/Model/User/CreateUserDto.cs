namespace Application.Model.User
{
    public class CreateUserDto
    {
        public CreateUserDto(string login, string password, string confirmPassword)
        {
            Login = login;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public string Login { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
