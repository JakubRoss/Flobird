using Cabanoss.Core.Data;
using Cabanoss.Core.Data.Entities;

namespace Cabanoss.Core.Repositories.Impl
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private CabanossDbContext _context;

        public UserRepository(CabanossDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersAsync(string searchphrase)
        {
            var users = _context.Users.Where(x=>searchphrase == null || (x.Login.ToLower().Contains(searchphrase)) || x.Email.ToLower().Contains(searchphrase)).ToList();
            return users;
        }
    }
}
