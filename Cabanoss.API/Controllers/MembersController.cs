using Cabanoss.Core.Model.Board;
using Cabanoss.Core.Model.User;
using Cabanoss.Core.Service;
using Cabanoss.Core.Service.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cabanoss.API.Controllers
{
    [Route("api/members")]
    [ApiController]
    [Authorize]
    public class MembersController : Controller
    {
        private IBoardService _boardService;
        private IElementService _elementService;

        public MembersController(IBoardService boardService,
            IElementService elementService)
        {
            _boardService = boardService;
            _elementService = elementService;
        }

        [HttpGet("boards")]
        public async Task<List<ResponseBoardUser>> GetBoardUsers([FromQuery] int boardId)
        {
            var users = await _boardService.GetBoardUsersAsync(boardId, User);
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
        //
        //
        [HttpGet("elements")]
        public async Task GetElementUsers([FromQuery] int elementId)
        {
            await _elementService.GetElementUsers(elementId, User);
        }

        [HttpPost("elements/{elementId}")]
        public async Task AddUserToElement([FromRoute] int elementId, [FromQuery] int userId)
        {
            await _elementService.AddUserToElement(elementId, userId, User);
        }

        [HttpDelete("elements/{elementId}")]
        public async Task DeleteUserFromElement ([FromRoute] int elementId, [FromQuery] int userId)
        {
            await _elementService.DeleteUserFromElement(elementId, userId, User);
        }
    }
}
