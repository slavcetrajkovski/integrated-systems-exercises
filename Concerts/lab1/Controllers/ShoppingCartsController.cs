    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Lab.Model.DTO;
using Lab.Model.Domain;
using Lab.Repository;
using Lab.Service.Interface;

namespace lab1.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartsController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        // GET: ShoppingCarts
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = this.shoppingCartService.getShoppingCartDetails(userId??"");

            return View(model);
        }

        public IActionResult DeleteTicket(Guid? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var ticketToDelete = this.shoppingCartService.deleteFromShoppingCart(userId, id);

            return RedirectToAction("Index", "ShoppingCarts");
        }

        public IActionResult Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orderProcessing = this.shoppingCartService.orderTicket(userId??"");

            return RedirectToAction("Index", "ShoppingCarts");
        }
    }
}
