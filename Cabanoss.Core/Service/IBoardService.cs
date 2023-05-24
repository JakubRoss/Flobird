using Cabanoss.Core.Model.Board;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Cabanoss.Core.Service
{
    public interface IBoardService
    {
        Task CreateBoardAsync(CreateBoardDto createBoardDto, ClaimsPrincipal user);
        Task<List<ResponseBoardDto>> GetBoardsAsync(ClaimsPrincipal user);
        Task<List<ResponseBoardUser>> GetBoardUsersAsync(int BoardId, ClaimsPrincipal user);
        Task DeleteBoardAsync(int boardId, ClaimsPrincipal user);
        Task AddUsersAsync(int boardId, int userId, ClaimsPrincipal user);
        Task UpdateBoardAsync(int boardId, UpdateBoardDto updateBoardDto, ClaimsPrincipal user);
        Task RemoveUserAsync(int boardId, int userId, ClaimsPrincipal user);
        Task SetUserRole(int boardId, int modifyUserId, int roles, ClaimsPrincipal user);
    }
}