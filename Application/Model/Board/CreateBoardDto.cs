namespace Application.Model.Board
{
    public class CreateBoardDto
    {
        public CreateBoardDto(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
