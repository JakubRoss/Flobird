namespace Application.Model.Element
{
    public class ElementDto
    {
        public ElementDto(string description)
        {
            Description = description;
        }

        public string Description { get; set; }
    }
}
