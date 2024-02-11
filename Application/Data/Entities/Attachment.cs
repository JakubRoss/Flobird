namespace Application.Data.Entities
{
    public class Attachment : BaseEntity
    {
        public Attachment(string path)
        {
            Path = path;
        }
        /// <summary>
        /// Ogólnie rzecz biorąc, zastosowanie new do właściwości Id może być interpretowane
        /// jako sposób na ukrycie tej właściwości, aby była ona dostępna tylko
        /// wewnętrznie, np. dla potrzeb EF i operacji bazodanowych, a nie dla użytkownika klasy. 
        /// </summary>
        public new int Id { get; set; } 
        public string? Name { get; set; }
        public string Path { get; set; }
        public DateTime DateCreated { get; set; } 

        //Navigation
        public int CardId { get; set; }
        public virtual Card Card { get; set; } = null!;
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
