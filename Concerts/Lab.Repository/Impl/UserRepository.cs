using Lab.Model.Identity;
using Lab.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Repository.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<ConcertUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<ConcertUser>();
        }
        public void Delete(ConcertUser concertUser)
        {
            if (concertUser == null) throw new ArgumentNullException("concertUser");

            entities.Remove(concertUser);
            this.context.SaveChanges();
        }

        public ConcertUser Get(string id)
        {
            var strGuid = id.ToString();
            return entities
                .Include(z => z.UserCart)
                .Include(z => z.UserCart.TicketsInShoppingCart)
                .Include("UserCart.TicketsInShoppingCart")
                .First(s => s.Id == strGuid);
        }

        public IEnumerable<ConcertUser> GetAll()
        {
            return entities.AsEnumerable();
        }
            
        public void Insert(ConcertUser concertUser)
        {
            if (concertUser == null) throw new ArgumentNullException("concertUser");

            entities.Add(concertUser);
            this.context.SaveChanges();
        }

        public void Update(ConcertUser concertUser)
        {
            if (concertUser == null) throw new ArgumentNullException("concertUser");

            entities.Update(concertUser);
            this.context.SaveChanges();
        }
    }
}
