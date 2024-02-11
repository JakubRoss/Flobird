using Application.Data.Entities;

namespace Application.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<List<User>> GetUsersAsync(string searchPhrase);
    }
}
