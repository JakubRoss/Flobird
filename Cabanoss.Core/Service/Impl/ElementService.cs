using AutoMapper;
using Cabanoss.Core.Authorization;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Exceptions;
using Cabanoss.Core.Model.Element;
using Cabanoss.Core.Model.User;
using Cabanoss.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Cabanoss.Core.Service.Impl
{
    public class ElementService : IElementService
    {
        private IBoardRepository _boardRepository;
        private IAuthorizationService _authorizationService;
        private IElementRepository _element;
        private IMapper _mapper;
        private IElementUsersRepository _elementUsersRepository;
        private IBoardUsersRepository _boardUsersRepository;
        private IUserRepository _userRepository;

        public ElementService(IBoardRepository boardRepository,
            IAuthorizationService authorizationService,
            IElementRepository element,
            IMapper mapper,
            IElementUsersRepository elementUsersRepository,
            IBoardUsersRepository boardUsersRepository,
            IUserRepository userRepository)
        {
            _boardRepository = boardRepository;
            _authorizationService = authorizationService;
            _element = element;
            _mapper = mapper;
            _elementUsersRepository = elementUsersRepository;
            _boardUsersRepository = boardUsersRepository;
            _userRepository = userRepository;

        }
        #region Utils
        private async Task<Board> GetBoardByElementId(int elementId)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists
                    .Any(list => list.Cards.Any(card => card.Tasks.Any(task => task.Elements.Any(eId => eId.Id == elementId)))), i => i.BoardUsers);

            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");
            return board;
        }
        private async Task<Board> GetBoardByTaskId(int taskId)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(list => list.Cards.Any(card => card.Tasks.Any(task => task.Id == taskId))), i => i.BoardUsers);
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");
            return board;
        }
        private async Task CheckBoardMembership(Board board, ClaimsPrincipal user)
        {
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");

            var authorizationResult = await _authorizationService.AuthorizeAsync(user, board, new MembershipRequirements());
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("no access");
        }
        private async Task CheckBoardMembership(Board board, int userId)
        {
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");

            var isUser = board.BoardUsers.Any(u=>u.UserId == userId);
            if (!isUser)
                throw new ResourceNotFoundException("no access");
        }
        private async Task<AuthorizationResult> ChceckAdminRole(Board board, ClaimsPrincipal user)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(user, board, new AdminRoleRequirements());
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("No Access");
            return authorizationResult;
        }
        #endregion

        public async Task<List<ResponseElementDto>> GetElements(int taskId, ClaimsPrincipal claims)
        {
            var board = await GetBoardByTaskId(taskId);

            await CheckBoardMembership(board, claims);

            var elements = await _element.GetAllAsync(p => p.TaskId == taskId);
            var elementsDto = _mapper.Map<List<ResponseElementDto>>(elements);
            return elementsDto;
        }
        public async Task<ResponseElementDto> GetElement(int elementId, ClaimsPrincipal claims)
        {
            var board = await GetBoardByElementId(elementId);

            await CheckBoardMembership(board, claims);

            var element = await _element.GetFirstAsync(p => p.Id == elementId);
            var elementDto = _mapper.Map<ResponseElementDto>(element);
            return elementDto;
        }
        public async Task AddElement(int taskId, ElementDto elementDto, ClaimsPrincipal claims)
        {
            var board = await GetBoardByTaskId(taskId);

            await ChceckAdminRole(board, claims);

            var newElement = new Element()
            {
                Description = elementDto.Description,
                IsComplete = false,
                CreatedAt = DateTime.UtcNow,
                TaskId = taskId
            };

            await _element.AddAsync(newElement);
        }
        public async Task UpdateElement(int elementId, UpdateElementDto updateElementDto, ClaimsPrincipal claims)
        {
            var board = await GetBoardByElementId(elementId);

            await CheckBoardMembership(board, claims);

            var element = await _element.GetFirstAsync(p => p.Id == elementId);
            if (element == null)
                throw new ResourceNotFoundException("Resource Not Found");

            if (element.Description != null)
                element.Description = updateElementDto.Description;

            if (element.IsComplete != null)
                element.IsComplete = updateElementDto.IsComplete;

            await _element.UpdateAsync(element);
        }
        public async Task DeleteElement(int elementId, ClaimsPrincipal claims)
        {
            var board = await GetBoardByElementId(elementId);
            await ChceckAdminRole(board, claims);

            var element = await _element.GetFirstAsync(p => p.Id == elementId);
            if (element == null)
                throw new ResourceNotFoundException("Resource Not Found");

            await _element.DeleteAsync(element);
        }
        public async Task CheckElement(int elementId, UpdateElementDto updateElementDto, ClaimsPrincipal claims)
        {
            var board = await GetBoardByElementId(elementId);
            await CheckBoardMembership(board, claims);

            var element = await _element.GetFirstAsync(p => p.Id == elementId);
            if (element == null)
                throw new ResourceNotFoundException("Resource Not Found");

            if (element.IsComplete != true && element.IsComplete != false)
                throw new ResourceNotFoundException("Is Complete must be true or false");

            element.IsComplete = updateElementDto.IsComplete;
            await _element.UpdateAsync(element);
        }

        public async Task<List<ResponseUserDto>> GetElementUsers(int elementId,ClaimsPrincipal claims)
        {
            var board = await GetBoardByElementId(elementId);
            await CheckBoardMembership(board,claims);

            var boardUsers = await _boardUsersRepository.GetAllAsync(board => board.ElementUsers.Any(p=>p.ElementId == elementId));
            if (boardUsers == null)
                throw new ResourceNotFoundException("Resource not Found");

            var users = new List<User>();
            foreach (var user in boardUsers)
            {
                var cos = _userRepository.GetFirstAsync(u=>u.Id == user.UserId).Result;
                users.Add(cos);
            }
            var usersDto = _mapper.Map<List<ResponseUserDto>>(users);
            return usersDto;

        } 
        public async Task AddUserToElement(int elementId, int userId, ClaimsPrincipal claims)
        {
            var board = await GetBoardByElementId(elementId);
            await ChceckAdminRole(board, claims);
            await CheckBoardMembership(board, userId);

            var element = await _element.GetFirstAsync(_ => _.Id == elementId);

            var userElement = new ElementUsers()
            {
                BoardUserId = userId,
                ElementId = elementId
            };

            await _elementUsersRepository.AddAsync(userElement);
        }
        public async Task DeleteUserFromElement(int elementId, int userId, ClaimsPrincipal claims)
        {
            var board = await GetBoardByElementId(elementId);
            await ChceckAdminRole(board, claims);
            await CheckBoardMembership(board, userId);

            var boardUser = board.BoardUsers.FirstOrDefault(p=>p.BoardId == board.Id && p.UserId==userId);

            var element = await _elementUsersRepository.GetFirstAsync(_ => _.ElementId == elementId && _.BoardUserId == userId);
            if (element == null)
                throw new ResourceNotFoundException("Resource not Found");
            await _elementUsersRepository.DeleteAsync(element);
        }
    }
}
