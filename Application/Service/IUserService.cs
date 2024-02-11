using Application.Common;
using Application.Model.User;

namespace Application.Service
{
    public interface IUserService
    {
        System.Threading.Tasks.Task AddUserAsync(CreateUserDto user);
        System.Threading.Tasks.Task<UserDto> GetUserAsync();
        System.Threading.Tasks.Task<UserDto> UpdateUserAsync(   UpdateUserDto user);
        System.Threading.Tasks.Task RemoveUserAsync();
        System.Threading.Tasks.Task<List<ResponseUserDto>> GetUsersAsync(string searchingPhrase);
        System.Threading.Tasks.Task<LoginResult> LogIn(UserLoginDto userLoginDto);
    }
}