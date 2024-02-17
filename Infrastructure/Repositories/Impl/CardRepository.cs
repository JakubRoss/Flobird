using Domain.Data.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories.Impl
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        public CardRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
