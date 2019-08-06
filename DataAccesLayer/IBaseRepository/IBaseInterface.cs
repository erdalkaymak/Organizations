using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Repositorys
{
    public interface IBaseInterface<T> where T:class
    {
        List<T> GetAll();
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        T Find(int id);
    }
}
