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

            modelBuilder.Entity<BoardUser>()
                .HasKey(bu => new { bu.BoardId, bu.UserId });

            //modelBuilder.Entity<BoardUserTaskElement>()
            //    .HasKey(bu => new { bu.BoardUserId, bu.TaskElementId });

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

            modelBuilder.Entity<List>()
                .HasMany(c=>c.Cards)
                .WithOne(l=>l.List)
                .HasForeignKey(c=>c.ListId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Card>()
                .HasMany(t=>t.Tasks)
                .WithOne(c=>c.Card)
                .HasForeignKey(t=>t.CardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Card>()
                .HasMany(c => c.Comments)
                .WithOne(c => c.Card)
                .HasForeignKey(c => c.CardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(u => u.User)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Card>()
                .HasMany(a=>a.Attachments)
                .WithOne(c=>c.Card)
                .HasForeignKey(a=>a.CardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Attachment>()
                .HasOne(u => u.User)
                .WithMany(a => a.Attachments)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Tasks>()
                .HasMany(te=>te.Elements)
                .WithOne(t=>t.Task)
                .HasForeignKey(te=>te.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
