using Cabanoss.Core.Model.Board;

namespace Cabanoss.Core.Service
{
    public interface IBoardService
    {
        Task CreateBoardAsync(int id, CreateBoardDto createBoardDto);
        Task<List<ResponseBoardDto>> GetBoardsAsync(int id);
        Task<List<ResponseBoardUser>> GetUsersAsync(int id);
        Task DeleteBoardAsync(int id);
        Task AddUsersAsync(int boardId, int userId);
        Task ModifyNameBoardAsync(int id, UpdateBoardDto updateBoardDto);
        Task RemoveUserAsync(int boardId, int userId);
    }
}