using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Repositorys
{
    public abstract class BaseGenericRepository<T> : IBaseInterface<T> where T : class
    {
        protected MyOrganizationEntities _db;
        public BaseGenericRepository(MyOrganizationEntities db)
        {
            _db = db;
        } 

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
            _db.SaveChanges();
        }

        public T Find(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public void Insert(T entity)
        {
            _db.Set<T>().Add(entity);
            _db.SaveChanges();
        }

        public void Update(T entity)
        {
            _db.Set<T>().AddOrUpdate(entity);
            _db.SaveChanges();
        }
    }
}
