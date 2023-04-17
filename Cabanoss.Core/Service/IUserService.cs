using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Model.User;

namespace Cabanoss.Core.Service
{
    public interface IUserService
    {
        System.Threading.Tasks.Task<User> GetUserById(int id);
        System.Threading.Tasks.Task AddUserAsync(CreateUserDto user);
        System.Threading.Tasks.Task<UserDto> GetUserAsync(int id);
        System.Threading.Tasks.Task<UserDto> UpdateUserAsync(int id, UpdateUserDto user);
        System.Threading.Tasks.Task RemoveUserAsync(int id);
        System.Threading.Tasks.Task<List<UserDto>> GetUsersAsync();
        System.Threading.Tasks.Task<string> GenerateJwt(UserLoginDto userLoginDto);
    }
}