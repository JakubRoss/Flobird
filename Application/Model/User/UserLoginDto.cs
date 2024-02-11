namespace Application.Model.User
{
    public class UserLoginDto
    {
        public UserLoginDto(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; set; }
        public string Password { get; set; }
    }
}
