using Cabanoss.Core.Common;

namespace Cabanoss.Core.Data.Entities
{
    public class ElementUsers : BaseEntity
    {
        public int BoardUserId { get; set; }
        public virtual BoardUser BoardUser { get; set; }

        public int ElementId { get; set; }
        public virtual Element Element { get; set; }
    }
}
