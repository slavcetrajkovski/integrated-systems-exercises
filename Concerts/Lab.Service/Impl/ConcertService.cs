using Lab.Model.Domain;
using Lab.Repository.Interface;
using Lab.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Service.Impl
{
    public class ConcertService : IConcertService
    {
        private readonly IRepository<Concert> concertRepository;

        public ConcertService(IRepository<Concert> concertRepository)
        {
            this.concertRepository = concertRepository;
        }

        public Concert CreateNewConcert(Concert c)
        {
            return this.concertRepository.Insert(c); 
        }

        public Concert DeleteConcerts(Concert c)
        {
            return this.concertRepository.Delete(c);
        }

        public Concert GetConcertById(Guid? id)
        {
            return this.concertRepository.Get(id);
        }

        public List<Concert> ListAllConcerts()
        {
            return this.concertRepository.GetAll().ToList();
        }

        public Concert UpdateConcert(Concert c)
        {
            return this.concertRepository.Update(c);
        }
    }
}
