namespace Application.Model.Attachments
{
    public class AttachmentDto
    {
        public AttachmentDto(string path)
        {
            Path = path;
        }

        public string? Name { get; set; }
        public string Path { get; set; }
    }
}
