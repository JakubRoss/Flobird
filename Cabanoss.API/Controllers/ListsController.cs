using Cabanoss.Core.Model.List;
using Cabanoss.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cabanoss.API.Controllers
{
    [Route("api/boards/{boardId}/lists")]
    [ApiController]
    [Authorize]
    public class ListsController : ControllerBase
    {
        private IListService _listService;

        public ListsController(IListService listService)
        {
            _listService = listService;
        }

        [HttpGet]
        public async Task<List<ListDto>> GetLists([FromRoute] int boardId)
        {
            var lists = await _listService.GetAllAsync(boardId, User);
            return lists;
        }

        [HttpPost]
        public async Task CreateList([FromRoute]int boardId, [FromBody] CreateListDto createList)
        {
            await _listService.CreateListAsync(boardId, createList.Name, User);
        }

        [HttpGet("{listId}")]
        public async Task<ListDto> GetList([FromRoute] int boardId, int listId)
        {
            return await _listService.GetListAsync(listId, boardId, User);
        }

        [HttpPut("{listId}")]
        public async Task ModifyList([FromRoute] int boardId, int listId , [FromBody] CreateListDto createList)
        {
            await _listService.ModList(listId,boardId,createList.Name,User);
        }

        [HttpPost("{listId}")]
        public async Task SetDeadline([FromRoute] int boardId, int listId, [FromBody] DateOnly date)
        {
            await _listService.SetDeadline(listId , boardId , date , User);
        }
    }
}
