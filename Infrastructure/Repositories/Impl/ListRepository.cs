using Domain.Data.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories.Impl
{
    public class ListRepository : Repository<List>, IListRepository
    {
        public ListRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
