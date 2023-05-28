namespace Cabanoss.Core.Data.Entities
{
    public class CardUser : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int CardId { get; set; }
        public virtual Card Card { get; set; }
    }
}
