using Cabanoss.Core.Data;
using Cabanoss.Core.Data.Entities;

namespace Cabanoss.Core.Repositories.Impl
{
    public class BoardRepository : BaseRepository<Board>, IBoardRepository
    {
        public BoardRepository(CabanossDbContext context) : base(context)
        {
        }
    }
}
