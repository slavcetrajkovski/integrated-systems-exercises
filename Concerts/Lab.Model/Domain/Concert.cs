using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Model.Domain
{
    public class Concert : BaseEntity
    {
        public string? ConcertName { get; set; }
        public DateTime ConcertDate { get; set; }
        public int ConcertPrice { get; set; }
        public string? ConcertPlace { get; set; }
        public string? ConcertImage { get; set; }
        public ICollection<Ticket>? ConcertTickets { get; set; }
    }
}
