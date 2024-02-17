using Domain.Data.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories.Impl
{
    public class CardUserRepository : Repository<CardUser>, ICardUserRepository
    {
        public CardUserRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
