using AutoMapper;
using Cabanoss.Core.Authorization;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Exceptions;
using Cabanoss.Core.Model.List;
using Cabanoss.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Cabanoss.Core.Service.Impl
{
    public class ListService : IListService
    {
        private IListRepository _listRepository;
        private IMapper _mapper;
        private IBoardRepository _boardRepository;
        private IAuthorizationService _authorizationService;

        public ListService(
            IListRepository listRepository,
            IMapper mapper,
            IBoardRepository boardRepository,
            IAuthorizationService authorizationService)
        {
            _listRepository = listRepository;
            _mapper = mapper;
            _boardRepository = boardRepository;
            _authorizationService = authorizationService;
        }
        #region Utils
        private async Task<List> GetList(int listId)
        {
            var list = await _listRepository.GetFirstAsync(p => p.Id == listId);
            if (list is null)
                throw new ResourceNotFoundException("ResourceNotFound");
            return list;
        }
        private async Task<Board> GetBoardById(int boardId)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Id == boardId, i => i.BoardUsers);
            if (board is null) throw new ResourceNotFoundException("Resource Not Found");
            return board;
        }
        private async Task<Board> GetBoardByListId(int listId)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(list => list.Id == listId), i => i.BoardUsers);
            if (board is null) throw new ResourceNotFoundException("Resource Not Found");
            return board;
        }
        private async System.Threading.Tasks.Task CheckBoardMembership(int listId, ClaimsPrincipal user)
        {
            var board = await _boardRepository.GetFirstAsync(board => board.Lists.Any(list => list.Id == listId), i=>i.BoardUsers);
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");

            var authorizationResult = await _authorizationService.AuthorizeAsync(user, board, new MembershipRequirements());
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("no access");
        }
        private async Task<AuthorizationResult> ChceckCreatorRole(int BoardId, ClaimsPrincipal user)
        {
            var board = await _boardRepository.GetFirstAsync(i => i.Id == BoardId, i => i.BoardUsers);
            var authorizationResult = await _authorizationService.AuthorizeAsync(user, board, new CreatorRoleRequirements());
            return authorizationResult;
        }
        private async Task<AuthorizationResult> ChceckAdminRole(int BoardId, ClaimsPrincipal user)
        {
            var board = await _boardRepository.GetFirstAsync(i => i.Id == BoardId, i => i.BoardUsers);
            var authorizationResult = await _authorizationService.AuthorizeAsync(user, board, new AdminRoleRequirements());
            return authorizationResult;
        }
        #endregion

        public async Task<List<ListDto>> GetAllAsync(int boardId, ClaimsPrincipal claims)
        {
            var board = await GetBoardById(boardId);
            var authorizationResult = await _authorizationService.AuthorizeAsync(claims, board, new MembershipRequirements());
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("no access");

            var lists = await _listRepository.GetAllAsync(l => l.BoardId == boardId);
            var dtoList = _mapper.Map<List<ListDto>>(lists);
            return dtoList;
        }
        public async Task CreateListAsync(int boardId , string name, ClaimsPrincipal claims)
        {
            var authorizationResult = await ChceckAdminRole(boardId, claims);
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("No Access");

            var i = GetAllAsync(boardId, claims).Result.Count;
            var list = new List();
            list.CreatedAt = DateTime.Now;
            list.BoardId = boardId;
            list.Name = name;
            list.Position = i++;
            await _listRepository.AddAsync(list);
        }
        public async Task<ListDto> GetListAsync(int listId , ClaimsPrincipal claims)
        {
            await CheckBoardMembership(listId, claims);

            var list = await GetList(listId);;
            return _mapper.Map<ListDto>(list);
        }
        public async Task UpdateList(int listId , string name ,ClaimsPrincipal claims)
        {
            var board = await GetBoardByListId(listId);
            var authorizationResult = await ChceckAdminRole(board.Id, claims);
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("No Access");

            var list = await GetList(listId);
            list.UpdatedAt = DateTime.Now;
            list.Name = name;
            await _listRepository.UpdateAsync(list);

        }
        public async Task SetDeadline (int listId, DateOnly date, ClaimsPrincipal claims)
        {
            var board = await GetBoardByListId(listId);
            var authorizationResult = await ChceckAdminRole(board.Id, claims);
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("No Access");

            var list = await GetList(listId);
            list.Deadline = date.ToDateTime(new TimeOnly());
            await _listRepository.UpdateAsync(list);
        }
        public async Task DeleteList(int listId, ClaimsPrincipal claims)
        {
            var board = await GetBoardByListId(listId);
            var authorizationResult = await ChceckCreatorRole(board.Id, claims);
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("No Access");

            var list = await GetList(listId);
            await _listRepository.DeleteAsync(list);
        }
    }
}
