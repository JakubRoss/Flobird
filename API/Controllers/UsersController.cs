using API.Swagger;
using Application.Model.User;
using Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// we're setting main route of controller by [route] attribute 
    /// </summary>
    [Route("users")]
    [ApiController]
    [Authorize]
    [SwaggerControllerOrder(1)]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="userService"></param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// updates account details
        /// </summary>
        /// <param name="user">Request's payload</param>
        /// <remarks>
        /// PUT flobird.azurewebsites.net/users
        /// </remarks>
        [HttpPut]
        public async Task<UserDto> PutUser([FromBody] UpdateUserDto user)
        {
            var updatedUser = await _userService.UpdateUserAsync(user);
            return updatedUser;
        }

        /// <summary>
        /// downloads account details
        /// </summary>
        /// <remarks>
        /// GET flobird.azurewebsites.net/users
        /// </remarks>
        [HttpGet]
        public async Task<UserDto> GetUser()
        {
            var userDto =await _userService.GetUserAsync();
            return userDto;
        }

        /// <summary>
        /// downloads all users of the application
        /// </summary>
        /// /// <param name="searchingPhrase">phrase search in login or email if null returns all users</param>
        /// <remarks>
        /// GET flobird.azurewebsites.net/users/all?searchingPhrase={phrase}
        /// </remarks>
        [HttpGet("all")]
        public async Task<List<ResponseUserDto>> GetUsersAsync([FromQuery]string? searchingPhrase)
        {
            return !string.IsNullOrEmpty(searchingPhrase) ? await _userService.GetUsersAsync(searchingPhrase) : new List<ResponseUserDto>();
        }

        /// <summary>
        /// deletes account
        /// </summary>
        /// <remarks>
        /// DELETE flobird.azurewebsites.net/users
        /// </remarks>
        [HttpDelete]
        public async Task DeleteUser()
        {
            await _userService.RemoveUserAsync();
        }
    }
}
