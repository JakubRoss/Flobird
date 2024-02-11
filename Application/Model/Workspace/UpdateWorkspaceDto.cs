namespace Application.Model.Workspace
{
    public class UpdateWorkspaceDto
    {
        public UpdateWorkspaceDto(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
