namespace Application.Model.Board
{
    public class ResponseBoardUser
    {
        public ResponseBoardUser(string login)
        {
            Login = login;
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string? Email { get; set; }
        public string? AvatarPath { get; set; }
        public bool? IsAdmin { get; set; }
    }
}
