using Application.Data;
using Application.Data.Entities;

namespace Application.Repositories.Impl
{
    public class ElementUsersRepository : BaseRepository<ElementUsers>, IElementUsersRepository
    {
        public ElementUsersRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
