namespace Application.Model.Card
{
    public class CreateCardDto
    {
        public CreateCardDto(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
