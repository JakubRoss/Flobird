using Cabanoss.Core.Model.List;
using System.Security.Claims;

namespace Cabanoss.Core.Service
{
    public interface IListService
    {
        Task<List<ListDto>> GetAllAsync(int boardId, ClaimsPrincipal claims);
        Task CreateListAsync(int boardId, string name, ClaimsPrincipal claims);
        Task<ListDto> GetListAsync(int listId, ClaimsPrincipal claims);
        Task UpdateList(int listId, string name, ClaimsPrincipal claims);
        Task SetDeadline(int listid, DateOnly date, ClaimsPrincipal claims);
        Task DeleteList(int listId, ClaimsPrincipal user);
    }
}