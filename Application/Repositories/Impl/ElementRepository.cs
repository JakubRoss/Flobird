using Application.Data;
using Application.Data.Entities;

namespace Application.Repositories.Impl
{
    public class ElementRepository : BaseRepository<Element>, IElementRepository
    {
        public ElementRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
