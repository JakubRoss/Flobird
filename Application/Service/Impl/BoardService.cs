using Application.Authorization;
using Application.Model.Board;
using AutoMapper;
using Domain.Common;
using Domain.Data.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Application.Service.Impl
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;
        private readonly IBoardUsersRepository _boardUsersBaseRepository;
        private readonly IUserRepository _userBase;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserRepository _userRepository;
        private readonly IHttpUserContextService _httpUserContextService;

        public BoardService(IBoardRepository boardBaseRepository,
            IMapper mapper,
            IBoardUsersRepository boardUsersBaseRepository,
            IUserRepository userBase,
            IAuthorizationService authorizationService,
            IUserRepository userRepository,
            IHttpUserContextService httpUserContextService)
        {
            _boardRepository = boardBaseRepository;
            _mapper = mapper;
            _boardUsersBaseRepository = boardUsersBaseRepository;
            _userBase = userBase;
            _authorizationService = authorizationService;
            _userRepository = userRepository;
            _httpUserContextService = httpUserContextService;
        }
        #region utils
        private async Task<BoardUser> CheckBoardMembership(int boardId, int? userId)
        {
            var boardUser = await _boardUsersBaseRepository.GetFirstAsync(i => i.BoardId == boardId && i.UserId == userId);
            return boardUser ?? throw new ResourceNotFoundException("User don't exists");
        }
        private async Task<Board> GetBoard(int boardId)
        {
            var board = await _boardRepository.GetFirstAsync(x => x.Id == boardId, i=>i.BoardUsers);
            return board ?? throw new ResourceNotFoundException("Resource not found");
        }
        #endregion

        public async Task<ResponseBoardDto> GetBoardAsync(int boardId)
        {
            var board = await GetBoard(boardId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Read));
            return !authorizationResult.Succeeded ? throw new UnauthorizedException("Unauthorized") : _mapper.Map<ResponseBoardDto>(board);
        }
        public async Task CreateBoardAsync(CreateBoardDto createBoardDto)
        {
            var userDb = await _userBase.GetFirstAsync(p => p.Id == _httpUserContextService.UserId);

            var board = _mapper.Map<Board>(createBoardDto);
            board.CreatedAt = DateTime.Now;
            var sboard = await _boardRepository.AddAsync(board);

            var newBoardUser = new BoardUser(sboard.Id, userDb.Id)
            {
                Roles = Roles.Creator
        };
            await _boardUsersBaseRepository.AddAsync(newBoardUser);

        }

        public async Task DeleteBoardAsync(int boardId)
        {
            var board = await GetBoard(boardId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Delete));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");
            await _boardRepository.DeleteAsync(board);
        }

        public async Task<List<ResponseBoardDto>> GetBoardsAsync()
        {
            var userboards = await _boardUsersBaseRepository.GetAllAsync(p=>p.UserId == _httpUserContextService.UserId);
            var boards = new List<Board>();
            foreach (var board in userboards)
            {
                var cos = await _boardRepository.GetFirstAsync(p=>p.Id == board.BoardId);
                boards.Add(cos);
            }
            var responseBoards = _mapper.Map<List<ResponseBoardDto>>(boards);
            return responseBoards;
        }

        public async Task<List<ResponseBoardUser>> GetBoardUsersAsync(int boardId)
        {
            var board = await GetBoard(boardId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Read));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var users = await _userBase.GetAllAsync(u => u.BoardUsers.Any(bu => bu.BoardId == boardId));

            var mapedUsers = new List<ResponseBoardUser>();
            if(users != null)
            {
                foreach (var user in users)
                {
                    var role = user.BoardUsers.Where(id=>id.BoardId == boardId).Select(r=>r.Roles).First();
                    mapedUsers.Add(new ResponseBoardUser(user.Login)
                    {
                        Id = user.Id,
                        Email = user.Email,
                        AvatarPath = user.AvatarPath,
                        IsAdmin = role != Roles.User
                    });
                }
            }
            return mapedUsers;
        }

        public async Task AddUsersAsync(int boardId , int userId)
        {
            var isUserExist = await _userRepository.GetFirstAsync(i=>i.Id == userId);
            if (isUserExist == null)
                throw new ResourceNotFoundException("User don't exists");

            var board = await GetBoard(boardId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Create));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var newBoardUser = new BoardUser(boardId, userId)
            {
                Roles = Roles.User
            };
            await _boardUsersBaseRepository.AddAsync(newBoardUser);
        }

        public async Task RemoveUserAsync (int boardId, int userId)
        {
            var board = await GetBoard(boardId);
            var boardUserToRemove = await CheckBoardMembership(boardId, userId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Delete));
            if ((authorizationResult.Succeeded && boardUserToRemove.Roles !=Roles.Creator) || (_httpUserContextService.UserId == userId && boardUserToRemove.Roles !=Roles.Creator))
                await _boardUsersBaseRepository.DeleteAsync(boardUserToRemove);
            else
                throw new UnauthorizedException("Unauthorized");
        }

        public async Task UpdateBoardAsync(int boardId, UpdateBoardDto updateBoardDto)
        {
            var board = await GetBoard(boardId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Update));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            board.Name = updateBoardDto.Name;
            board.UpdatedAt = DateTime.Now;
            await _boardRepository.UpdateAsync(board);
        }

        public async Task SetUserRole (int boardId,int userId, int roles)
        {
            var board = await GetBoard(boardId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_httpUserContextService.User, board, new ResourceOperationRequirement(ResourceOperations.Update));
            if (!authorizationResult.Succeeded)
                throw new UnauthorizedException("Unauthorized");

            var boardUser = await CheckBoardMembership(boardId, userId);
            if (boardUser.Roles == Roles.Creator)
                throw new UnauthorizedException("Unauthorized");

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
                    throw new ConflictExceptions("Use 0 (Admin) or 1 (User) to set role");
            }
        }

    }
}

