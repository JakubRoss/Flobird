using Cabanoss.Core.Common;

namespace Cabanoss.Core.Data.Entities
{
    public class Comment : BaseEentity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        //navigation
        public int UserId { get; set; }
        public User User { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }
    }
}