using Domain.Data.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories.Impl
{
    public class TasksRepository : Repository<Tasks>, ITasksRepository
    {
        public TasksRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
