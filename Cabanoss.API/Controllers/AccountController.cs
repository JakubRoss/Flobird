using Cabanoss.Core.BussinessLogicService;
using Cabanoss.Core.BussinessLogicService.Impl;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Model.User;
using Microsoft.AspNetCore.Mvc;

namespace Cabanoss.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserBussinessLogicService _userBussinessLogicService;

        public AccountController(IUserBussinessLogicService userBussinessLogicService)
        {
            _userBussinessLogicService = userBussinessLogicService;
        }
        [HttpPost("login")]
        public async System.Threading.Tasks.Task<string> Login(UserLoginDto userLoginDto)
        {
            var token = await _userBussinessLogicService.GenerateJwt(userLoginDto);
            return token;
        }

        [HttpPost("registger")]
        public async System.Threading.Tasks.Task register([FromBody] CreateUserDto user)
        {
            await _userBussinessLogicService.AddUserAsync(user);
        }

    }
}
