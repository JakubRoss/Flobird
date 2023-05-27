namespace Cabanoss.Core.Data.Entities
{
    public class Attachment : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Path { get; set; }
        public DateTime DateCreated { get; set; } 

        //Navigation
        public int CardId { get; set; }
        public Card Card { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
