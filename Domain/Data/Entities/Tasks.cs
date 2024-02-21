namespace Domain.Data.Entities
{
    public class Tasks
    {
        /// <summary>
        /// Ogólnie rzecz biorąc, zastosowanie new do właściwości Id może być interpretowane
        /// jako sposób na ukrycie tej właściwości, aby była ona dostępna tylko
        /// wewnętrznie, np. dla potrzeb EF i operacji bazodanowych, a nie dla użytkownika klasy. 
        /// </summary>
        public new int Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }

        //Navigation
        public int CardId { get; set; }
        public Card Card { get; set; } = null!;

        public ICollection<Element> Elements { get; set; } = new List<Element>();
    }
}