using Application.Authorization;
using Application.Model.List;
using AutoMapper;
using Domain.Data.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Application.Service.Impl
{
    public class ListService : IListService
    {
        private readonly IListRepository _listRepository;
        private readonly IMapper _mapper;
        private readonly IBoardRepository _boardRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpUserContextService _httpUserContextService;

        public ListService(
            IListRepository listRepository,
            IMapper mapper,
            IBoardRepository boardRepository,
            IAuthorizationService authorizationService,
            IHttpUserContextService httpUserContextService)
        {
            _listRepository = listRepository;
            _mapper = mapper;
            _boardRepository = boardRepository;
            _authorizationService = authorizationService;
            _httpUserContextService = httpUserContextService;
        }
        #region Utils
        private async Task<List> GetList(int listId)
        {
            var list = await _listRepository.GetFirstAsync(p => p.Id == listId);
            return list ?? throw new ResourceNotFoundException("ResourceNotFound");
        }
        private async Task<Board> GetBoardById(int boardId)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Id == boardId, i => i.BoardUsers);
            return board ?? throw new ResourceNotFoundException("Resource Not Found");
        }
        private async Task<Board> GetBoardByListId(int listId)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(list => list.Id == listId), i => i.BoardUsers);
            return board ?? throw new ResourceNotFoundException("Resource Not Found");
        }
        #endregion

        public async Task<List<ListDto>> GetAllAsync(int boardId)
        {
            var board = await GetBoardById(boardId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Read));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var lists = await _listRepository.GetAllAsync(l => l.BoardId == boardId);
            var dtoList = _mapper.Map<List<ListDto>>(lists);
            return dtoList;
        }
        public async Task CreateListAsync(int boardId , string name)
        {
            var board = await GetBoardById(boardId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Create));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var i = GetAllAsync(boardId).Result.Count;
            var list = new List(name)
            {
                CreatedAt = DateTime.Now,
                BoardId = boardId,
                Position = i++
        };
            await _listRepository.AddAsync(list);
        }
        public async Task<ListDto> GetListAsync(int listId)
        {
            var board = await GetBoardByListId(listId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Read));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var list = await GetList(listId);;
            return _mapper.Map<ListDto>(list);
        }
        public async Task UpdateList(int listId , string name)
        {
            var board = await GetBoardByListId(listId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Update));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var list = await GetList(listId);
            list.UpdatedAt = DateTime.Now;
            list.Name = name;
            await _listRepository.UpdateAsync(list);

        }
        public async Task SetDeadline (int listId, DateOnly date)
        {
            var board = await GetBoardByListId(listId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Update));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var list = await GetList(listId);
            list.Deadline = date.ToDateTime(new TimeOnly());
            await _listRepository.UpdateAsync(list);
        }
        public async Task DeleteList(int listId)
        {
            var board = await GetBoardByListId(listId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Delete));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var list = await GetList(listId);
            await _listRepository.DeleteAsync(list);
        }
    }
}
