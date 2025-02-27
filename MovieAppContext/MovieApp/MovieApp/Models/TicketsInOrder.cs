﻿using System.ComponentModel.DataAnnotations;

namespace MovieApp.Models
{
    public class TicketsInOrder
    {
        [Key]
        public Guid Id { get; set; }
        public Guid TicketId { get; set; }
        public Guid OrderId { get; set; }
        public Ticket? Ticket { get; set; }
        public Order? Order { get; set; }
        public int Quantity { get; set; }
    }
}
