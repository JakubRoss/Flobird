using Cabanoss.Core.Model.Board;
using Cabanoss.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cabanoss.API.Controllers
{
    [Route("boards")]
    [ApiController]
    [Authorize]
    public class BoardsController : ControllerBase
    {
        private IBoardService _boardService;

        public BoardsController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        /// <summary>
        /// Create new board
        /// </summary>
        /// <param name="createBoardDto">Request's payload</param>
        /// <remarks>
        /// POST cabanoss.azurewebsites.net/boards
        /// </remarks>
        [HttpPost]
        public async Task PostBoard([FromBody] CreateBoardDto createBoardDto)
        {
            await _boardService.CreateBoardAsync(createBoardDto , User);
        }

        /// <summary>
        /// Downloads list of boards
        /// </summary>
        /// <returns>
        /// Returns a list of boards 
        /// </returns>
        /// <remarks>
        /// GET cabanoss.azurewebsites.net/boards
        /// </remarks>
        [HttpGet]
        public async Task<List<ResponseBoardDto>> GetBoards()
        {
            var boards = await _boardService.GetBoardsAsync(User);
            return boards;
        }

        /// <summary>
        /// Renames the board
        /// </summary>
        /// <param name="updateBoard">Request's payload</param>
        /// <param name="boardId">Board id</param>
        /// <remarks>
        /// PUT cabanoss.azurewebsites.net/boards?boardId={id}
        /// </remarks>
        [HttpPut]
        public async Task UpdateBoardName([FromBody] UpdateBoardDto updateBoard , [FromQuery] int boardId)
        {
            await _boardService.UpdateBoardAsync(boardId, updateBoard, User);
        }

        /// <summary>
        /// Removes tables
        /// </summary>
        /// <param name="boardId">Board id</param>
        /// <remarks>
        /// DELETE cabanoss.azurewebsites.net/boards?boardId={id}
        /// </remarks>
        [HttpDelete]
        public async Task DeleteBoard([FromQuery] int boardId)
        {
            await _boardService.DeleteBoardAsync(boardId, User);
        }

    }
}
