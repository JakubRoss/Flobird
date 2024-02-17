using Domain.Data.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories.Impl
{
    public class ElementUsersRepository : Repository<ElementUsers>, IElementUsersRepository
    {
        public ElementUsersRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
