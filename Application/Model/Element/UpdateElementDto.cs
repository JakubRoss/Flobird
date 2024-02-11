namespace Application.Model.Element
{
    public class UpdateElementDto
    {
        public UpdateElementDto(string description)
        {
            Description = description;
        }

        public string Description { get; set; }
        public bool IsComplete { get; set; }
    }
}
