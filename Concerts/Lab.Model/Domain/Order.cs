using Lab.Model.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Model.Domain
{
    public class Order : BaseEntity
    {
        public string? OwnerId { get; set; }
        public ConcertUser? Owner { get; set; }
        public virtual ICollection<TicketsInOrder>? TicketsInOrder { get; set; }
    }
}
