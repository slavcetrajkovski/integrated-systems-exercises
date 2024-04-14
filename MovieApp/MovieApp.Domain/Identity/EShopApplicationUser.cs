using Microsoft.AspNetCore.Identity;
using MovieApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.Identity
{
    public class EShopApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public ShoppingCart? UserCart { get; set; }
        public virtual ICollection<Ticket>? MyTickets { get; set; }
    }
}
