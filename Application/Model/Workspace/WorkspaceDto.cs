namespace Application.Model.Workspace
{
    public class WorkspaceDto
    {
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int UserId { get; set; }
    }
}
