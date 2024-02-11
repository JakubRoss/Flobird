using Application.Data;
using Application.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories.Impl
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersAsync(string searchPhrase)
        {
            var users =  await _context.Users.Where(x=>searchPhrase == null || (x.Login.ToLower().Contains(searchPhrase)) || x.Email.ToLower().Contains(searchPhrase)).ToListAsync();
            return users;
        }
    }
}
