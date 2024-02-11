namespace Application.Data.Entities
{
    public class ElementUsers : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int ElementId { get; set; }
        public virtual Element Element { get; set; }
    }
}
