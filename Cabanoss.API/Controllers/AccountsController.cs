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
        /// <summary>
        /// Logging in
        /// </summary>
        /// <returns>authorization token</returns>
        /// <remarks>
        /// POST cabanoss.azurewebsites.net/accounts/login
        /// </remarks>
        [HttpPost("login")]
        public async System.Threading.Tasks.Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var token = await _userService.LogIn(userLoginDto);
            
            return Ok(token);
        }

        /// <summary>
        /// Create new account
        /// </summary>
        /// <param name="user">Request's payload</param>
        /// <remarks>
        /// POST cabanoss.azurewebsites.net/accounts/register
        /// </remarks>
        [HttpPost("register")]
        public async System.Threading.Tasks.Task register([FromBody] CreateUserDto user)
        {
             await _userService.AddUserAsync(user);
        }
    }
}
