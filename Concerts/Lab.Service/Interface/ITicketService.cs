using Lab.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Service.Interface
{
    public interface ITicketService
    {
        public List<Ticket> ListAllTickets();
        public Ticket CreateNewTicket(Ticket t, string userId);
        public Ticket UpdateTicket(Ticket t);
        public Ticket DeleteTicket(Guid? Id);
        public Ticket GetTicketById(Guid? Id);
    }
}
