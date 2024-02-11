﻿using Application.Authorization;
using Application.Data.Entities;
using Application.Exceptions;
using Application.Model.Element;
using Application.Model.User;
using Application.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Application.Service.Impl
{
    public class ElementService : IElementService
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IElementRepository _element;
        private readonly IMapper _mapper;
        private readonly IElementUsersRepository _elementUsersRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpUserContextService _httpUserContextService;

        public ElementService(IBoardRepository boardRepository,
            IAuthorizationService authorizationService,
            IElementRepository element,
            IMapper mapper,
            IElementUsersRepository elementUsersRepository,
            IUserRepository userRepository,
            IHttpUserContextService httpUserContextService)
        {
            _boardRepository = boardRepository;
            _authorizationService = authorizationService;
            _element = element;
            _mapper = mapper;
            _elementUsersRepository = elementUsersRepository;
            _userRepository = userRepository;
            _httpUserContextService = httpUserContextService;

        }
        #region Utils
        private async Task<Board> GetBoardByElementId(int elementId)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists
                    .Any(list => list.Cards.Any(card => card.Tasks.Any(task => task.Elements.Any(eId => eId.Id == elementId)))), i=>i.BoardUsers);

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
        private async Task CheckBoardMembership(Board board, int userId)
        {
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");

            var isUser = board.BoardUsers.Any(u=>u.UserId == userId);
            if (!isUser)
                throw new ResourceNotFoundException("no access");
        }
        #endregion

        public async Task<List<ResponseElementDto>> GetElements(int taskId)
        {
            var board = await GetBoardByTaskId(taskId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Read));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var elements = await _element.GetAllAsync(p => p.TaskId == taskId);
            var elementsDto = _mapper.Map<List<ResponseElementDto>>(elements);
            return elementsDto;
        }
        public async Task<ResponseElementDto> GetElement(int elementId)
        {
            var board = await GetBoardByElementId(elementId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Read));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var element = await _element.GetFirstAsync(p => p.Id == elementId, i=>i.ElementUsers);
            var elementDto = _mapper.Map<ResponseElementDto>(element);
            return elementDto;
        }
        public async Task AddElement(int taskId, ElementDto elementDto)
        {
            var board = await GetBoardByTaskId(taskId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Create));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var newElement = new Element()
            {
                Description = elementDto.Description,
                IsComplete = false,
                CreatedAt = DateTime.UtcNow,
                TaskId = taskId
            };

            await _element.AddAsync(newElement);
        }
        public async Task UpdateElement(int elementId, UpdateElementDto updateElementDto)
        {
            var board = await GetBoardByElementId(elementId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Update));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var element = await _element.GetFirstAsync(p => p.Id == elementId);
            if (element == null)
                throw new ResourceNotFoundException("Resource Not Found");

            if (element.Description != null)
                element.Description = updateElementDto.Description;

            if (element.IsComplete != null)
                element.IsComplete = updateElementDto.IsComplete;

            await _element.UpdateAsync(element);
        }
        public async Task DeleteElement(int elementId)
        {
            var board = await GetBoardByElementId(elementId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Delete));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var element = await _element.GetFirstAsync(p => p.Id == elementId);
            if (element == null)
                throw new ResourceNotFoundException("Resource Not Found");

            await _element.DeleteAsync(element);
        }
        public async Task CheckElement(int elementId, ElementCheckDto updateElementDto)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User,
                await GetBoardByElementId(elementId),
                new ResourceOperationRequirement(ResourceOperations.Update));

            #region Validation
            var element = await _element.GetFirstAsync(p => p.Id == elementId, i=>i.ElementUsers);
            if (element == null)
                throw new ResourceNotFoundException("Resource Not Found");

            if (element.IsComplete != true && element.IsComplete != false)
                throw new ResourceNotFoundException("Is Complete must be true or false");
            #endregion

            if (authorizationResult.Succeeded || (element.ElementUsers
                    .FirstOrDefault(id => id.UserId == _httpUserContextService.UserId) != null) ? true : false )
            {
                element.IsComplete = updateElementDto.IsComplete;
                await _element.UpdateAsync(element);
            }
            else
                throw new UnauthorizedException("Unauthorized");
        }
        //For members
        public async Task<List<ResponseUserDto>> GetElementUsers(int elementId)
        {
            var board = await GetBoardByElementId(elementId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Read));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var element = await _element.GetFirstAsync(eid => eid.Id == elementId, i => i.ElementUsers);
            var elementUsers = element.ElementUsers.ToList();

            var users = new List<User>();
            foreach (var user in elementUsers)
            {
                var cos = _userRepository.GetFirstAsync(u=>u.Id == user.UserId).Result;
                users.Add(cos);
            }
            var usersDto = _mapper.Map<List<ResponseUserDto>>(users);
            return usersDto;

        } 
        public async Task AddUserToElement(int elementId, int userId)
        {
            var board = await GetBoardByElementId(elementId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Update));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");
            await CheckBoardMembership(board, userId);

            var element = await _element.GetFirstAsync(_ => _.Id == elementId, i=>i.ElementUsers);

            var user = element.ElementUsers.FirstOrDefault(i => i.UserId == userId);
            if (user != null)
                throw new ConflictExceptions("the object is in the resource");

            var userElement = new ElementUsers()
            {
                UserId = userId,
                ElementId = elementId
            };

            await _elementUsersRepository.AddAsync(userElement);
        }
        public async Task DeleteUserFromElement(int elementId, int userId)
        {
            var board = await GetBoardByElementId(elementId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Update));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");
            await CheckBoardMembership(board, userId);

            var boardUser = board.BoardUsers.FirstOrDefault(p=>p.BoardId == board.Id && p.UserId==userId);

            var element = await _elementUsersRepository.GetFirstAsync(_ => _.ElementId == elementId && _.UserId == userId);
            if (element == null)
                throw new ResourceNotFoundException("Resource not Found");
            await _elementUsersRepository.DeleteAsync(element);
        }
    }
}
