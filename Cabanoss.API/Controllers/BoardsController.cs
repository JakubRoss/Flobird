using Cabanoss.Core.Common;
using Cabanoss.Core.Model.Board;
using Cabanoss.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cabanoss.API.Controllers
{
    [Route("api/users/w")]
    [ApiController]
    [Authorize]
    public class BoardsController : ControllerBase
    {
        private IBoardService _boardService;

        public BoardsController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpPost("boards")]
        public async Task PostBoard([FromBody] CreateBoardDto createBoardDto)
        {
            await _boardService.CreateBoardAsync(createBoardDto , User);
        }

        [HttpGet("boards")]
        public async Task<List<ResponseBoardDto>> GetBoards()
        {
            var boards = await _boardService.GetBoardsAsync(User);
            return boards;
        }

        [HttpPut("boards={boardId}")]
        public async Task UpdateBoardName([FromBody] UpdateBoardDto updateBoard , [FromRoute] int boardId)
        {
            await _boardService.ModifyNameBoardAsync(boardId, updateBoard, User);
        }

        [HttpDelete("boards={boardId}")]
        public async Task DeleteBoard([FromRoute] int boardId)
        {
            await _boardService.DeleteBoardAsync(boardId, User);
        }

        [HttpGet("boards={boardId}&users")]
        public async Task<List<ResponseBoardUser>> GetBoardUsers([FromRoute] int boardId)
        {
            var users = await _boardService.GetUsersAsync(boardId, User);
            return users;
        }

        [HttpPost("boards={boardId}&users={userId}")]
        public async Task AddBoardUsers([FromRoute] int boardId, int userId)
        {
            await _boardService.AddUsersAsync(boardId, userId, User);
        }

        [HttpDelete("boards={boardId}&users={userId}")]
        public async Task RemoveBoardUsers([FromRoute] int boardId, int userId)
        {
            await _boardService.RemoveUserAsync(boardId, userId, User);
        }
        [HttpPatch("boards={boardId}&users={userId}")]
        public async Task SetUserRole([FromRoute] int boardId, int userId,[FromBody]int userRole)
        {
            await _boardService.SetUserRole(boardId, userId, userRole, User);
        }

        //dodac Get specific board?
    }
}
