using Cabanoss.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Cabanoss.Core.Common
{
    public class CabanossSeeder
    {
        private readonly CabanossDbContext _dbContext;
        public CabanossSeeder(CabanossDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {

            if (_dbContext.Database.CanConnect())
            {
                var pendingMigration = _dbContext.Database.GetPendingMigrations();
                if (pendingMigration != null && pendingMigration.Any())
                {
                    _dbContext.Database.Migrate();
                }
            }

        }

    }
}