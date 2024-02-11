namespace Application.Model.Board
{
    public class UpdateBoardDto
    {
        public UpdateBoardDto(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
