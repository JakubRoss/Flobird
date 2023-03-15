using Cabanoss.Core.Common;

namespace Cabanoss.Core.Data.Entities
{
    public class TaskElement : BaseEentity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; } = false;

        //Navigation
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }

        public virtual ICollection<BoardUserTaskElement> BoardUsers { get; set; }
    }
}
