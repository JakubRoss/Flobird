using Cabanoss.Core.Common;

namespace Cabanoss.Core.Data.Entities
{
    public class CardUser : BaseEntity
    {
        public int UserId_cu { get; set; }
        public virtual User User { get; set; }

        public int CardId_cu { get; set; }
        public virtual Card Card { get; set; }
    }
}
