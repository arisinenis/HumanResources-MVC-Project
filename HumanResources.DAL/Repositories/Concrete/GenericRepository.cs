using HumanResources.Core.Entities;
using HumanResources.DAL.Context;
using HumanResources.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.DAL.Repositories.Concrete
{
    public class GenericRepository<T>:IRepository<T> where T : BaseEntity
    {

        private readonly ApplicationDbContext _applicationDbContext;
        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public bool Add(T entity)
        {
            try
            {
                _applicationDbContext.Set<T>().Add(entity);
                return _applicationDbContext.SaveChanges() > 0;

            }
            catch
            {

                return false;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                _applicationDbContext.Set<T>().Remove(entity);
                return _applicationDbContext.SaveChanges() > 0;
            }
            catch
            {

                return false;
            }
        }

        public bool Exists(int id)
        {
            return _applicationDbContext.Set<T>().Any(a => a.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _applicationDbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _applicationDbContext.Set<T>().FirstOrDefault(a => a.Id == id);
        }

        public bool Update(T entity)
        {
            try
            {
                _applicationDbContext.Set<T>().Update(entity);
                return _applicationDbContext.SaveChanges() > 0;
            }
            catch
            {

                return false;
            }
        }
    }
}

