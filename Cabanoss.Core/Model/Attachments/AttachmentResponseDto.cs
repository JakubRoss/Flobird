namespace Cabanoss.Core.Model.Attachments
{
    public class AttachmentResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Path { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
    }
}
