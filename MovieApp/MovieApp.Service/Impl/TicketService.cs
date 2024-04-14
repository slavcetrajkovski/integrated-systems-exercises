using MovieApp.Domain.Identity;
using MovieApp.Domain.Model;
using MovieApp.Service.Interface;
using MovieAppRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Impl
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IUserRepository userRepository;

        public TicketService(IRepository<Ticket> ticketRepository, IUserRepository userRepository)
        {
            this.ticketRepository = ticketRepository;
            this.userRepository = userRepository;
        }

        public Ticket CreateNewTicket(Ticket t, string userId)
        {
            var createdBy = this.userRepository.Get(userId);
            t.CreatedBy = createdBy;
            return this.ticketRepository.Insert(t);
        }

        public Ticket DeleteTicket(Guid Id)
        {
            var ticket = this.GetTicketById(Id);
            return this.ticketRepository.Delete(ticket);
        }

        public Ticket GetTicketById(Guid? Id)
        {
            return this.ticketRepository.Get(Id);
        }

        public List<Ticket> ListAllTickets()
        {
            return this.ticketRepository.Include(t => t.Movie).ToList();
        }

        public Ticket UpdateTicket(Ticket t)
        {
            return this.ticketRepository.Update(t);
        }
    }
}
