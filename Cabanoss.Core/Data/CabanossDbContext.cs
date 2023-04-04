using Cabanoss.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cabanoss.Core.Data
{
    public class CabanossDbContext :DbContext
    {
        public CabanossDbContext(DbContextOptions<CabanossDbContext> options) : base(options) { }

        public DbSet<Board> Boards { get; set; }
        public DbSet<BoardUser> BoardsUser { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Set_Keys

            modelBuilder.Entity<Board>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<BoardUser>()
                .HasKey(bu => new { bu.BoardId, bu.UserId });

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Workspace>()
                .HasKey(w => w.Id);

            #endregion
            #region Deleting_Behavior

            modelBuilder.Entity<User>()
                .HasOne(e => e.Workspace)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Workspace>()
                .HasMany(e=>e.Boards)
                .WithOne(e=>e.Workspace)
                .HasForeignKey(k=>k.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade);

            // CONF BOARD <-> BOARDUSER
            modelBuilder.Entity<BoardUser>()
                .HasOne(e => e.Board)
                .WithMany(e => e.BoardUsers)
                .HasForeignKey(k => k.BoardId)
                .OnDelete(DeleteBehavior.Cascade); //gdy usuwam Board usuwam powiazane z nim BoardUser

            modelBuilder.Entity<Board>()
                .HasMany(e=>e.BoardUsers)
                .WithOne(e=>e.Board)
                .HasForeignKey(k=>k.BoardId)
                .OnDelete(DeleteBehavior.NoAction); //gdy usuwam BoardUser nie usuwam powiazanego z nim Board

            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
