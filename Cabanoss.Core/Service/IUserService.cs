using Cabanoss.Core.Common;
using Cabanoss.Core.Model.User;
using System.Security.Claims;

namespace Cabanoss.Core.Service
{
    public interface IUserService
    {
        System.Threading.Tasks.Task AddUserAsync(CreateUserDto user);
        System.Threading.Tasks.Task<UserDto> GetUserAsync(ClaimsPrincipal claims);
        System.Threading.Tasks.Task<UserDto> UpdateUserAsync(ClaimsPrincipal claims, UpdateUserDto user);
        System.Threading.Tasks.Task RemoveUserAsync(ClaimsPrincipal claims);
        System.Threading.Tasks.Task<List<ResponseUserDto>> GetUsersAsync(string searchingPhrase);
        System.Threading.Tasks.Task<LoginResult> LogIn(UserLoginDto userLoginDto);
    }
}