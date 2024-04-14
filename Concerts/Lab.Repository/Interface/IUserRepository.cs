using Lab.Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<ConcertUser> GetAll();
        ConcertUser Get(string id);
        void Insert(ConcertUser concertUser);
        void Update(ConcertUser concertUser);
        void Delete(ConcertUser concertUser);
    }
}
