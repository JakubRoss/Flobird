using Cabanoss.Core.BussinessLogicService;
using Cabanoss.Core.Model.User;
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
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var usersDto =await _userBussinessLogicService.GetUsersAsync();
            return usersDto;
        }

        // GET api/<UserController>/login
        [HttpGet("{login}")]
        public async Task<UserDto> GetUser(string login)
        {
            var userDto =await _userBussinessLogicService.GetUserAsync(login);
            return userDto;
        }

        // POST api/<UserController>
        [HttpPost]
        public async System.Threading.Tasks.Task PostUser([FromBody] UserDto user)
        {
            await _userBussinessLogicService.AddUserAsync(user);
        }

        // PUT api/<UserController>/login
        [HttpPut("{login}")]
        public async System.Threading.Tasks.Task<UserDto> PutUser(string login, [FromBody] UpdateUserDto user)
        {
            var updatedUser = await _userBussinessLogicService.UpdateUserAsync(login, user);
            return updatedUser;
        }
        // DELETE api/<UserController>/login
        [HttpDelete("{login}")]
        public async System.Threading.Tasks.Task DeleteUser(string login)
        {
            await _userBussinessLogicService.RemoveUserAsync(login);
        }
    }
}
