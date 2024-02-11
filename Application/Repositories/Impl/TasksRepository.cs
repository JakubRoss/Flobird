using Application.Data;
using Application.Data.Entities;

namespace Application.Repositories.Impl
{
    public class TasksRepository : BaseRepository<Tasks>, ITasksRepository
    {
        public TasksRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
