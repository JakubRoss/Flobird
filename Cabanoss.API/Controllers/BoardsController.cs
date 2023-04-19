using Cabanoss.Core.Model.Board;
using Cabanoss.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cabanoss.API.Controllers
{
    [Route("api/boards")]
    [ApiController]
    [Authorize]
    public class BoardsController : ControllerBase
    {
        private IBoardService _boardService;

        public BoardsController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpPost]
        public async Task PostBoard([FromBody] CreateBoardDto createBoardDto)
        {
            await _boardService.CreateBoardAsync(createBoardDto , User);
        }

        [HttpGet]
        public async Task<List<ResponseBoardDto>> GetBoards()
        {
            var boards = await _boardService.GetBoardsAsync(User);
            return boards;
        }

        [HttpPut("{boardId}")]
        public async Task UpdateBoardName([FromBody] UpdateBoardDto updateBoard , [FromRoute] int boardId)
        {
            await _boardService.ModifyNameBoardAsync(boardId, updateBoard, User);
        }

        [HttpDelete("{boardId}")]
        public async Task DeleteBoard([FromRoute] int boardId)
        {
            await _boardService.DeleteBoardAsync(boardId, User);
        }

        //dodac Get specific board?
    }
}
