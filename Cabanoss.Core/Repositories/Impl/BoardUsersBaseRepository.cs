using Cabanoss.Core.Data;
using Cabanoss.Core.Data.Entities;

namespace Cabanoss.Core.Repositories.Impl
{
    public class BoardUsersBaseRepository : BaseRepository<BoardUser>, IBoardUsersBaseRepository
    {
        public BoardUsersBaseRepository(CabanossDbContext context) : base(context)
        {
        }
    }
}
