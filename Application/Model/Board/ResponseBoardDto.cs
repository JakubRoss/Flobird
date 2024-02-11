namespace Application.Model.Board
{
    public class ResponseBoardDto
    {
        public ResponseBoardDto(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
