using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _1007MiniProject.Application.interfaces.repositories
{
    public interface IRepository<T> 
    {
        void Add(T entity);
        bool Any(Expression<Func<T, bool>> IsExist);
        void Delete(T entity);
        List<T> GetAll();
        T? GetById(int id);
        void Update(T entity);
        void SaveChanges();
    }
}   

        
