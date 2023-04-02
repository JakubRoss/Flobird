using Cabanoss.Core.BussinessLogicService;
using Cabanoss.Core.Model.Board;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cabanoss.API.Controllers
{
    [Route("api/users/{login}/WorkSpace/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private IBoardBussinessLogicService _boardBussinessLogicService;

        public BoardController(IBoardBussinessLogicService boardBussinessLogicService)
        {
            _boardBussinessLogicService = boardBussinessLogicService;
        }
        // GET: api/<TableController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<TableController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<TableController>
        [HttpPost]
        public async Task PostBoard(string login, [FromBody] CreateBoardDto createBoardDto)
        {
            await _boardBussinessLogicService.CreateBoardAsync(login, createBoardDto);
        }

        //// PUT api/<TableController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<TableController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
