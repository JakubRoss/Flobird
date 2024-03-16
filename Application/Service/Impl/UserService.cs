using Application.Common;
using Application.Model.User;
using AutoMapper;
using Domain.Authentication;
using Domain.Common;
using Domain.Data.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace Application.Service.Impl
{
    public class UserService(
        IUserRepository userRepository,
        IMapper mapper,
        IPasswordHasher<User> passwordHasher,
        IHttpUserContextService httpUserContextService,
        IAuthenticationService authenticationService,
        IBoardRepository boardRepository)
        : IUserService
    {
        #region Utils
        private async Task<User> GetUser()
        {
            var id = httpUserContextService.UserId;
            var user = await userRepository.GetFirstAsync(p => p.Id == id);
            return user ?? throw new ResourceNotFoundException("User don't exists");
        }

        #endregion

        public async Task AddUserAsync(CreateUserDto userDto)
        {
            // tutaj musze zrobic tranzakcje przy seedowaniu (dodawanie usera i seedowanie tablicy)!

             var user = mapper.Map<User>(userDto);
             var hashedPassword = passwordHasher.HashPassword(user, userDto.Password);
             user.PasswordHash = hashedPassword;
             user.CreatedAt = DateTime.Now;

             // Dodanie użytkownika do repozytorium
             var userAdded =await userRepository.AddAsync(user);

             // Teraz możemy uzyskać Id nowo utworzonego użytkownika
             // i przypisać mu tablicę
             var board = RegistrationDataSeeder.RegistrationDataSeeder.RegistrationBoardSeeder($"{user.Login} Board", userAdded.Id);
             user.BoardUsers.Add(new BoardUser()
             {
                 Roles = Roles.Creator,
                 Board = board,
             });
             user.AvatarPath = RegistrationDataSeeder.RegistrationDataSeeder.AvatarPathSeeder();

             // Aktualizacja użytkownika w repozytorium
             await userRepository.UpdateAsync(user);

            
        }
        public async Task<UserDto> GetUserAsync()
        {
            var user = await GetUser();
            var userDto = mapper.Map<UserDto>(user);
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
                var hashedPassword = passwordHasher.HashPassword(user,userDto.Password);
                user.PasswordHash = hashedPassword;
            }
            if (userDto.Email != null)
                user.Email = userDto.Email;
            user.UpdatedAt = DateTime.Now;
            #endregion

            var updated = await userRepository.UpdateAsync(user);
            var updatedDto = mapper.Map<UserDto>(updated);
            return updatedDto;
        }

        public async Task RemoveUserAsync()
        {
            var user = await GetUser();

            // Pobierz wszystkie tablice powiązane z użytkownikiem w ktorych jest on w roli creatora danej tablicy
            var boards = await boardRepository.GetAllAsync(b => b.BoardUsers.Any(bu => bu.UserId == user.Id && bu.Roles == Roles.Creator), 
                i => i.BoardUsers);

            // Wykonaj operacje w ramach jednej transakcji
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                // Usuń boards (kaskadowo usuwa boardUsers i listy)
                await boardRepository.DeleteRangeAsync(boards); 

                // Usuń user (kaskadowo usuwa boardUsers)
                await userRepository.DeleteAsync(user); 

                // Jeśli wszystko przebiegło pomyślnie, zatwierdź transakcję
                scope.Complete();
            }

        }

        public async Task<List<ResponseUserDto>> GetUsersAsync(string? searchingPhrase)
        {
            var users = await userRepository.GetUsersAsync(searchingPhrase);
            if(users is null)
                throw new ResourceNotFoundException("The application has no users");
            var usersDto = new List<ResponseUserDto>();
            foreach (var user in users)
            {
                var userDto = mapper.Map<ResponseUserDto>(user);
                usersDto.Add(userDto);
            }
            return usersDto;
        }
        public async Task<LoginResult> LogIn(UserLoginDto userLoginDto)
        {
            var user = await userRepository.GetFirstAsync(u => u.Login.ToLower() == userLoginDto.Login.ToLower());

            string tokenText = authenticationService.GenerateJwt(user!, userLoginDto.Password);

            var userDto = mapper.Map<ResponseUserDto>(user);
            var loginResult = new LoginResult(tokenText, userDto);
            return loginResult;
        }
    }
}
