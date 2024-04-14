using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Model.Domain
{
    public class TicketsInOrder : BaseEntity
    {
        public Guid TicketId { get; set; }
        public Guid OrderId { get; set; }
        public Ticket? Ticket { get; set; }
        public Order? Order { get; set; }
        public int NumberOfPeople { get; set; }
    }
}
