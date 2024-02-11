using Application.Data;
using Application.Data.Entities;

namespace Application.Repositories.Impl
{
    public class BoardUsersRepository : BaseRepository<BoardUser>, IBoardUsersRepository
    {
        public BoardUsersRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
