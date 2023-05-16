using Cabanoss.Core.Service;
using Cabanoss.Core.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpGet]
        public async Task<UserDto> GetUser()
        {
            var userDto =await _userService.GetUserAsync(User);
            return userDto;
        }

        [HttpPut]
        public async System.Threading.Tasks.Task<UserDto> PutUser([FromBody] UpdateUserDto user)
        {
            var updatedUser = await _userService.UpdateUserAsync(User, user);
            return updatedUser;
        }

        [HttpDelete]
        public async System.Threading.Tasks.Task DeleteUser()
        {
            await _userService.RemoveUserAsync(User);
        }
    }
}
