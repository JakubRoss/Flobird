namespace Domain.Data.Entities
{
    public class User : BaseEntity
    {
        public User(string login, string passwordHash)
        {
            Login = login;
            PasswordHash = passwordHash;
        }

        /// <summary>
        /// Ogólnie rzecz biorąc, zastosowanie new do właściwości Id może być interpretowane
        /// jako sposób na ukrycie tej właściwości, aby była ona dostępna tylko
        /// wewnętrznie, np. dla potrzeb EF i operacji bazodanowych, a nie dla użytkownika klasy. 
        /// </summary>
        public new int Id { get; set; }
        public string Login { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? AvatarPath { get; set; }

        //Navigation

        public virtual ICollection<BoardUser> BoardUsers { get; set; } = new List<BoardUser>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
        public virtual ICollection<CardUser> CardUsers { get; set; } = new List<CardUser>();
        public virtual ICollection<ElementUsers> ElementUsers { get; set; } = new List<ElementUsers>();
    }
}
