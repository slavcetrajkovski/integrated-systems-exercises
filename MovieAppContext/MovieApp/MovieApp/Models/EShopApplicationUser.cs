﻿using Microsoft.AspNetCore.Identity;

namespace MovieApp.Models
{
    public class EShopApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public ShoppingCart? UserCart { get; set; }
        public virtual ICollection<Ticket> MyTickets { get; set; }
    }

}
