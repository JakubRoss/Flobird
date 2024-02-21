namespace Domain.Data.Entities
{
    public class Comment
    {
        /// <summary>
        /// Ogólnie rzecz biorąc, zastosowanie new do właściwości Id może być interpretowane
        /// jako sposób na ukrycie tej właściwości, aby była ona dostępna tylko
        /// wewnętrznie, np. dla potrzeb EF i operacji bazodanowych, a nie dla użytkownika klasy. 
        /// </summary>
        public new int Id { get; set; }
        public string Text { get; set; } = default!;    
        public DateTime? CreatedAt { get; set; }

        //navigation
        public int UserId { get; set; }
        public  User User { get; set; } = null!;
        public int CardId { get; set; }
        public  Card Card { get; set; } = null!;
    }
}