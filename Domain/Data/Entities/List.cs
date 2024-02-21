namespace Domain.Data.Entities
{
    public class List
    {
        /// <summary>
        /// Ogólnie rzecz biorąc, zastosowanie new do właściwości Id może być interpretowane
        /// jako sposób na ukrycie tej właściwości, aby była ona dostępna tylko
        /// wewnętrznie, np. dla potrzeb EF i operacji bazodanowych, a nie dla użytkownika klasy. 
        /// </summary>
        public new int Id { get; set; }
        public string Name { get; set; } = default!;
        public int? Position {get; set;}
        public DateTime? Deadline {get;set;}
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //Navigation
        public int BoardId { get; set; }
        public Board Board { get; set; } = null!;

        public ICollection<Card> Cards { get; set; } = new List<Card>();
    }
}
