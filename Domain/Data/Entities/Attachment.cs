namespace Domain.Data.Entities
{
    public class Attachment
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Path { get; set; } = default!;
        public DateTime DateCreated { get; set; } 

        //Navigation
        public int CardId { get; set; }
        public virtual Card Card { get; set; } = null!;
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
