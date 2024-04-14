using MovieApp.Domain.DTO;
using MovieApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Interface
{
    public interface IShoppingCartService
    {
        // public ShoppingCart addTicketToShoppingCart(string userId, AddToShoppingCartDTO model);
        public AddToShoppingCartDTO getTicketInfo(Guid Id);
        public ShoppingCartDTO getShoppingCartDetails(string userId);
        public Boolean deleteFromShoppingCart(string userId, Guid? Id);
        public Boolean orderProducts(string userId);

    }
}
