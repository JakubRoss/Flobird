using Cabanoss.Core.Model.List;
using System.Security.Claims;

namespace Cabanoss.Core.Service
{
    public interface IListService
    {
        Task<List<ListDto>> GetAllAsync(int boardId, ClaimsPrincipal claims);
        Task CreateListAsync(int boardId, string name, ClaimsPrincipal claims);
        Task<ListDto> GetListAsync(int listId, int boardId, ClaimsPrincipal claims);
        Task ModList(int listId, int boardId, string name, ClaimsPrincipal claims);
        Task SetDeadline(int listid, int boardId, DateOnly date, ClaimsPrincipal claims);
    }
}