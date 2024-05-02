namespace Domain.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; } = default!;    
        public DateTime? CreatedAt { get; set; }

        //navigation
        public int UserId { get; set; }
        public  User User { get; set; } = null!;
        public int CardId { get; set; }
        public  Card Card { get; set; } = null!;
    }
}