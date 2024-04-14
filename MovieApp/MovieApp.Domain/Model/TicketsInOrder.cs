using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.Model
{
    public class TicketsInOrder : BaseEntity
    {
        public Guid TicketId { get; set; }
        public Ticket? OrderedTicket { get; set; }
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public int Quantity { get; set; }
    }
}
