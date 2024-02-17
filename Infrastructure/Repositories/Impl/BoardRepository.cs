using Domain.Data.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories.Impl
{
    public class BoardRepository : Repository<Board>, IBoardRepository
    {
        public BoardRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
