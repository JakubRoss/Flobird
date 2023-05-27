namespace Cabanoss.Core.Data.Entities
{
    public class Board : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //Navigation
        public int WorkspaceId { get; set; }
        public virtual Workspace Workspace { get; set; }

        public virtual ICollection<BoardUser> BoardUsers { get; set; }
        public virtual ICollection<List> Lists { get; set; }
    }
}
