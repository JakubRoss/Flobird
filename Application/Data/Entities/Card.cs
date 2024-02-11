namespace Application.Data.Entities
{
    public class Card : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        //Navigation
        public int ListId { get; set; }
        public List List { get; set; }

        public virtual ICollection<Tasks> Tasks { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<CardUser> CardUsers { get; set; }
    }
}