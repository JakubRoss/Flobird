namespace Cabanoss.Core.Model.Board
{
    public class ResponseBoardUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string? Email { get; set; }
        public bool? IsAdmin { get; set; }
    }
}
