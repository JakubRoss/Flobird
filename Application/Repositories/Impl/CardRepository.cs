using Application.Data;
using Application.Data.Entities;

namespace Application.Repositories.Impl
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
