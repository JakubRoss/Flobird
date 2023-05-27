namespace Cabanoss.Core.Data.Entities
{
    public class ElementUsers : BaseEntity
    {
        public int UserId_eu { get; set; }
        public virtual User User { get; set; }

        public int ElementId_eu { get; set; }
        public virtual Element Element { get; set; }
    }
}
