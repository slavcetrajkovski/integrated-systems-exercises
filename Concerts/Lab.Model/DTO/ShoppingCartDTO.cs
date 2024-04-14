using Lab.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Model.DTO
{
    public class ShoppingCartDTO
    {
        public List<TicketsInShoppingCart>? inCartTickets { get; set; }
        public int TotalPrice { get; set; }
    }
}
