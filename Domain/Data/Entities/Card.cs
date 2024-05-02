namespace Domain.Data.Entities
{
    public class Card
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        //Navigation
        public int ListId { get; set; }
        public List List { get; set; } = null!;

        public ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
        public ICollection<CardUser> CardUsers { get; set; } = new List<CardUser>();
    }
}