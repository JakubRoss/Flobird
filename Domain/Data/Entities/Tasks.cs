namespace Domain.Data.Entities
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }

        //Navigation
        public int CardId { get; set; }
        public Card Card { get; set; } = null!;

        public ICollection<Element> Elements { get; set; } = new List<Element>();
    }
}