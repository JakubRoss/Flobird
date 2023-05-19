using Cabanoss.Core.Model.Board;
using Cabanoss.Core.Model.User;
using Cabanoss.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cabanoss.API.Controllers
{
    [Route("members")]
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

        /// <summary>
        /// downloads users of a certain board
        /// </summary>
        /// <param name="boardId">board id</param>
        /// <remarks>
        /// GET cabanoss.azurewebsites.net/members/boards?boardId={id}
        /// </remarks>
        [HttpGet("boards")]
        public async Task<List<ResponseBoardUser>> GetBoardUsers([FromQuery] int boardId)
        {
            var users = await _boardService.GetBoardUsersAsync(boardId, User);
            return users;
        }

        /// <summary>
        /// adds the specified user to the given board
        /// </summary>
        /// <param name="boardId">board id</param>
        /// <param name="userId">user id</param>
        /// <remarks>
        /// POST cabanoss.azurewebsites.net/members/boards/{boardId}?userId={userId}
        /// </remarks>
        [HttpPost("boards/{boardId}")]
        public async Task AddBoardUsers([FromRoute] int boardId,[FromQuery] int userId)
        {
            await _boardService.AddUsersAsync(boardId, userId, User);
        }

        /// <summary>
        /// removes the specified user to the given board
        /// </summary>
        /// <param name="boardId">board id</param>
        /// <param name="userId">user id</param>
        /// <remarks>
        /// DELETE cabanoss.azurewebsites.net/members/{boardId}?userId={userId}
        /// </remarks>
        [HttpDelete("boards/{boardId}")]
        public async Task RemoveBoardUsers([FromRoute] int boardId,[FromQuery] int userId)
        {
            await _boardService.RemoveUserAsync(boardId, userId, User);
        }

        /// <summary>
        /// sets the user role in the given board
        /// </summary>
        /// <param name="boardId">board id</param>
        /// <param name="userId">user id</param>
        /// <param name="userRole">user role [0 - Admin, 1 - User]</param>
        /// <remarks>
        /// PATCH cabanoss.azurewebsites.net/members/boards/{boardId}?userId={userId}
        /// </remarks>
        [HttpPatch("boards/{boardId}")]
        public async Task SetUserRole([FromRoute] int boardId, [FromQuery] int userId, [FromBody] int userRole)
        {
            await _boardService.SetUserRole(boardId, userId, userRole, User);
        }

        /// <summary>
        /// retrieves users assigned to a specific task element
        /// </summary>
        /// <param name="elementId">element id</param>
        /// <remarks>
        /// GET cabanoss.azurewebsites.net/members/elements?elementId={id}
        /// </remarks>
        [HttpGet("elements")]
        public async Task<List<ResponseUserDto>> GetElementUsers([FromQuery] int elementId)
        {
           return await _elementService.GetElementUsers(elementId, User);
        }

        /// <summary>
        /// adds a user to a specific task element
        /// </summary>
        /// <param name="elementId">element id</param>
        /// <param name="userId">user id</param>
        /// <remarks>
        /// POST cabanoss.azurewebsites.net/members/elements/{elementId}?userId={id}
        /// </remarks>
        [HttpPost("elements/{elementId}")]
        public async Task AddUserToElement([FromRoute] int elementId, [FromQuery] int userId)
        {
            await _elementService.AddUserToElement(elementId, userId, User);
        }

        /// <summary>
        /// removes the user to the specified task element
        /// </summary>
        /// <param name="elementId">element id</param>
        /// <param name="userId">user id</param>
        /// <remarks>
        /// DELETE cabanoss.azurewebsites.net/members/elements/{elementId}?userId={id}
        /// </remarks>
        [HttpDelete("elements/{elementId}")]
        public async Task DeleteUserFromElement ([FromRoute] int elementId, [FromQuery] int userId)
        {
            await _elementService.DeleteUserFromElement(elementId, userId, User);
        }
    }
}
