using Cabanoss.Core.Common;

namespace Cabanoss.Core.Data.Entities
{
    public class Task : BaseEentity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //Navigation
        public int CardId { get; set; }
        public virtual Card Card { get; set; }

        public virtual ICollection<TaskElement> TaskElements { get; set; }
    }
}