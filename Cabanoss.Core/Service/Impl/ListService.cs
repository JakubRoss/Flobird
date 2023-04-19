using AutoMapper;
using Cabanoss.Core.Authorization;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Exceptions;
using Cabanoss.Core.Model.List;
using Cabanoss.Core.Repositories;
using Cabanoss.Core.Repositories.Impl;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Cabanoss.Core.Service.Impl
{
    public class ListService : IListService
    {
        private IListRepository _listRepository;
        private IMapper _mapper;
        private IBoardBaseRepository _boardBaseRepository;
        private IAuthorizationService _authorizationService;

        public ListService(
            IListRepository listRepository,
            IMapper mapper,
            IBoardBaseRepository boardBaseRepository,
            IAuthorizationService authorizationService)
        {
            _listRepository = listRepository;
            _mapper = mapper;
            _boardBaseRepository = boardBaseRepository;
            _authorizationService = authorizationService;
        }
        #region Utils
        private async Task<List> GetList(int listId, int? boardId)
        {
            var list = new List();
            if(boardId is null)
            {
                list = await _listRepository.GetFirstAsync(p => p.Id == listId);
            }
            list = await _listRepository.GetFirstAsync(p=>p.Id == listId && p.BoardId == boardId);
            if (list is null)
                throw new ResourceNotFoundException("ResourceNotFound");
            return list;
        }
        public async System.Threading.Tasks.Task<AuthorizationResult> CheckBoardMembership(int BoardId, ClaimsPrincipal user)
        {
            var board = await _boardBaseRepository.GetFirstAsync(i => i.Id == BoardId, i => i.BoardUsers);
            var authorizationResult = await _authorizationService.AuthorizeAsync(user, board, new BelongToRequirements());
            return authorizationResult;
        }
        #endregion

        public async Task<List<ListDto>> GetAllAsync(int boardId, ClaimsPrincipal claims)
        {
            var authorizationResult = await CheckBoardMembership(boardId, claims);
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("no access");

            var lists = await _listRepository.GetAllAsync(l => l.BoardId == boardId);
            var dtoList = _mapper.Map<List<ListDto>>(lists);
            return dtoList;
        }
        public async Task CreateListAsync(int boardId , string name, ClaimsPrincipal claims)
        {
            var i = GetAllAsync(boardId, claims).Result.Count;
            var list = new List();
            list.CreatedAt = DateTime.Now;
            list.BoardId = boardId;
            list.Name = name;
            list.Position = i++;
            await _listRepository.AddAsync(list);
        }
        public async Task<ListDto> GetListAsync(int listId , int boardId , ClaimsPrincipal claims)
        {
            var list = await GetList(listId, boardId);
            var authorizationResult = await CheckBoardMembership(list.BoardId, claims);
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("no access");
            return _mapper.Map<ListDto>(list);
        }
        public async Task ModList (int listId , int boardId, string name ,ClaimsPrincipal claims)
        {
            var list = await GetList(listId, boardId);
            var authorizationResult = await CheckBoardMembership(list.BoardId, claims);
            if (!authorizationResult.Succeeded && boardId != list.BoardId)
                throw new ResourceNotFoundException("no access");
            list.UpdatedAt = DateTime.Now;
            list.Name = name;
            await _listRepository.UpdateAsync(list);

        }
        public async Task SetDeadline (int listid,int boardId, DateOnly date, ClaimsPrincipal claims)
        {
            var authorizationResult = await CheckBoardMembership(boardId, claims);
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("no access");

            var list = await GetList(listid, boardId);
            list.Deadline = date.ToDateTime(new TimeOnly());
            await _listRepository.UpdateAsync(list);
        }
    }
}
