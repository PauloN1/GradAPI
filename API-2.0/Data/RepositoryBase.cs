using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
     public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DataContext _appDbContext;
        public RepositoryBase(DataContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Create(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _appDbContext.Set<T>();
        }

        public T GetById(int id)
        {
            return _appDbContext.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            _appDbContext.Set<T>().Update(entity);
        }
        public int SaveChanges()
        {
            return _appDbContext.SaveChanges();
        }
    }
}