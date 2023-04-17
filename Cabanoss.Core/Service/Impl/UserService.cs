using AutoMapper;
using Cabanoss.Core.Common;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Exceptions;
using Cabanoss.Core.Model.User;
using Cabanoss.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cabanoss.Core.Service.Impl
{
    public class UserService : IUserService
    {
        private IUserBaseRepository _userBase;
        private IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IWorkspaceService _workspaceBussiness;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserService(IUserBaseRepository userBase
            ,IMapper mapper
            , IWorkspaceService workspaceBussiness
            ,IPasswordHasher<User>passwordHasher
            ,AuthenticationSettings authenticationSettings)
        {
            _userBase = userBase;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _workspaceBussiness = workspaceBussiness;
            _authenticationSettings = authenticationSettings;
        }
        private async System.Threading.Tasks.Task<User> GetUser(string login)
        {
            var LowLogin = login.ToLower();
            var user = await _userBase.GetFirstAsync(u => u.Login.ToLower() == LowLogin);
            if (user == null)
                throw new ResourceNotFoundException("Invalid login name");
            return user;
        }
        public async System.Threading.Tasks.Task<User> GetUserById(int id)
        {
            var user = await _userBase.GetFirstAsync(p => p.Id == id);
            if (user == null)
                throw new ResourceNotFoundException("User don't exists");
            return user;
        }

        public async System.Threading.Tasks.Task AddUserAsync(CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var hashedPassword = _passwordHasher.HashPassword(user, userDto.Password);
            user.PasswordHash = hashedPassword;
            user.CreatedAt = DateTime.Now;
            await _userBase.AddAsync(user);

            await _workspaceBussiness.AddWorkspaceAsync(user.Id);
        }
        public async System.Threading.Tasks.Task<UserDto> GetUserAsync(int id)
        {
            var user = await GetUserById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        public async System.Threading.Tasks.Task<UserDto> UpdateUserAsync(int id, UpdateUserDto userDto)
        {
            var user = await GetUserById(id);
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
        public async System.Threading.Tasks.Task RemoveUserAsync(int id)
        {
            var user = await GetUserById(id);
            await _userBase.DeleteAsync(user);
        }
        public async System.Threading.Tasks.Task<List<UserDto>> GetUsersAsync()
        {
            var users = await _userBase.GetAllAsync();
            if(users is null)
                throw new ResourceNotFoundException("Aplikacja nie posiada uzytkownikow");
            var usersDto = new List<UserDto>();
            foreach (var user in users)
            {
                var userDto = _mapper.Map<UserDto>(user);
                usersDto.Add(userDto);
            }
            return usersDto;
        }
        public async System.Threading.Tasks.Task<string> GenerateJwt(UserLoginDto userLoginDto)
        {
            var user = await GetUser(userLoginDto.Login);
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userLoginDto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new ResourceNotFoundException("Invalid User name or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpiredays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var TokenHandler = new JwtSecurityTokenHandler();
            return TokenHandler.WriteToken(token);
        }
    }
}
