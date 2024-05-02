namespace Domain.Data.Entities
{
    public class List
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int? Position {get; set;}
        public DateTime? Deadline {get;set;}
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //Navigation
        public int BoardId { get; set; }
        public Board Board { get; set; } = null!;

        public ICollection<Card> Cards { get; set; } = new List<Card>();
    }
}
