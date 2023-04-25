using Cabanoss.Core.Data;
using Cabanoss.Core.Data.Entities;

namespace Cabanoss.Core.Repositories.Impl
{
    public class ElementUsersRepository : BaseRepository<ElementUsers>, IElementUsersRepository
    {
        public ElementUsersRepository(CabanossDbContext context) : base(context)
        {
        }
    }
}
