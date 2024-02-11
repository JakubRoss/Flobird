namespace Application.Model.Attachments
{
    public class AttachmentResponseDto
    {
        public AttachmentResponseDto(string path)
        {
            Path = path;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string Path { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
    }
}
