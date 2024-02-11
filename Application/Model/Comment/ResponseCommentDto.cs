namespace Application.Model.Comment
{
    public class ResponseCommentDto
    {
        public ResponseCommentDto(int id, int userId, string author, string text)
        {
            Id = id;
            UserId = userId;
            Author = author;
            Text = text;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
