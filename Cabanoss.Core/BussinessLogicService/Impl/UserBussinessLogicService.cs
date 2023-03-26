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
        private readonly IWorkspaceBussinessLogicService _workspaceBussiness;

        public UserBussinessLogicService(IUserBaseRepository userBase, IMapper mapper, IWorkspaceBussinessLogicService workspaceBussiness)
        {
            _userBase = userBase;
            _mapper = mapper;
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
        private async System.Threading.Tasks.Task IsLoginTaken(string login)
        {
            var user = await _userBase.GetFirstAsync(u => u.Login.ToLower() == login.ToLower());
            if(user != null) 
                throw new ResourceNotFoundException($"Nazwa {login} jest zajeta");  
        }


        public async System.Threading.Tasks.Task AddUserAsync(CreateUserDto userDto)
        {
            await IsLoginTaken(userDto.Login);
            if (userDto.Password != userDto.ConfirmPassword)
                throw new ResourceNotFoundException("Passwords are not equal");
            var user = _mapper.Map<User>(userDto);
            user.CreatedAt = DateTime.Now;

            await _userBase.AddAsync(user);

            var login = user.Login;
            var workspace = await _workspaceBussiness.GetUserWorkspaceAsync(login);
            if (workspace is null)
                await _workspaceBussiness.AddWorkspaceAsync(login);
        }
        public async System.Threading.Tasks.Task<UserDto> GetUserAsync(string login)
        {
            var user = await GetUser(login);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        public async System.Threading.Tasks.Task<UserDto> UpdateUserAsync(string login, UpdateUserDto userDto)
        {
            if (userDto.Password != userDto.ConfirmPassword)
                throw new ResourceNotFoundException("Passwords are not equal");

            var user = GetUser(login).Result;

            await IsLoginTaken(userDto.Login);

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
            var user = await _userBase.GetFirstAsync(u => u.Login == login.ToLower());
            if (user == null)
                throw new ResourceNotFoundException("Niepoprawna nazwa uzytkownika");
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
