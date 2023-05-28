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

            modelBuilder.Entity<ElementUsers>()
                .HasKey(bu => new { bu.UserId, bu.ElementId });

            modelBuilder.Entity<CardUser>()
                .HasKey(bu => new { bu.UserId, bu.CardId });

            #endregion

            #region Deleting_Behaviour

            #region User_relations
            modelBuilder.Entity<User>()
                .HasMany(eu=>eu.ElementUsers)
                .WithOne(u=>u.User)
                .HasForeignKey(k=>k.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(c=>c.Comments)
                .WithOne(u=>u.User)
                .HasForeignKey(k=>k.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(cu=>cu.CardUsers)
                .WithOne(u=>u.User)
                .HasForeignKey(k=>k.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(bu=>bu.BoardUsers)
                .WithOne(u=>u.User)
                .HasForeignKey(k=>k.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(ws=>ws.Workspace)
                .WithOne(u=>u.User)
                .HasForeignKey<Workspace>(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(a=>a.Attachments)
                .WithOne(u=>u.User)
                .HasForeignKey(k=>k.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region ElementUsers_Relations
            modelBuilder.Entity<ElementUsers>()
                .HasOne(u => u.User)
                .WithMany(eu => eu.ElementUsers)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ElementUsers>()
                .HasOne(e=>e.Element)
                .WithMany(eu=>eu.ElementUsers)
                .HasForeignKey(k=>k.ElementId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region Comment_relations
            modelBuilder.Entity<Comment>()
                .HasOne(u => u.User)
                .WithMany(c => c.Comments)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c=>c.Card)
                .WithMany(c => c.Comments)
                .HasForeignKey(k => k.CardId) 
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region CardUser_relations
            modelBuilder.Entity<CardUser>()
                .HasOne(u => u.User)
                .WithMany(cu=>cu.CardUsers)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CardUser>()
                .HasOne(c => c.Card)
                .WithMany(cu=>cu.CardUsers)
                .HasForeignKey(k => k.CardId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region BoardUser_Relations
            modelBuilder.Entity<BoardUser>()
                .HasOne(u => u.User)
                .WithMany(bu=>bu.BoardUsers)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BoardUser>()
                .HasOne(b=>b.Board)
                .WithMany(bu=>bu.BoardUsers)
                .HasForeignKey(k=>k.BoardId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region WorkSpace_Relations
            modelBuilder.Entity<Workspace>()
                .HasOne(u => u.User)
                .WithOne(w => w.Workspace)
                .HasForeignKey<Workspace>(k => k.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Workspace>()
                .HasMany(b=>b.Boards)
                .WithOne(w=>w.Workspace)
                .HasForeignKey(k=>k.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Attachment_Relations
            modelBuilder.Entity<Attachment>()
                .HasOne(u => u.User)
                .WithMany(a => a.Attachments)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Attachment>()
                .HasOne(c=>c.Card)
                .WithMany(a=>a.Attachments)
                .HasForeignKey(k => k.CardId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region Element_Relations
            modelBuilder.Entity<Element>()
                .HasMany(eu=>eu.ElementUsers)
                .WithOne(e=>e.Element)
                .HasForeignKey(e=>e.ElementId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Element>()
                .HasOne(t=>t.Task)
                .WithMany(e=>e.Elements)
                .HasForeignKey(k=>k.TaskId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region Tasks_Relations
            modelBuilder.Entity<Tasks>()
                .HasMany(e=>e.Elements)
                .WithOne(t=>t.Task)
                .HasForeignKey(k=>k.TaskId) 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tasks>()
                .HasOne(c => c.Card)
                .WithMany(t => t.Tasks)
                .HasForeignKey(k => k.CardId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region Card_Relations
            modelBuilder.Entity<Card>()
                .HasMany(cu => cu.CardUsers)
                .WithOne(c => c.Card)
                .HasForeignKey(k => k.CardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Card>()
                .HasMany(c => c.Comments)
                .WithOne(c => c.Card)
                .HasForeignKey(k => k.CardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Card>()
                .HasMany(t => t.Tasks)
                .WithOne(c => c.Card)
                .HasForeignKey(k => k.CardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Card>()
                .HasMany(a => a.Attachments)
                .WithOne(c => c.Card)
                .HasForeignKey(k => k.CardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Card>()
                .HasOne(l=>l.List)
                .WithMany(c=>c.Cards)
                .HasForeignKey(k=>k.ListId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region List_Relations
            modelBuilder.Entity<List>()
                .HasMany(c => c.Cards)
                .WithOne(l => l.List)
                .HasForeignKey(k => k.ListId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<List>()
                .HasOne(b=>b.Board)
                .WithMany(l=>l.Lists)
                .HasForeignKey(k=>k.BoardId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region Boards_Relations
            modelBuilder.Entity<Board>()
                .HasOne(w=>w.Workspace)
                .WithMany(b=>b.Boards)
                .HasForeignKey(k=>k.WorkspaceId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Board>()
                .HasMany(bu => bu.BoardUsers)
                .WithOne(b => b.Board)
                .HasForeignKey(k => k.BoardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Board>()
                .HasMany(l => l.Lists)
                .WithOne(b => b.Board)
                .HasForeignKey(k => k.BoardId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
