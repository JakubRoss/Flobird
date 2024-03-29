﻿namespace Domain.Data.Entities
{
    public class ElementUsers
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int ElementId { get; set; }
        public Element Element { get; set; } = null!;
    }
}
