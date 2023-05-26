using Cabanoss.Core.Common;

namespace Cabanoss.Core.Data.Entities
{
    public class CardUser : BaseEntity
    {
        public int BoardUserId { get; set; }
        public virtual BoardUser BoardUser { get; set; }

        public int CardId { get; set; }
        public virtual Card Card { get; set; }
    }
}
