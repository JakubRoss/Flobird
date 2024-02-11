using Application.Data;
using Application.Data.Entities;

namespace Application.Repositories.Impl
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context) : base(context) 
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
