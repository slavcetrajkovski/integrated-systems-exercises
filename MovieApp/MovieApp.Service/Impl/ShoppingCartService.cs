using MovieApp.Domain.DTO;
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
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUserRepository userRepository;
        private readonly IRepository<ShoppingCart> shoppingCartRepository;
        private readonly IRepository<Ticket> ticketRepository;

        public ShoppingCartService(IUserRepository userRepository, IRepository<ShoppingCart> shoppingCartRepository, IRepository<Ticket> ticketRepository)
        {
            this.userRepository = userRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.ticketRepository = ticketRepository;
        }

        public bool deleteFromShoppingCart(string userId, Guid? Id)
        {
            throw new NotImplementedException();
        }

        public AddToShoppingCartDTO getTicketInfo(Guid Id)
        {
            throw new NotImplementedException();
        }

        public ShoppingCartDTO getShoppingCartDetails(string userId)
        {
            throw new NotImplementedException();
        }

        public bool orderProducts(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
