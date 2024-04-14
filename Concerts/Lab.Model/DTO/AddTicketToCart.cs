using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Model.DTO
{
    public class AddTicketToCart
    {
        public Guid TicketId { get; set; }
        public string? SelectedConcert { get; set; }
        public int SelectedConcertPrice { get; set; }
        public int NumberOfPeople { get; set; }
    }
}
