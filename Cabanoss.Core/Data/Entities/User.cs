using Cabanoss.Core.Common;

namespace Cabanoss.Core.Data.Entities
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? AvatarPath { get; set; }

        //Navigation
        public virtual Workspace Workspace { get; set; }

        public virtual ICollection<BoardUser> BoardUsers { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
