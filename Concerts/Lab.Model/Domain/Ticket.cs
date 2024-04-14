using Lab.Model.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Model.Domain
{
    public class Ticket : BaseEntity
    {
        public int NumberOfPeople { get; set; }
        public Guid ConcertId { get; set; }
        public Concert? Concert { get; set; }
        public virtual ConcertUser? CreatedBy { get; set; }
        public virtual ICollection<TicketsInShoppingCart>? TicketsInShoppingCart { get; set; }
    }
}
