using Domain.Data.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories.Impl
{
    public class ElementRepository : Repository<Element>, IElementRepository
    {
        public ElementRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
