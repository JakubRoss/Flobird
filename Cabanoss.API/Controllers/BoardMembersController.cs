using Cabanoss.Core.Model.Board;
using Cabanoss.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cabanoss.API.Controllers
{
    [Route("api/boards/{boardId}/members")]
    [ApiController]
    [Authorize]
    public class BoardMembersController : Controller
    {
        private IBoardService _boardService;

        public BoardMembersController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet]
        public async Task<List<ResponseBoardUser>> GetBoardUsers([FromRoute] int boardId)
        {
            var users = await _boardService.GetUsersAsync(boardId, User);
            return users;
        }

        [HttpPost("{userId}")]
        public async Task AddBoardUsers([FromRoute] int boardId, int userId)
        {
            await _boardService.AddUsersAsync(boardId, userId, User);
        }

        [HttpDelete("{userId}")]
        public async Task RemoveBoardUsers([FromRoute] int boardId, int userId)
        {
            await _boardService.RemoveUserAsync(boardId, userId, User);
        }
        [HttpPatch("{userId}")]
        public async Task SetUserRole([FromRoute] int boardId, int userId, [FromBody] int userRole)
        {
            await _boardService.SetUserRole(boardId, userId, userRole, User);
        }
    }
}
