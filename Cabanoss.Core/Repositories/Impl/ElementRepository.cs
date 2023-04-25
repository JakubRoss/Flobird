using Cabanoss.Core.Data;
using Cabanoss.Core.Data.Entities;

namespace Cabanoss.Core.Repositories.Impl
{
    public class ElementRepository : BaseRepository<Element>, IElementRepository
    {
        public ElementRepository(CabanossDbContext context) : base(context)
        {
        }
    }
}
