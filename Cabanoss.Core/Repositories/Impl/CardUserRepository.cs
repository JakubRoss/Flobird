using Cabanoss.Core.Data;
using Cabanoss.Core.Data.Entities;

namespace Cabanoss.Core.Repositories.Impl
{
    public class CardUserRepository : BaseRepository<CardUser>, ICardUserRepository
    {
        public CardUserRepository(CabanossDbContext context) : base(context)
        {
        }
    }
}
