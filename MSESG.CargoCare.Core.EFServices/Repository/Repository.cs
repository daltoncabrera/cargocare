using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSESG.CargoCare.Core;

namespace MSESG.CargoCare.Core.EFServices
{
    public class Repository<T> : IRepository<T> where T : class 
    {
        protected readonly DbContext _context;
        public IUnitOfWork Uow { get; private set; }
        public IQueryable<T> All
        {
            get { return _context.Set<T>(); }
        }

        public Repository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<T> AllEager(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>();
            foreach (var incl in includes)
            {
                query.Include(incl);
            }

            return query;
        }

     

        public void Insert(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Update(T entity)
        {
            if (!_context.Set<T>().Local.Contains(entity))
                _context.Set<T>().Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
