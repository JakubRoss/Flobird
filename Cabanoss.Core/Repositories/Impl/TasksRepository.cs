using Cabanoss.Core.Data;
using Cabanoss.Core.Data.Entities;

namespace Cabanoss.Core.Repositories.Impl
{
    public class TasksRepository : BaseRepository<Tasks>, ITasksRepository
    {
        public TasksRepository(CabanossDbContext context) : base(context)
        {
        }
    }
}
