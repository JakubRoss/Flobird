using Application.Common;
using Application.Model.User;
using AutoMapper;
using Domain.Authentication;
using Domain.Data.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Application.Service.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IHttpUserContextService _httpUserContextService;
        private readonly IAuthenticationService _authenticationService;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            IPasswordHasher<User>passwordHasher,
            IHttpUserContextService httpUserContextService,
            IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _httpUserContextService = httpUserContextService;
            _authenticationService = authenticationService;
        }
        #region Utils
        private async Task<User> GetUser()
        {
            var id = _httpUserContextService.UserId;
            var user = await _userRepository.GetFirstAsync(p => p.Id == id);
            return user ?? throw new ResourceNotFoundException("User don't exists");
        }

        #endregion

        public async Task AddUserAsync(CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var hashedPassword = _passwordHasher.HashPassword(user, userDto.Password);
            user.PasswordHash = hashedPassword;
            user.CreatedAt = DateTime.Now;
            await _userRepository.AddAsync(user);
        }
        public async Task<UserDto> GetUserAsync()
        {
            var user = await GetUser();
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        public async Task<UserDto> UpdateUserAsync(UpdateUserDto userDto)
        {
            var user = await GetUser();
            #region updt_properties
            if (userDto.Login!=null)
                user.Login = userDto.Login;
            if (userDto.Password != null)
            {
                var hashedPassword = _passwordHasher.HashPassword(user,userDto.Password);
                user.PasswordHash = hashedPassword;
            }
            if (userDto.Email != null)
                user.Email = userDto.Email;
            user.UpdatedAt = DateTime.Now;
            #endregion

            var updated = await _userRepository.UpdateAsync(user);
            var updatedDto = _mapper.Map<UserDto>(updated);
            return updatedDto;
        }

        public async Task RemoveUserAsync()
        {
            var user = await GetUser();
            await _userRepository.DeleteAsync(user);
        }

        public async Task<List<ResponseUserDto>> GetUsersAsync(string? searchingPhrase)
        {
            var users = await _userRepository.GetUsersAsync(searchingPhrase);
            if(users is null)
                throw new ResourceNotFoundException("The application has no users");
            var usersDto = new List<ResponseUserDto>();
            foreach (var user in users)
            {
                var userDto = _mapper.Map<ResponseUserDto>(user);
                usersDto.Add(userDto);
            }
            return usersDto;
        }
        public async Task<LoginResult> LogIn(UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetFirstAsync(u => u.Login.ToLower() == userLoginDto.Login.ToLower());

            string tokenText = _authenticationService.GenerateJwt(user!, userLoginDto.Password);

            var userDto = _mapper.Map<ResponseUserDto>(user);
            var loginResult = new LoginResult(tokenText, userDto);
            return loginResult;
        }
    }
}
