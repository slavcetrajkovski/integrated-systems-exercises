using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab.Model.Domain;
using Lab.Model.DTO;
using System.Security.Claims;
using Lab.Repository;
using Lab.Service.Interface;

namespace lab1.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService ticketService;
        private readonly IConcertService concertService;
        private readonly IShoppingCartService shoppingCartService;

        public TicketsController(ITicketService ticketService, IConcertService concertService,
            IShoppingCartService shoppingCartService)
        {
            this.ticketService = ticketService;
            this.concertService = concertService;
            this.shoppingCartService = shoppingCartService;
        }


        // GET: Tickets
        public IActionResult Index()
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
        public IActionResult Create()
        {
            ViewData["ConcertId"] = new SelectList(this.concertService.ListAllConcerts(), "Id", "ConcertName");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,NumberOfPeople,ConcertId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                this.ticketService.CreateNewTicket(ticket, loggedInUser);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConcertId"] = new SelectList(this.concertService.ListAllConcerts(), "Id", "ConcertName", ticket.ConcertId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public IActionResult Edit(Guid? id)
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
            ViewData["ConcertId"] = new SelectList(this.concertService.ListAllConcerts(), "Id", "ConcertName", ticket.ConcertId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,NumberOfPeople")] Ticket ticket)
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
            ViewData["ConcertId"] = new SelectList(this.concertService.ListAllConcerts(), "Id", "ConcertName", ticket.ConcertId);
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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            this.ticketService.DeleteTicket(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(Guid id)
        {
            return this.ticketService.GetTicketById(id) != null;
        }

        public async Task<IActionResult> AddTicketToCart(Guid? id)
        {
            var ticket = this.shoppingCartService.getTicketInfo(id);
            if(ticket != null) 
            {
                return View(ticket);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTicketToCart([Bind("TicketId", "NumberOfPeople")] AddTicketToCart item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this.shoppingCartService.AddTicketToShoppingCart(userId, item);

            if(result != null)
            {
                return RedirectToAction("Index", "ShoppingCarts");
            }
            else
            {
                return View(item);
            }
        }
    }
}
