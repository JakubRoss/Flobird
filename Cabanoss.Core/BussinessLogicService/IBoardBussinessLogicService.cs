using Cabanoss.Core.Model.Board;

namespace Cabanoss.Core.BussinessLogicService
{
    public interface IBoardBussinessLogicService
    {
        Task CreateBoardAsync(string login, CreateBoardDto createBoardDto);
    }
}