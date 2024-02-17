namespace Domain.Data.Entities
{
    public class Comment : BaseEntity
    {
        public Comment(string text)
        {
            Text = text;
        }
        /// <summary>
        /// Ogólnie rzecz biorąc, zastosowanie new do właściwości Id może być interpretowane
        /// jako sposób na ukrycie tej właściwości, aby była ona dostępna tylko
        /// wewnętrznie, np. dla potrzeb EF i operacji bazodanowych, a nie dla użytkownika klasy. 
        /// </summary>
        public new int Id { get; set; }
        public string Text { get; set; }
        public DateTime? CreatedAt { get; set; }

        //navigation
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public int CardId { get; set; }
        public virtual Card Card { get; set; } = null!;
    }
}