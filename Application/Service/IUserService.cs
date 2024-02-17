using Application.Common;
using Application.Model.User;

namespace Application.Service
{
    public interface IUserService
    {
        Task AddUserAsync(CreateUserDto user);
        Task<UserDto> GetUserAsync();
        Task<UserDto> UpdateUserAsync(   UpdateUserDto user);
        Task RemoveUserAsync();
        Task<List<ResponseUserDto>> GetUsersAsync(string? searchingPhrase);
        Task<LoginResult> LogIn(UserLoginDto userLoginDto);
    }
}