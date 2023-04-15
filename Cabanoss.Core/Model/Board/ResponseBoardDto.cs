using Cabanoss.Core.Model.User;

namespace Cabanoss.Core.Model.Board
{
    public class ResponseBoardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
