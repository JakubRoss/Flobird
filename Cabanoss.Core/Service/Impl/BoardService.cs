using AutoMapper;
using Cabanoss.Core.Common;
using Cabanoss.Core.Data;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Exceptions;
using Cabanoss.Core.Model.Board;
using Cabanoss.Core.Repositories;
using Cabanoss.Core.Repositories.Impl;

namespace Cabanoss.Core.Service.Impl
{
    public class BoardService : IBoardService
    {
        private IBoardBaseRepository _boardBaseRepository;
        private IMapper _mapper;
        private IBoardUsersBaseRepository _boardUsersBaseRepository;
        private IUserBaseRepository _userBase;
        private CabanossDbContext _dbContext;
        private WorkspaceService _workspaceService;

        public BoardService(IBoardBaseRepository boardBaseRepository,
            IMapper mapper,
            IBoardUsersBaseRepository boardUsersBaseRepository,
            IUserBaseRepository userBase)
        {
            _boardBaseRepository = boardBaseRepository;
            _mapper = mapper;
            _boardUsersBaseRepository = boardUsersBaseRepository;
            _userBase = userBase;
        }
        private async System.Threading.Tasks.Task<User> GetUserById(int id)
        {
            var user = await _userBase.GetFirstAsync(p => p.Id == id);
            if (user == null)
                throw new ResourceNotFoundException("User don't exists");
            return user;
        }

        public async System.Threading.Tasks.Task CreateBoardAsync(int id, CreateBoardDto createBoardDto)
        {
            var user = await GetUserById(id);
            var board = _mapper.Map<Board>(createBoardDto);
            board.CreatedAt = DateTime.Now;
            board.WorkspaceId = _dbContext.Workspaces.FirstOrDefault(p => p.UserId == user.Id).Id;
            var sboard = await _boardBaseRepository.AddAsync(board);
            var newBoardUser = new BoardUser { BoardId = sboard.Id, UserId = user.Id };
            newBoardUser.Role = Roles.Admin.ToString();
            await _boardUsersBaseRepository.AddAsync(newBoardUser);
        }

        public async System.Threading.Tasks.Task DeleteBoardAsync(int id)
        {
            var board = await _boardBaseRepository.GetFirstAsync(p => p.Id == id);
            await _boardBaseRepository.DeleteAsync(board);
        }

        public async System.Threading.Tasks.Task<List<ResponseBoardDto>> GetBoardsAsync(int id)
        {
            var user = await GetUserById(id);

            var userboards = await _boardUsersBaseRepository.GetAllAsync(p=>p.UserId == user.Id);
            var boards = new List<Board>();
            foreach (var board in userboards)
            {
                var cos = await _boardBaseRepository.GetFirstAsync(p=>p.Id == board.BoardId);
                boards.Add(cos);
            }
            var responseBoards = _mapper.Map<List<ResponseBoardDto>>(boards);
            return responseBoards;
        }

        public async System.Threading.Tasks.Task<List<ResponseBoardUser>> GetUsersAsync(int id)
        {
            var users = await _userBase.GetAllAsync(u => u.BoardUsers.Any(bu => bu.BoardId == id));
            var mapedUsers = _mapper.Map<List<ResponseBoardUser>>(users);
            return mapedUsers;
        }

        public async System.Threading.Tasks.Task AddUsersAsync(int boardId , int userId)
        {
            var newBoardUser = new BoardUser { BoardId = boardId, UserId = userId };
            newBoardUser.Role = Roles.User.ToString();
            await _boardUsersBaseRepository.AddAsync(newBoardUser);
        }

        public async System.Threading.Tasks.Task RemoveUserAsync (int boardId, int userId)
        {
            var boardUser = await _boardUsersBaseRepository.GetFirstAsync(i=>i.BoardId == boardId&& i.UserId ==userId);
            if (boardUser != null)
                await _boardUsersBaseRepository.DeleteAsync(boardUser);
        }

        public async System.Threading.Tasks.Task ModifyNameBoardAsync(int id, UpdateBoardDto updateBoardDto)
        {
            var board = await _boardBaseRepository.GetFirstAsync(i=>i.Id == id);
            board.Name = updateBoardDto.Name;
            board.UpdatedAt = DateTime.Now;
            _dbContext.SaveChanges();
        }

    }
}

