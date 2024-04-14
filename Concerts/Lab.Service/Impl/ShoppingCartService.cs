using Lab.Model.Domain;
using Lab.Model.DTO;
using Lab.Repository.Interface;
using Lab.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Service.Impl
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> shoppingCartRepository;
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IUserRepository userRepository;
        private readonly IRepository<Concert> concertRepository;
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<TicketsInOrder> ticketsInOrderRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<Ticket> ticketRepository,
            IUserRepository userRepository, IRepository<Concert> concertRepository,
            IRepository<Order> orderRepository, IRepository<TicketsInOrder> ticketsInOrderRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.ticketRepository = ticketRepository;
            this.userRepository = userRepository;
            this.concertRepository = concertRepository;
            this.orderRepository = orderRepository;
            this.ticketsInOrderRepository = ticketsInOrderRepository;
        }

        public ShoppingCart AddTicketToShoppingCart(string userId, AddTicketToCart model)
        {
            if (userId != null)
            {
                var loggedInUser = this.userRepository.Get(userId);

                var userCart = loggedInUser?.UserCart;

                var selectedTicket = this.ticketRepository.Get(model.TicketId);

                if (selectedTicket != null && userCart != null)
                {
                    userCart?.TicketsInShoppingCart?.Add(new TicketsInShoppingCart
                    {
                        TicketId = selectedTicket.Id,
                        ShoppingCartId = userCart.Id,
                        Ticket = selectedTicket,
                        ShoppingCart = userCart,
                        NumberOfPeople = model.NumberOfPeople
                    });

                    return this.shoppingCartRepository.Update(userCart);
                }
            }
            return null;
        }

        public bool deleteFromShoppingCart(string userId, Guid? id)
        {
            if (userId != null)
            {
                var loggedInUser = this.userRepository.Get(userId);

                var ticketToDelete = loggedInUser?.UserCart.TicketsInShoppingCart.
                    First(u => u.TicketId == id);

                loggedInUser?.UserCart?.TicketsInShoppingCart?.Remove(ticketToDelete);

                this.shoppingCartRepository.Update(loggedInUser.UserCart);

                return true;
            }
            return false;
        }

        public ShoppingCartDTO getShoppingCartDetails(string userId)
        {
            if (userId != null)
            {
                var loggedInUser = this.userRepository.Get(userId);

                var allTickets = loggedInUser?.UserCart?.TicketsInShoppingCart?.ToList();

                int totalPrice = 0;

                foreach (var item in allTickets)
                {
                    var ticket = this.ticketRepository.Get(item.TicketId);
                    var concert = this.concertRepository.Get(ticket.ConcertId);
                    totalPrice += item.NumberOfPeople * concert.ConcertPrice;
                }

                var model = new ShoppingCartDTO
                {
                    inCartTickets = allTickets,
                    TotalPrice = totalPrice
                };

                return model;
            }

            return new ShoppingCartDTO
            {
                inCartTickets = new List<TicketsInShoppingCart>(),
                TotalPrice = 0
            };
        }

        public AddTicketToCart getTicketInfo(Guid? id)
        {
            var ticket = this.ticketRepository.Get(id);
            var concert = this.concertRepository.Get(ticket.ConcertId);

            if(ticket != null)
            {
                var model = new AddTicketToCart
                {
                    SelectedConcert = concert?.ConcertName,
                    TicketId = ticket.Id,
                    NumberOfPeople = ticket.NumberOfPeople,
                    SelectedConcertPrice = concert.ConcertPrice
                };
                return model;
            }
            return null;
        }

        public bool orderTicket(string userId)
        {
            if (userId != null)
            {
                var loggedInUser = this.userRepository.Get(userId);

                var userCart = loggedInUser?.UserCart;

                var userOrder = new Order
                {
                    Id = Guid.NewGuid(),
                    OwnerId = userId,
                    Owner = loggedInUser
                };

                this.orderRepository.Insert(userOrder);

                var ticketsInOrder = userCart?.TicketsInShoppingCart
                    .Select(u => new TicketsInOrder
                    {
                        Order = userOrder,
                        OrderId = userOrder.Id,
                        Ticket = u.Ticket,
                        TicketId = u.TicketId,
                        NumberOfPeople = u.NumberOfPeople
                    }).ToList();

                this.ticketsInOrderRepository.InsertMany(ticketsInOrder);

                userCart?.TicketsInShoppingCart.Clear();

                this.shoppingCartRepository.Update(userCart);

                return true;
            }
            return false;
        }
    }
}