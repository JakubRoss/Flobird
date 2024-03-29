﻿namespace Domain.Data.Entities
{
    public class User
    {
        /// <summary>
        /// Ogólnie rzecz biorąc, zastosowanie new do właściwości Id może być interpretowane
        /// jako sposób na ukrycie tej właściwości, aby była ona dostępna tylko
        /// wewnętrznie, np. dla potrzeb EF i operacji bazodanowych, a nie dla użytkownika klasy. 
        /// </summary>
        public new int Id { get; set; }
        public string Login { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? AvatarPath { get; set; }

        //Navigation

        public  ICollection<BoardUser> BoardUsers { get; set; } = new List<BoardUser>();
        public  ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
        public ICollection<CardUser> CardUsers { get; set; } = new List<CardUser>();
        public ICollection<ElementUsers> ElementUsers { get; set; } = new List<ElementUsers>();
    }
}
