namespace Domain.Data.Entities
{
    public class Board : BaseEntity
    {
        /// <summary>
        /// Ogólnie rzecz biorąc, zastosowanie new do właściwości Id może być interpretowane
        /// jako sposób na ukrycie tej właściwości, aby była ona dostępna tylko
        /// wewnętrznie, np. dla potrzeb EF i operacji bazodanowych, a nie dla użytkownika klasy. 
        /// </summary>
        public new int Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //Navigation
        public ICollection<BoardUser> BoardUsers { get; set; } = new List<BoardUser>();
        public ICollection<List> Lists { get; set; } = new List<List>();
    }
}
