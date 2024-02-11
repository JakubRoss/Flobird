using API.Swagger;
using Application.Model.User;
using Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("accounts")]
    [ApiController]
    [SwaggerControllerOrder(0)]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Create new account
        /// </summary>
        /// <param name="user">Request's payload</param>
        /// <remarks>
        /// POST cabanoss.azurewebsites.net/accounts/register
        /// </remarks>
        [HttpPost("register")]
        public async System.Threading.Tasks.Task Register([FromBody] CreateUserDto user)
        {
            await _userService.AddUserAsync(user);
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
    }
}
