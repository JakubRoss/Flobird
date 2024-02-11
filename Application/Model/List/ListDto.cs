namespace Application.Model.List
{
    public class ListDto
    {
        public ListDto(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Position { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
