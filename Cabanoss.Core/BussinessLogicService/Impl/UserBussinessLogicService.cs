using AutoMapper;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Exceptions;
using Cabanoss.Core.Model.User;
using Cabanoss.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Cabanoss.Core.BussinessLogicService.Impl
{
    public class UserBussinessLogicService : IUserBussinessLogicService
    {
        private IUserBaseRepository _userBase;
        private IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IWorkspaceBussinessLogicService _workspaceBussiness;

        public UserBussinessLogicService(IUserBaseRepository userBase
            ,IMapper mapper
            ,IWorkspaceBussinessLogicService workspaceBussiness
            ,IPasswordHasher<User>passwordHasher)
        {
            _userBase = userBase;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _workspaceBussiness = workspaceBussiness;
        }
        private async System.Threading.Tasks.Task<User> GetUser(string login)
        {
            var LowLogin = login.ToLower();
            var user = await _userBase.GetFirstAsync(u => u.Login.ToLower() == LowLogin);
            if (user == null)
                throw new ResourceNotFoundException("Uzytkownik nie istnieje");
            return user;
        }
        public async System.Threading.Tasks.Task AddUserAsync(CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var hashedPassword = _passwordHasher.HashPassword(user, userDto.Password);
            user.PasswordHash = hashedPassword;
            user.CreatedAt = DateTime.Now;
            await _userBase.AddAsync(user);

            await _workspaceBussiness.AddWorkspaceAsync(user.Login);
        }
        public async System.Threading.Tasks.Task<UserDto> GetUserAsync(string login)
        {
            var user = await GetUser(login);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        public async System.Threading.Tasks.Task<UserDto> UpdateUserAsync(string login, UpdateUserDto userDto)
        {
            var user = await GetUser(login);
            #region updt_properties
            if (userDto.Login!=null)
                user.Login = userDto.Login;
            if (userDto.Password != null)
                user.PasswordHash = userDto.Password;
            if (userDto.Email != null)
                user.Email = userDto.Email;
            user.UpdatedAt = DateTime.Now;
            #endregion

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
