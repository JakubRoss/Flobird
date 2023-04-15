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

        private int Getid()
        {
            var claims = User.Claims;
            var idClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return int.Parse(idClaim.Value);
        }

        [HttpPost("boards")]
        public async Task PostBoard([FromBody] CreateBoardDto createBoardDto)
        {
            await _boardService.CreateBoardAsync(Getid(), createBoardDto);
        }

        [HttpGet("boards")]
        public async Task<List<ResponseBoardDto>> GetBoards()
        {
            var boards = await _boardService.GetBoardsAsync(Getid());
            return boards;
        }

        [HttpPut("boards={id}")]
        public async Task UpdateBoardName([FromBody] UpdateBoardDto updateBoard , [FromRoute] int id)
        {
            await _boardService.ModifyNameBoardAsync(id, updateBoard);
        }

        [HttpDelete("boards={id}")]
        public async Task DeleteBoard([FromRoute] int id)
        {
            await _boardService.DeleteBoardAsync(id);
        }

        [HttpGet("boards={id}&users")]
        public async Task<List<ResponseBoardUser>> GetBoardUsers([FromRoute] int id)
        {
            var users = await _boardService.GetUsersAsync(id);
            return users;
        }

        [HttpPost("boards={boardId}&users={userId}")]
        public async Task AddBoardUsers([FromRoute] int boardId, int userId)
        {
            await _boardService.AddUsersAsync(boardId, userId);
        }

        [HttpDelete("boards={boardId}&users={userId}")]
        public async Task RemoveBoardUsers([FromRoute] int boardId, int userId)
        {
            await _boardService.RemoveUserAsync(boardId, userId);
        }
    }
}
