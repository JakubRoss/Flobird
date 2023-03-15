using Cabanoss.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cabanoss.Core.Data
{
    public class CabanossDbContext :DbContext
    {
        public CabanossDbContext(DbContextOptions<CabanossDbContext> options) : base(options) { }

        public DbSet<Attachment> Attachment { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<BoardUser> BoardsUser { get; set; }
        public DbSet<BoardUserTaskElement> boardUserTaskElements { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Entities.Task> Tasks { get; set; }
        public DbSet<TaskElement> TaskElements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Set_Keys

            modelBuilder.Entity<Attachment>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Board>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<BoardUser>()
                .HasKey(bu => new { bu.BoardId, bu.UserId });

            modelBuilder.Entity<Card>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Comment>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<List>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<Entities.Task>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<TaskElement>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Workspace>()
                .HasKey(w => w.Id);

            modelBuilder.Entity<BoardUserTaskElement>()
                .HasKey(bute => new { bute.BoardUserId, bute.TaskElementId });

            #endregion
            #region Deleting_Behavior

            modelBuilder.Entity<BoardUser>()
                .HasOne(u => u.Board)
                .WithMany(b=> b.BoardUsers)
                .HasForeignKey(b => b.BoardId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BoardUser>()
                .HasOne(bu => bu.User)
                .WithMany(u=>u.BoardUsers)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Board>()
                .HasOne(b=>b.Workspace)
                .WithMany(w=>w.Boards)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BoardUserTaskElement>()
                .HasOne(bute => bute.TaskElement)
                .WithMany(te => te.BoardUsers)
                .HasForeignKey(bute => bute.TaskElementId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Workspace>()
                .HasMany(b=>b.Boards)
                .WithOne(w=>w.Workspace)
                .HasForeignKey(w=>w.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Board>()
                .HasOne(b => b.Workspace)
                .WithMany(w => w.Boards)
                .HasForeignKey(b => b.WorkspaceId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Board>()
                .HasMany(b => b.Lists)
                .WithOne(l => l.Board)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Board>()
                .HasMany(b => b.BoardUsers)
                .WithOne(bu => bu.Board)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Board>()
                .HasMany(b => b.Lists)
                .WithOne(a => a.Board)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<List>()
                .HasMany(l => l.Cards)
                .WithOne(c => c.List)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Card>()
                .HasMany(c => c.Comments)
                .WithOne(cmt => cmt.Card)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Card>()
                .HasMany(c => c.Attachments)
                .WithOne(at => at.Card)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Card>()
                .HasMany(t=>t.Tasks)
                .WithOne(c=>c.Card)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Entities.Task>()
                .HasMany(te=>te.TaskElements)
                .WithOne(t=>t.Task)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
