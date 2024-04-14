using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieAppRepository;
using MovieApp.Domain.Model;
using MovieApp.Domain.Identity;
using MovieApp.Domain.DTO;
using MovieApp.Service.Interface;

namespace MovieApp.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITicketService ticketService;
        private readonly IMovieService movieService;

        public TicketsController(ITicketService ticketService, IMovieService movieService)
        {
            this.ticketService = ticketService;
            this.movieService = movieService;
        }

        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = this.ticketService.ListAllTickets();
            return View(tickets);
        }

        // GET: Tickets/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = this.ticketService.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(this.movieService.ListAllMovies(), "Id", "MovieName");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("Id,Price,MovieId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                this.ticketService.CreateNewTicket(ticket, loggedInUser);
                return RedirectToAction(nameof(Index));   
            }
            ViewData["MovieId"] = new SelectList(this.movieService.ListAllMovies(), "Id", "MovieName", ticket.MovieId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = this.ticketService.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(this.movieService.ListAllMovies(), "Id", "MovieName", ticket.MovieId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, [Bind("Id,Price,MovieId")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.ticketService.UpdateTicket(ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(this.movieService.ListAllMovies(), "Id", "MovieName", ticket.MovieId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = this.ticketService.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            this.ticketService.DeleteTicket(id);

            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(Guid id)
        {
            return this.ticketService.GetTicketById(id) != null;
        }

        public ActionResult AddTicketToShoppingCart(Guid? id)
        {
            var ticket = this.ticketService.GetTicketById(id);
            var movie = this.movieService.GetMovieById(ticket.MovieId);

            if(ticket != null) 
            {
                var model = new AddToShoppingCartDTO
                {
                    SelectedMovie = movie?.MovieName,
                    TicketId = ticket.Id,
                    Quantity = 1
                };

            return View(model);
            }
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> AddTicketToShoppingCart([Bind("TicketId", "Quantity")] AddToShoppingCartDTO item)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    if (userId == null)
        //    {
        //        var loggedInUser = await _context.Users
        //            .Include("UserCart")
        //            .Include("UserCart.TicketsInShoppingCarts")
        //            .FirstOrDefaultAsync(u => u.Id == userId);

        //        var userCart = loggedInUser?.UserCart;

        //        var selectedTicket = await _context.Tickets.FirstOrDefaultAsync
        //            (t => t.Id == item.TicketId);

        //        if (selectedTicket != null && userCart != null)
        //        {
        //            userCart.TicketsInShoppingCarts.Add(new TicketsInShoppingCart
        //            {
        //                TicketId = selectedTicket.Id,
        //                Ticket = selectedTicket,
        //                ShoppingCart = userCart,
        //                ShoppingCartId = userCart.Id,
        //                Quantity = item.Quantity
        //            });

        //            _context.Update(userCart);

        //            _context.SaveChanges();

        //            return RedirectToAction("Index", "ShoppingCarts");
        //        }
        //    }
        //    return View(item);
        //}
   }
}
