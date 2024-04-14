﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;
using MovieApp.Models.DTO;

namespace MovieApp.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingCarts
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId != null)
            {
                var loggedInUser = await _context.Users
                    .Include(u => u.UserCart)
                    .Include("UserCart.TicketsInShoppingCarts")
                    .Include("UserCart.TicketsInShoppingCarts.Ticket")
                    .FirstOrDefaultAsync(u => u.Id == userId);

                var allTickets = loggedInUser?.UserCart.TicketsInShoppingCarts.ToList();

                double totalPrice = 0;

                foreach(var item in allTickets)
                {
                    totalPrice += Double.Round(item.Quantity * item.Ticket.Price, 2);
                }

                var model = new ShoppingCartDTO
                {
                    TotalPrice = totalPrice,
                    inShoppingCart = allTickets,
                };

                return View(model);
            }
            return View();
        }

        // GET: ShoppingCarts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCarts
                .Include(s => s.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Create
        public IActionResult Create()
        {
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ShoppingCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OwnerId")] ShoppingCart shoppingCart)
        {
            if (ModelState.IsValid)
            {
                shoppingCart.Id = Guid.NewGuid();
                _context.Add(shoppingCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", shoppingCart.OwnerId);
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", shoppingCart.OwnerId);
            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,OwnerId")] ShoppingCart shoppingCart)
        {
            if (id != shoppingCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingCartExists(shoppingCart.Id))
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
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", shoppingCart.OwnerId);
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCarts
                .Include(s => s.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingCart != null)
            {
                _context.ShoppingCarts.Remove(shoppingCart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCartExists(Guid id)
        {
            return _context.ShoppingCarts.Any(e => e.Id == id);
        }

        public async Task<IActionResult> DeleteTicketFromCart(Guid? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                var loggedInUser = await _context.Users
                    .Include(u => u.UserCart)
                    .Include("UserCart.TicketsInShoppingCarts")
                    .Include("UserCart.TicketsInShoppingCarts.Ticket")
                    .FirstOrDefaultAsync(u => u.Id == userId);

                var ticketToDelete = loggedInUser?.UserCart.TicketsInShoppingCarts.
                    First(u => u.TicketId == id);

                loggedInUser?.UserCart.TicketsInShoppingCarts.Remove(ticketToDelete);

                _context.ShoppingCarts.Update(loggedInUser?.UserCart);
                _context.SaveChanges();

                return RedirectToAction("Index", "ShoppingCarts");
            }

            return RedirectToAction("Index", "ShoppingCarts");
        }

        public async Task<IActionResult> Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                var loggedInUser = await _context.Users
                    .Include(u => u.UserCart)
                    .Include("UserCart.TicketsInShoppingCarts")
                    .Include("UserCart.TicketsInShoppingCarts.Ticket")
                    .FirstOrDefaultAsync(u => u.Id == userId);

                var userCart = loggedInUser?.UserCart;

                var userOrder = new Order
                {
                    Id = Guid.NewGuid(),
                    OwnerId = userId,
                    Owner = loggedInUser
                };

                _context.Orders.Add(userOrder);
                _context.SaveChanges();

                var ticketsInOrder = userCart?.TicketsInShoppingCarts.Select(u => new TicketsInOrder
                {
                    Order = userOrder,
                    OrderId = userOrder.Id,
                    Ticket = u.Ticket,
                    TicketId = u.TicketId,
                    Quantity = u.Quantity,
                }).ToList();

                userCart?.TicketsInShoppingCarts.Clear();

                _context.ShoppingCarts.Update(userCart);
                _context.SaveChanges();

                return RedirectToAction("Index", "ShoppingCarts");
            }
            return RedirectToAction("Index", "ShoppingCarts");
        }
    }
}
