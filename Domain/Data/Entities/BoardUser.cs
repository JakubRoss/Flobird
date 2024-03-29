﻿using Domain.Common;

namespace Domain.Data.Entities
{
    public class BoardUser
    {
        public virtual Roles Roles { get; set; }

        //Navigation
        public int BoardId { get; set; }
        public virtual Board Board { get; set; } = null!;

        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}