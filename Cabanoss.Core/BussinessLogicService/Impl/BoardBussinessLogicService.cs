using AutoMapper;
using Cabanoss.Core.Common;
using Cabanoss.Core.Data;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Exceptions;
using Cabanoss.Core.Model.Board;
using Cabanoss.Core.Repositories;

namespace Cabanoss.Core.BussinessLogicService.Impl
{
    public class BoardBussinessLogicService : IBoardBussinessLogicService
    {
        private IBoardBaseRepository _boardBaseRepository;
        private IMapper _mapper;
        private IBoardUsersBaseRepository _boardUsersBaseRepository;
        private IUserBaseRepository _userBase;
        private CabanossDbContext _dbContext;

        public BoardBussinessLogicService(IBoardBaseRepository boardBaseRepository,
            IMapper mapper,
            IBoardUsersBaseRepository boardUsersBaseRepository,
            IUserBaseRepository userBase,
            CabanossDbContext dbContext)
        {
            _boardBaseRepository = boardBaseRepository;
            _mapper = mapper;
            _boardUsersBaseRepository = boardUsersBaseRepository;
            _userBase = userBase;
            _dbContext = dbContext;
        }
        private async System.Threading.Tasks.Task<User> GetUser(string login)
        {
            var user = await _userBase.GetFirstAsync(u => u.Login.ToLower() == login.ToLower());
            if (user == null)
                throw new ResourceNotFoundException("Uzytkownik nie istnieje");
            return user;
        }

        public async System.Threading.Tasks.Task CreateBoardAsync(string login, CreateBoardDto createBoardDto)
        {
            var user = GetUser(login).Result;
            var board = _mapper.Map<Board>(createBoardDto);
            board.CreatedAt = DateTime.Now;
            board.WorkspaceId =_dbContext.Workspaces.FirstOrDefault(p => p.UserId == user.Id).Id;
            var sboard = await _boardBaseRepository.AddAsync(board);
            var newBoardUser = new BoardUser { BoardId = sboard.Id, UserId = user.Id };
            newBoardUser.Role = Roles.User.ToString();
            await _boardUsersBaseRepository.AddAsync(newBoardUser);
            await _dbContext.SaveChangesAsync();
        }

    }
}

