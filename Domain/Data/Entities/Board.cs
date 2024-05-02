namespace Domain.Data.Entities
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //Navigation
        public ICollection<BoardUser> BoardUsers { get; set; } = new List<BoardUser>();
        public ICollection<List> Lists { get; set; } = new List<List>();
    }
}
