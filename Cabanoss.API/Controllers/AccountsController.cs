using Cabanoss.Core.Model.User;
using Cabanoss.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace Cabanoss.API.Controllers
{
    [Route("accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async System.Threading.Tasks.Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var token = await _userService.LogIn(userLoginDto);
            
            return Ok(token);
        }

        [HttpPost("register")]
        public async System.Threading.Tasks.Task register([FromBody] CreateUserDto user)
        {
             await _userService.AddUserAsync(user);
        }
    }
}
