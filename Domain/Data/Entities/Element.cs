namespace Domain.Data.Entities
{
    public class Element : BaseEntity
    {
        /// <summary>
        /// Ogólnie rzecz biorąc, zastosowanie new do właściwości Id może być interpretowane
        /// jako sposób na ukrycie tej właściwości, aby była ona dostępna tylko
        /// wewnętrznie, np. dla potrzeb EF i operacji bazodanowych, a nie dla użytkownika klasy. 
        /// </summary>
        public new int Id { get; set; }
        public string Description { get; set; } = default!;
        public bool IsComplete { get; set; }
        public DateTime? CreatedAt { get; set; }

        //Navigation
        public int TaskId { get; set; }
        public Tasks Task { get; set; } = null!;

        public ICollection<ElementUsers> ElementUsers { get; set; } = new List<ElementUsers>();

    }
}
