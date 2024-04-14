using Lab.Model.Domain;
using Lab.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCart AddTicketToShoppingCart(string userId, AddTicketToCart model);
        AddTicketToCart getTicketInfo(Guid? id);
        ShoppingCartDTO getShoppingCartDetails(string userId);
        Boolean deleteFromShoppingCart(string userId, Guid? id);
        Boolean orderTicket(string userId);
    }
}
