namespace Application.Data.Entities
{
    public class Board : BaseEntity
    {
        public Board(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Ogólnie rzecz biorąc, zastosowanie new do właściwości Id może być interpretowane
        /// jako sposób na ukrycie tej właściwości, aby była ona dostępna tylko
        /// wewnętrznie, np. dla potrzeb EF i operacji bazodanowych, a nie dla użytkownika klasy. 
        /// </summary>
        public new int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //Navigation
        public int WorkspaceId { get; set; }
        public virtual Workspace Workspace { get; set; } = null!;

        public virtual ICollection<BoardUser> BoardUsers { get; set; } = new List<BoardUser>();
        public virtual ICollection<List> Lists { get; set; } = new List<List>();
    }
}
