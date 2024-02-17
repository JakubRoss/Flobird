using Domain.Data.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetUsersAsync(string? searchPhrase);
    }
}
