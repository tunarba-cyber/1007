using _1007MiniProject.Application.interfaces.repositories;
using _1007MiniProject.Core.Entities.common;
using _1007MiniProject.Persistance.contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _1007MiniProject.Persistance.implementations.repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;

        public Repository(AppDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public void Add(T entity)
        {
            _table.Add(entity);
        }

        public bool Any(Expression<Func<T, bool>> IsExist)
        {
            return _table.Any(IsExist);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public List<T> GetAll()
        {
            return _table.ToList();
        }

        public T? GetById(int id)
        {
            return _table.Find(id);
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
