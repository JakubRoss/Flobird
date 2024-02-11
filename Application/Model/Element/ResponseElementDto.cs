namespace Application.Model.Element
{
    public class ResponseElementDto
    {
        public ResponseElementDto(string description)
        {
            Description = description;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
    }
}
