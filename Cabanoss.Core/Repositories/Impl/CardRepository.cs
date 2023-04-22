using Cabanoss.Core.Data;
using Cabanoss.Core.Data.Entities;

namespace Cabanoss.Core.Repositories.Impl
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(CabanossDbContext context) : base(context)
        {
        }
    }
}
