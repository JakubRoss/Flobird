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
            #region Deleting_Behaviour

            modelBuilder.Entity<User>()
                .HasOne(u => u.Workspace)
                .WithOne(w => w.User)
                .HasForeignKey<Workspace>(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Workspace>()
                .HasMany(w => w.Boards)
                .WithOne(b => b.Workspace)
                .HasForeignKey(b => b.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Board>()
                .HasMany(b => b.BoardUsers)
                .WithOne(bu => bu.Board)
                .HasForeignKey(bu => bu.BoardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BoardUser>()
                .HasOne(u => u.User)
                .WithMany(bu => bu.BoardUsers)
                .HasForeignKey(bu => bu.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Board>()
                .HasMany(l=>l.Lists)
                .WithOne(b=>b.Board)
                .HasForeignKey (l=>l.BoardId)
                .OnDelete (DeleteBehavior.Cascade);

            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
