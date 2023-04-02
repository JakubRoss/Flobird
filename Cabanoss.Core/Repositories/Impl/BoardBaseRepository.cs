using Cabanoss.Core.Data;
using Cabanoss.Core.Data.Entities;

namespace Cabanoss.Core.Repositories.Impl
{
    public class BoardBaseRepository : BaseRepository<Board>, IBoardBaseRepository
    {
        public BoardBaseRepository(CabanossDbContext context) : base(context)
        {
        }
    }
}
