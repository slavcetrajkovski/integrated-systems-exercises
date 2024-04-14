﻿using System.ComponentModel.DataAnnotations;

namespace MovieApp.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string? OwnerId { get; set; }
        public EShopApplicationUser? Owner { get; set; }
        public virtual ICollection<TicketsInOrder>? TicketsInOrders { get; set; }
    }
}
