namespace Application.Model.Comment
{
    public class ResponseCommentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
