using Cabanoss.Core.Data;
using Cabanoss.Core.Data.Entities;

namespace Cabanoss.Core.Repositories.Impl
{
    public class ListRepository : BaseRepository<List>, IListRepository
    {
        public ListRepository(CabanossDbContext context) : base(context)
        {
        }
    }
}
