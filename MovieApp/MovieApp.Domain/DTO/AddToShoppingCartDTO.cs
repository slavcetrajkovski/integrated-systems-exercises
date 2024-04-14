using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.DTO
{
    public class AddToShoppingCartDTO
    {
        public Guid TicketId { get; set; }
        public string? SelectedMovie { get; set; }
        public int Quantity { get; set; }
    }
}
