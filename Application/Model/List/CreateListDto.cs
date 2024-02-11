namespace Application.Model.List
{
    public class CreateListDto
    {
        public CreateListDto(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
