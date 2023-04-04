using Cabanoss.Core.Common;

namespace Cabanoss.Core.Data.Entities
{
    public class BoardUser : BaseEentity
    {
        public string Role { get; set; }
        public virtual Roles Roles { get; set; }

        //Navigation
        public int BoardId { get; set; }
        public virtual Board Board { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        //public virtual ICollection<BoardUserTaskElement> TaskElements { get; set; }
    }
}