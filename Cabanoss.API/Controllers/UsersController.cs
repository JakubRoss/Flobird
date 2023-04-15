using Cabanoss.Core.Service;
using Cabanoss.Core.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cabanoss.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private IUserService _userBussinessLogicService;

        public UsersController(IUserService userBussinessLogicService)
        {
            _userBussinessLogicService = userBussinessLogicService;
        }
        private int Getid()
        {
            var claims = User.Claims;
            var idClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return int.Parse(idClaim.Value);
        }

        [HttpGet]
        public async Task<UserDto> GetUser()
        {
            var userDto =await _userBussinessLogicService.GetUserAsync(Getid());
            return userDto;
        }

        [HttpPut]
        public async System.Threading.Tasks.Task<UserDto> PutUser([FromBody] UpdateUserDto user)
        {
            var updatedUser = await _userBussinessLogicService.UpdateUserAsync(Getid(), user);
            return updatedUser;
        }

        [HttpDelete]
        public async System.Threading.Tasks.Task DeleteUser()
        {
            await _userBussinessLogicService.RemoveUserAsync(Getid());
        }
    }
}
