using Domain.Data.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Impl
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context){}
        /// <summary>
        /// If we use GetUserAsync without the search phrase (null), we get the entire list of users.
        /// otherwise, if we enter the search phrase, we will get all users who contain the "search phrase" in their login or email
        /// </summary>
        /// <param name="searchPhrase"></param>
        /// <returns></returns>
        public async Task<List<User>> GetUsersAsync(string? searchPhrase)
        {
            var users = await this.GetAllAsync(x =>
                searchPhrase == null || x.Login.ToLower().Contains(searchPhrase) ||
                (x.Email != null && x.Email.Contains(searchPhrase)));
            return users;
        }
    }
}
