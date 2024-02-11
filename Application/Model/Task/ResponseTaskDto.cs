namespace Application.Model.Task
{
    public class ResponseTaskDto
    {
        public ResponseTaskDto(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
