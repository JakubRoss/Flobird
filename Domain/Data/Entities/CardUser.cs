namespace Domain.Data.Entities
{
    public class CardUser : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public int CardId { get; set; }
        public virtual Card Card { get; set; } = null!;
    }
}
