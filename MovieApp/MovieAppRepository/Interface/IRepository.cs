using MovieApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppRepository.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(Guid? Id);
        T Insert(T entity);
        T Update(T entity);  
        T Delete(T entity);
        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
    }
}
