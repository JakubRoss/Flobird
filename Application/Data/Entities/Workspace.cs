namespace Application.Data.Entities
{
    public class Workspace : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //Navigation
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Board> Boards { get; set; }
    }
}
