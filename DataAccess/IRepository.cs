using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetALL();
        IQueryable<T> GetALLQueryable();

        void Insert(T entity);

        void Update(T entity);
        void update2(Login entity);
        void Delete(int ID);

        T GetID(int ID);


        void Delete(T entity);
        void Save();
        void Delete(IEnumerable<T> entity);
    }
}
