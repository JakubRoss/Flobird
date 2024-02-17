using Domain.Data.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories.Impl
{
    public class BoardUsersRepository : Repository<BoardUser>, IBoardUsersRepository
    {
        public BoardUsersRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
