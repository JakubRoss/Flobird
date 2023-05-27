﻿namespace Cabanoss.Core.Data.Entities
{
    public class Comment : BaseEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime? CreatedAt { get; set; }

        //navigation
        public int UserId { get; set; }
        public User User { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }
    }
}