namespace Application.Data.Entities
{
    public class Element : BaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public DateTime? CreatedAt { get; set; }

        //Navigation
        public int TaskId { get; set; }
        public virtual Tasks Task { get; set; }

        public virtual ICollection<ElementUsers>? ElementUsers { get; set; }

    }
}
