namespace Application.Model.Task
{
    public class TaskDto
    {
        public TaskDto(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
