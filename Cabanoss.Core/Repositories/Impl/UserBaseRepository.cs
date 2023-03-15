using Cabanoss.Core.Data;
using Cabanoss.Core.Data.Entities;

namespace Cabanoss.Core.Repositories.Impl
{
    public class UserBaseRepository : BaseRepository<User>, IUserBaseRepository
    {
        public UserBaseRepository(CabanossDbContext context) : base(context) { }
    }
}
