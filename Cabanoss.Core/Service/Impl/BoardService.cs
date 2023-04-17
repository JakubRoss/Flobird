using AutoMapper;
using Cabanoss.Core.Authorization;
using Cabanoss.Core.Common;
using Cabanoss.Core.Data;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Exceptions;
using Cabanoss.Core.Model.Board;
using Cabanoss.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;

namespace Cabanoss.Core.Service.Impl
{
    public class BoardService : IBoardService
    {
        private IBoardBaseRepository _boardBaseRepository;
        private IMapper _mapper;
        private IBoardUsersBaseRepository _boardUsersBaseRepository;
        private IUserBaseRepository _userBase;
        private IAuthorizationService _authorizationService;
        private IUserService _userService;

        public BoardService(IBoardBaseRepository boardBaseRepository,
            IMapper mapper,
            IBoardUsersBaseRepository boardUsersBaseRepository,
            IUserBaseRepository userBase,
            IAuthorizationService authorizationService,
            IUserService userService)
        {
            _boardBaseRepository = boardBaseRepository;
            _mapper = mapper;
            _boardUsersBaseRepository = boardUsersBaseRepository;
            _userBase = userBase;
            _authorizationService = authorizationService;
            _userService = userService;
        }
        #region private

        /// <summary>
        /// Sprawdza przynaleznosc uzytkownika(pytajacego) do danej tablicy
        /// </summary>
        /// <param name="id">Id tablicy.</param>
        /// <param name="user">Claims principal.</param>
        /// <returns>Authorization result</returns>
        private async System.Threading.Tasks.Task<AuthorizationResult> CheckBoardMembership(int BoardId, ClaimsPrincipal user)
        {
            var board = await _boardBaseRepository.GetFirstAsync(i => i.Id == BoardId, i => i.BoardUsers);
            var authorizationResult = await _authorizationService.AuthorizeAsync(user, board, new BelongToRequirements());
            return authorizationResult;
        }
        private async System.Threading.Tasks.Task<BoardUser> CheckBoardMembership(int boardId, int userId)
        {
            var boardUser = await _boardUsersBaseRepository.GetFirstAsync(i => i.BoardId == boardId && i.UserId == userId);
            return boardUser;
            
        }
        /// <summary>
        /// Sprawdza role uzytkownika w danej tablicy
        /// </summary>
        /// <param name="id">Id tablicy.</param>
        /// <param name="user">Claims principal.</param>
        /// <returns>Role enum</returns>
        private async Task<Roles> ChceckUserRole(int BoardId,ClaimsPrincipal user)
        {
            var authorizationResult = await CheckBoardMembership(BoardId, user);
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("no access");
            var userId = int.Parse(user.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.NameIdentifier).Value);
            var boardUser = await _boardUsersBaseRepository.GetFirstAsync(p => p.UserId == userId && p.BoardId == BoardId);

            return boardUser.Roles;
        }
        #endregion

        public async System.Threading.Tasks.Task CreateBoardAsync(CreateBoardDto createBoardDto, ClaimsPrincipal user)
        {
            var userId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var userDb = await _userBase.GetFirstAsync(p => p.Id == userId, i => i.Workspace);

            var board = _mapper.Map<Board>(createBoardDto);
            board.CreatedAt = DateTime.Now;
            board.WorkspaceId = userDb.Workspace.Id;
            var sboard = await _boardBaseRepository.AddAsync(board);

            var newBoardUser = new BoardUser { BoardId = sboard.Id, UserId = userDb.Id };
            newBoardUser.Roles = Roles.Creator;
            await _boardUsersBaseRepository.AddAsync(newBoardUser);
        }

        public async System.Threading.Tasks.Task DeleteBoardAsync(int boardId, ClaimsPrincipal user)
        {
            var role = await ChceckUserRole(boardId, user);
            if (!(role == Roles.Creator))
                throw new ResourceNotFoundException("No Access");
            var board = await _boardBaseRepository.GetFirstAsync(p => p.Id == boardId);
            if (board is null)
                throw new ResourceNotFoundException("Resource Not Found");
            await _boardBaseRepository.DeleteAsync(board);
        }

