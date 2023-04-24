using Cabanoss.Core.Common;

namespace Cabanoss.Core.Data.Entities
{
    public class User : BaseEentity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //Navigation
        public virtual Workspace Workspace { get; set; }

        public virtual ICollection<BoardUser> BoardUsers { get; set; } = new List<BoardUser>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
