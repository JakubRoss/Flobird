namespace Application.Model.User
{
    public class ResponseUserDto
    {
        public ResponseUserDto(string login)
        {
            Login = login;
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string? Email { get; set; }
        public string? AvatarPath { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
