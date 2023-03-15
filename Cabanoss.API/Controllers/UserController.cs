using Cabanoss.Core.BussinessLogicService;
using Cabanoss.Core.BussinessLogicService.Impl;
using Cabanoss.Core.Data.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cabanoss.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserBussinessLogicService _userBussinessLogicService;

        public UserController(IUserBussinessLogicService userBussinessLogicService)
        {
            _userBussinessLogicService = userBussinessLogicService;
        }


        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public async System.Threading.Tasks.Task Post([FromBody] User user)
        {
            await _userBussinessLogicService.AddUser(user);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
