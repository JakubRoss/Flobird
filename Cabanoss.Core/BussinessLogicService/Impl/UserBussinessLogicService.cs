using AutoMapper;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Exceptions;
using Cabanoss.Core.Model.User;
using Cabanoss.Core.Repositories;

namespace Cabanoss.Core.BussinessLogicService.Impl
{
    public class UserBussinessLogicService : IUserBussinessLogicService
    {
        private IUserBaseRepository _userBase;
        private IMapper _mapper;

        public UserBussinessLogicService(IUserBaseRepository userBase, IMapper mapper)
        {
            _userBase = userBase;
            _mapper = mapper;
        }
        private async System.Threading.Tasks.Task<User> GetUser(string login)
        {
            var user = await _userBase.GetFirstAsync(u => u.Login == login);
            if (user == null)
                throw new ResourceNotFoundException();
            return user;
        }

        public async System.Threading.Tasks.Task AddUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userBase.AddAsync(user);
        }
        public async System.Threading.Tasks.Task<UserDto> GetUserAsync(string login)
        {
            var user = await GetUser(login);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        public async System.Threading.Tasks.Task<UserDto> UpdateUserAsync(string login,UserDto userDto)
        {
            var user = await GetUser(login);

            user.Login=userDto.Login;
            user.PasswordHash = userDto.PasswordHash;
            user.Email = userDto.Email;
            user.UpdatedAt = DateTime.Now;

            var updated = await _userBase.UpdateAsync(user);
            var updatedDto = _mapper.Map<UserDto>(updated);
            return updatedDto;
        }
        public async System.Threading.Tasks.Task RemoveUserAsync(string login)
        {
            var user = await GetUser(login);
            await _userBase.DeleteAsync(user);
        }
        public async System.Threading.Tasks.Task<List<UserDto>> GetUsersAsync()
        {
            var users = await _userBase.GetAllAsync();
            if(users is null)
                throw new ResourceNotFoundException();
            var usersDto = new List<UserDto>();
            foreach (var user in users)
            {
                var userDto = _mapper.Map<UserDto>(user);
                usersDto.Add(userDto);
            }
            return usersDto;
        }
    }
}
