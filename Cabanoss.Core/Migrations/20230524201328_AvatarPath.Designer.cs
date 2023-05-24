﻿// <auto-generated />
using System;
using Cabanoss.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cabanoss.Core.Migrations
{
    [DbContext(typeof(CabanossDbContext))]
    [Migration("20230524201328_AvatarPath")]
    partial class AvatarPath
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("UserId");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Board", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkspaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkspaceId");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.BoardUser", b =>
                {
                    b.Property<int>("BoardId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Roles")
                        .HasColumnType("int");

                    b.HasKey("BoardId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("BoardsUser");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ListId");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Element", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Element");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.ElementUsers", b =>
                {
                    b.Property<int>("BoardUserId")
                        .HasColumnType("int");

                    b.Property<int>("ElementId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("BoardUserId", "ElementId");

                    b.HasIndex("ElementId");

                    b.ToTable("ElementUsers");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.List", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BoardId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Position")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("List");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Tasks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AvatarPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Workspace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Workspaces");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Attachment", b =>
                {
                    b.HasOne("Cabanoss.Core.Data.Entities.Card", "Card")
                        .WithMany("Attachments")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cabanoss.Core.Data.Entities.User", "User")
                        .WithMany("Attachments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Board", b =>
                {
                    b.HasOne("Cabanoss.Core.Data.Entities.Workspace", "Workspace")
                        .WithMany("Boards")
                        .HasForeignKey("WorkspaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.BoardUser", b =>
                {
                    b.HasOne("Cabanoss.Core.Data.Entities.Board", "Board")
                        .WithMany("BoardUsers")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cabanoss.Core.Data.Entities.User", "User")
                        .WithMany("BoardUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Board");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Card", b =>
                {
                    b.HasOne("Cabanoss.Core.Data.Entities.List", "List")
                        .WithMany("Cards")
                        .HasForeignKey("ListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("List");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Comment", b =>
                {
                    b.HasOne("Cabanoss.Core.Data.Entities.Card", "Card")
                        .WithMany("Comments")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cabanoss.Core.Data.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Element", b =>
                {
                    b.HasOne("Cabanoss.Core.Data.Entities.Tasks", "Task")
                        .WithMany("Elements")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.ElementUsers", b =>
                {
                    b.HasOne("Cabanoss.Core.Data.Entities.Element", "Element")
                        .WithMany("ElementUsers")
                        .HasForeignKey("ElementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cabanoss.Core.Data.Entities.BoardUser", "BoardUser")
                        .WithMany("ElementUsers")
                        .HasForeignKey("BoardUserId", "ElementId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BoardUser");

                    b.Navigation("Element");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.List", b =>
                {
                    b.HasOne("Cabanoss.Core.Data.Entities.Board", "Board")
                        .WithMany("Lists")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Tasks", b =>
                {
                    b.HasOne("Cabanoss.Core.Data.Entities.Card", "Card")
                        .WithMany("Tasks")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Workspace", b =>
                {
                    b.HasOne("Cabanoss.Core.Data.Entities.User", "User")
                        .WithOne("Workspace")
                        .HasForeignKey("Cabanoss.Core.Data.Entities.Workspace", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Board", b =>
                {
                    b.Navigation("BoardUsers");

                    b.Navigation("Lists");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.BoardUser", b =>
                {
                    b.Navigation("ElementUsers");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Card", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("Comments");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Element", b =>
                {
                    b.Navigation("ElementUsers");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.List", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Tasks", b =>
                {
                    b.Navigation("Elements");
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.User", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("BoardUsers");

                    b.Navigation("Comments");

                    b.Navigation("Workspace")
                        .IsRequired();
                });

            modelBuilder.Entity("Cabanoss.Core.Data.Entities.Workspace", b =>
                {
                    b.Navigation("Boards");
                });
#pragma warning restore 612, 618
        }
    }
}
