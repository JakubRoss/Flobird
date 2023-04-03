using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Model.User;

namespace Cabanoss.Core.BussinessLogicService
{
    public interface IUserBussinessLogicService
    {
        System.Threading.Tasks.Task AddUserAsync(CreateUserDto user);
        System.Threading.Tasks.Task<UserDto> GetUserAsync(string login);
        System.Threading.Tasks.Task<UserDto> UpdateUserAsync(string login, UpdateUserDto user);
        System.Threading.Tasks.Task RemoveUserAsync(string login);
        System.Threading.Tasks.Task<List<UserDto>> GetUsersAsync();
        System.Threading.Tasks.Task<string> GenerateJwt(UserLoginDto userLoginDto);
    }
}