using Cabanoss.Core.Data.Entities;

namespace Cabanoss.Core.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<List<User>> GetUsersAsync(string searchphrase);
    }
}