        public async System.Threading.Tasks.Task<List<ResponseBoardDto>> GetBoardsAsync(ClaimsPrincipal user)
        {
            var userId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var userboards = await _boardUsersBaseRepository.GetAllAsync(p=>p.UserId == userId);
            var boards = new List<Board>();
            foreach (var board in userboards)
            {
                var cos = await _boardBaseRepository.GetFirstAsync(p=>p.Id == board.BoardId);
                boards.Add(cos);
            }
            var responseBoards = _mapper.Map<List<ResponseBoardDto>>(boards);
            return responseBoards;
        }

        public async System.Threading.Tasks.Task<List<ResponseBoardUser>> GetUsersAsync(int BoardId, ClaimsPrincipal user)
        {
            var authorizationResult = await CheckBoardMembership(BoardId, user);
            if (!authorizationResult.Succeeded)
                throw new ResourceNotFoundException("no access");

            var users = await _userBase.GetAllAsync(u => u.BoardUsers.Any(bu => bu.BoardId == BoardId));
            var mapedUsers = _mapper.Map<List<ResponseBoardUser>>(users);
            return mapedUsers;
        }

        public async System.Threading.Tasks.Task AddUsersAsync(int boardId , int userId, ClaimsPrincipal user)
        {
            await _userService.GetUserById(userId);
            var role = await ChceckUserRole(boardId, user);
            if (role == Roles.User)
                throw new ResourceNotFoundException("No Access");
            var newBoardUser = new BoardUser { BoardId = boardId, UserId = userId };
            newBoardUser.Roles = Roles.User;
            await _boardUsersBaseRepository.AddAsync(newBoardUser);
        }

        public async System.Threading.Tasks.Task RemoveUserAsync (int boardId, int userId, ClaimsPrincipal user)
        {
            var role = await ChceckUserRole(boardId, user);
            if (!(role == Roles.User && role == Roles.Creator))
                throw new ResourceNotFoundException("No Access");

            var boardUser = await _boardUsersBaseRepository.GetFirstAsync(i=>i.BoardId == boardId&& i.UserId ==userId);
            if (boardUser != null)
                await _boardUsersBaseRepository.DeleteAsync(boardUser);
        }

        public async System.Threading.Tasks.Task ModifyNameBoardAsync(int boardId, UpdateBoardDto updateBoardDto, ClaimsPrincipal user)
        {
            var board = await _boardBaseRepository.GetFirstAsync(i=>i.Id == boardId);
            if(board is null)
                throw new ResourceNotFoundException("ResourceNotFound");

            var role = await ChceckUserRole(boardId, user);
            if (!(role == Roles.Creator))
                throw new ResourceNotFoundException("No Access");

            board.Name = updateBoardDto.Name;
            board.UpdatedAt = DateTime.Now;
            await _boardBaseRepository.UpdateAsync(board);
        }

        public async System.Threading.Tasks.Task SetUserRole (int boardId,int userId, int roles, ClaimsPrincipal user)
        {
            var role = await ChceckUserRole(boardId, user);
            if (role ==Roles.User)
                throw new ResourceNotFoundException("No Access");
            var boardUser = await CheckBoardMembership(boardId, userId);
            if (boardUser == null || boardUser.Roles == Roles.Creator)
                throw new ResourceNotFoundException("ResourceNotFound");

            switch (roles)
            {
                case 0:
                    boardUser.Roles = Roles.Admin;
                    await _boardUsersBaseRepository.UpdateAsync(boardUser);
                    break;
                case 1:
                    boardUser.Roles = Roles.User;
                    await _boardUsersBaseRepository.UpdateAsync(boardUser);
                    break;
                default:
                    throw new ResourceNotFoundException("ResourceNotFound");
            }
        }

    }
}

