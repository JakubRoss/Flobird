namespace Domain.Data.Entities
{
    public class Element
    {
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public bool IsComplete { get; set; }
        public DateTime? CreatedAt { get; set; }

        //Navigation
        public int TaskId { get; set; }
        public Tasks Task { get; set; } = null!;

        public ICollection<ElementUsers> ElementUsers { get; set; } = new List<ElementUsers>();

    }
}
