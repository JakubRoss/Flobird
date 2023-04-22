using Cabanoss.Core.Model.Board;
using Cabanoss.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cabanoss.API.Controllers
{
    [Route("api/members")]
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
        public async Task<List<ResponseBoardUser>> GetBoardUsers([FromQuery] int boardId)
        {
            var users = await _boardService.GetUsersAsync(boardId, User);
            return users;
        }

        [HttpPost("boards/{boardId}")]
        public async Task AddBoardUsers([FromRoute] int boardId,[FromQuery] int userId)
        {
            await _boardService.AddUsersAsync(boardId, userId, User);
        }

        [HttpDelete("boards/{boardId}")]
        public async Task RemoveBoardUsers([FromRoute] int boardId,[FromQuery] int userId)
        {
            await _boardService.RemoveUserAsync(boardId, userId, User);
        }
        [HttpPatch("boards/{boardId}")]
        public async Task SetUserRole([FromRoute] int boardId, [FromQuery] int userId, [FromBody] int userRole)
        {
            await _boardService.SetUserRole(boardId, userId, userRole, User);
        }
    }
}
