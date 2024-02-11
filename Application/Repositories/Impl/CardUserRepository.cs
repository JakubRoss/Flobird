using Application.Data;
using Application.Data.Entities;

namespace Application.Repositories.Impl
{
    public class CardUserRepository : BaseRepository<CardUser>, ICardUserRepository
    {
        public CardUserRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
