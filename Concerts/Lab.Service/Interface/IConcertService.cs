using Lab.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Service.Interface
{
    public interface IConcertService
    {
        public List<Concert> ListAllConcerts();
        public Concert GetConcertById(Guid? id);
        public Concert CreateNewConcert(Concert c);
        public Concert UpdateConcert(Concert c);
        public Concert DeleteConcerts(Concert c);
    }
}
