using Cabanoss.Core.Common;

namespace Cabanoss.Core.Data.Entities
{
    public class BoardUserTaskElement : BaseEentity
    {
        public int BoardUserId { get; set; }
        public BoardUser BoardUser { get; set; }

        public int TaskElementId { get; set; }
        public TaskElement TaskElement { get; set; }
    }
}
