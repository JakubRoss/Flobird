namespace Application.Data.Entities
{
    public class Workspace : BaseEntity
    {
        public Workspace(string name)
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
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public virtual ICollection<Board> Boards { get; set; } = new List<Board>();
    }
}
