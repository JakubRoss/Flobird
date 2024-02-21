namespace Domain.Data.Entities
{
    public class CardUser
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int CardId { get; set; }
        public Card Card { get; set; } = null!;
    }
}
