using Application.Data;
using Application.Data.Entities;

namespace Application.Repositories.Impl
{
    public class ListRepository : BaseRepository<List>, IListRepository
    {
        public ListRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
