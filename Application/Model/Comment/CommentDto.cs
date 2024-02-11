namespace Application.Model.Comment
{
    public class CommentDto
    {
        public CommentDto(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}
