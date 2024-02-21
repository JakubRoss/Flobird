namespace Domain.Data.Entities
{
    public class Card
    {
        /// <summary>
        /// Ogólnie rzecz biorąc, zastosowanie new do właściwości Id może być interpretowane
        /// jako sposób na ukrycie tej właściwości, aby była ona dostępna tylko
        /// wewnętrznie, np. dla potrzeb EF i operacji bazodanowych, a nie dla użytkownika klasy. 
        /// </summary>
        public new int Id { get; set; }

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