using Cabanoss.Core.Common;

namespace Cabanoss.Core.Data.Entities
{
    public class List : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Position {get; set;}
        public DateTime? Deadline {get;set;}
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //Navigation
        public int BoardId { get; set; }
        public virtual Board Board { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
