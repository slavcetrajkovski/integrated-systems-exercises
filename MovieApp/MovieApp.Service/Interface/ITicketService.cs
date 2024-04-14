using MovieApp.Domain.DTO;
using MovieApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Interface
{
    public interface ITicketService
    {
        public List<Ticket> ListAllTickets();
        public Ticket GetTicketById(Guid? Id);
        public Ticket CreateNewTicket(Ticket t, string userId);
        public Ticket UpdateTicket(Ticket t);
        public Ticket DeleteTicket(Guid Id);
    }
}
