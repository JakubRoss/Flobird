using Cabanoss.Core.Model.List;
using Cabanoss.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cabanoss.API.Controllers
{
    [Route("lists")]
    [ApiController]
    [Authorize]
    public class ListsController : ControllerBase
    {
        private IListService _listService;

        public ListsController(IListService listService)
        {
            _listService = listService;
        }

        [HttpGet("boards")]
        public async Task<List<ListDto>> GetLists([FromQuery] int boardId)
        {
            var lists = await _listService.GetAllAsync(boardId, User);
            return lists;
        }

        [HttpPost]
        public async Task CreateList([FromQuery]int boardId, [FromBody] CreateListDto createList)
        {
            await _listService.CreateListAsync(boardId, createList.Name, User);
        }

        [HttpGet]
        public async Task<ListDto> GetList([FromQuery]int listId)
        {
            return await _listService.GetListAsync(listId, User);
        }

        [HttpPut]
        public async Task UpdateList([FromQuery] int listId , [FromBody] CreateListDto createList)
        {
            await _listService.UpdateList(listId, createList.Name ,User);
        }

        [HttpPatch]
        public async Task SetDeadline([FromQuery] int listId, [FromBody] DateOnly date)
        {
            await _listService.SetDeadline(listId , date , User);
        }
        [HttpDelete]
        public async Task DeleteList([FromQuery] int listId)
        {
            await _listService.DeleteList(listId , User);
        }
    }
}
