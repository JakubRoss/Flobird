using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DatabaseSeeder
    {
        private readonly DatabaseContext _dbContext;
        public DatabaseSeeder(DatabaseContext dbContext)
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