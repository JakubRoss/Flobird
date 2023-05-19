using Cabanoss.Core.Model.User;
using Cabanoss.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cabanoss.API.Controllers
{
    [Route("users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// downloads account details
        /// </summary>
        /// <remarks>
        /// GET cabanoss.azurewebsites.net/users
        /// </remarks>
        [HttpGet]
        public async Task<UserDto> GetUser()
        {
            var userDto =await _userService.GetUserAsync(User);
            return userDto;
        }

        /// <summary>
        /// updates account details
        /// </summary>
        /// <param name="user">Request's payload</param>
        /// <remarks>
        /// PUT cabanoss.azurewebsites.net/users
        /// </remarks>
        [HttpPut]
        public async System.Threading.Tasks.Task<UserDto> PutUser([FromBody] UpdateUserDto user)
        {
            var updatedUser = await _userService.UpdateUserAsync(User, user);
            return updatedUser;
        }

        /// <summary>
        /// deletes account
        /// </summary>
        /// <remarks>
        /// DELETE cabanoss.azurewebsites.net/users
        /// </remarks>
        [HttpDelete]
        public async System.Threading.Tasks.Task DeleteUser()
        {
            await _userService.RemoveUserAsync(User);
        }
    }
}
