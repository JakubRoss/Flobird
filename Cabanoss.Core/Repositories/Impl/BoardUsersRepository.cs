using Cabanoss.Core.Data;
using Cabanoss.Core.Data.Entities;

namespace Cabanoss.Core.Repositories.Impl
{
    public class BoardUsersRepository : BaseRepository<BoardUser>, IBoardUsersRepository
    {
        public BoardUsersRepository(CabanossDbContext context) : base(context)
        {
        }
    }
}
