using Cabanoss.Core.Model.User;
using Microsoft.AspNetCore.Mvc;
using Cabanoss.Core.Service;

namespace Cabanoss.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IUserService _userBussinessLogicService;

        public AccountsController(IUserService userBussinessLogicService)
        {
            _userBussinessLogicService = userBussinessLogicService;
        }
        [HttpPost("login")]
        public async System.Threading.Tasks.Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var token = await _userBussinessLogicService.GenerateJwt(userLoginDto);
            
            return Ok(token);
        }

        [HttpPost("registger")]
        public async System.Threading.Tasks.Task register([FromBody] CreateUserDto user)
        {
             await _userBussinessLogicService.AddUserAsync(user);
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var usersDto = await _userBussinessLogicService.GetUsersAsync();
            return usersDto;
        }

        //wyszukiwanie userow trzeba dodac ?z paginacja stron?

    }
}
