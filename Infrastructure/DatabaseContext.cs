using Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<BoardUser> BoardUsers { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardUser> CardUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<ElementUsers> ElementUsers { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<User> Users { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region lengthProperty

            modelBuilder.Entity<User>()
                .Property(p => p.Login)
                .HasMaxLength(20)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(p => p.PasswordHash)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder.Entity<Board>()
                .Property(p => p.Name)
                .HasMaxLength(25)
                .IsRequired();

            modelBuilder.Entity<List>()
                .Property(p => p.Name)
                .HasMaxLength(25)
                .IsRequired();

            modelBuilder.Entity<Card>()
                .Property(p => p.Name)
                .HasMaxLength(25)
                .IsRequired();

            modelBuilder.Entity<Card>()
                .Property(p => p.Description)
                .HasMaxLength(250);

            modelBuilder.Entity<Tasks>()
                .Property(p => p.Name)
                .HasMaxLength(25)
                .IsRequired();

            modelBuilder.Entity<Element>()
                .Property(p => p.Description)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Comment>()
                .Property(p => p.Text)
                .HasMaxLength(250)
                .IsRequired();
            #endregion

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
                .HasMany(eu => eu.ElementUsers)
                .WithOne(u => u.User)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(c => c.Comments)
                .WithOne(u => u.User)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(cu => cu.CardUsers)
                .WithOne(u => u.User)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(bu => bu.BoardUsers)
                .WithOne(u => u.User)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(a => a.Attachments)
                .WithOne(u => u.User)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region ElementUsers_Relations
            modelBuilder.Entity<ElementUsers>()
                .HasOne(u => u.User)
                .WithMany(eu => eu.ElementUsers)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ElementUsers>()
                .HasOne(e => e.Element)
                .WithMany(eu => eu.ElementUsers)
                .HasForeignKey(k => k.ElementId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region Comment_relations
            modelBuilder.Entity<Comment>()
                .HasOne(u => u.User)
                .WithMany(c => c.Comments)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Card)
                .WithMany(c => c.Comments)
                .HasForeignKey(k => k.CardId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region CardUser_relations
            modelBuilder.Entity<CardUser>()
                .HasOne(u => u.User)
                .WithMany(cu => cu.CardUsers)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CardUser>()
                .HasOne(c => c.Card)
                .WithMany(cu => cu.CardUsers)
                .HasForeignKey(k => k.CardId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region BoardUser_Relations
            modelBuilder.Entity<BoardUser>()
                .HasOne(u => u.User)
                .WithMany(bu => bu.BoardUsers)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BoardUser>()
                .HasOne(b => b.Board)
                .WithMany(bu => bu.BoardUsers)
                .HasForeignKey(k => k.BoardId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region Attachment_Relations
            modelBuilder.Entity<Attachment>()
                .HasOne(u => u.User)
                .WithMany(a => a.Attachments)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Attachment>()
                .HasOne(c => c.Card)
                .WithMany(a => a.Attachments)
                .HasForeignKey(k => k.CardId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region Element_Relations
            modelBuilder.Entity<Element>()
                .HasMany(eu => eu.ElementUsers)
                .WithOne(e => e.Element)
                .HasForeignKey(e => e.ElementId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Element>()
                .HasOne(t => t.Task)
                .WithMany(e => e.Elements)
                .HasForeignKey(k => k.TaskId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region Tasks_Relations
            modelBuilder.Entity<Tasks>()
                .HasMany(e => e.Elements)
                .WithOne(t => t.Task)
                .HasForeignKey(k => k.TaskId)
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
                .HasOne(l => l.List)
                .WithMany(c => c.Cards)
                .HasForeignKey(k => k.ListId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region List_Relations
            modelBuilder.Entity<List>()
                .HasMany(c => c.Cards)
                .WithOne(l => l.List)
                .HasForeignKey(k => k.ListId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<List>()
                .HasOne(b => b.Board)
                .WithMany(l => l.Lists)
                .HasForeignKey(k => k.BoardId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region Boards_Relations
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
