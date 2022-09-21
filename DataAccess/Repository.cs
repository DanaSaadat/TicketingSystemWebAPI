using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {


        private readonly DataContext _context;
        //private Table<T> table;
        //internal DbSet<T> _set;

        public Repository(DataContext context)
        {
            this._context = context;
        

        }


        public IEnumerable<T> GetALL()
        {
            try
            {
             return _context.Set<T>().ToList();
              //  return _context.Set<T>().Include().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public IQueryable<T> GetALLQueryable()
        {
            try
            {
              //return _context.Set<T>().AsQueryable();
              return _context.Set<T>();

                //IQueryable<T> query = _context.Set<T>().AsQueryable();

                //return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return _context.Set<T>().AsQueryable();
        }


        //public IQueryable<T> GetALLQueryable() 
        //{
        //    return _context.Set<T>();
        //}




        //public IEnumerable<T> Find(Expression<Func<T, bool>> where)
        //{
        //    return _context.Set<T>().Where(where);
        //}


        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
          
            _context.Set<T>().Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void update2(Login entity)
        {
            var localEntity = _context.Set<Login>().Local.FirstOrDefault(f => f.UserID == entity.UserID);


            if (localEntity != null)
            {
                _context.Entry(localEntity).State = EntityState.Detached;
            }
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int ID)
        {
            T obj = _context.Set<T>().Find(ID);

            _context.Set<T>().Remove(obj);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
          
               
         _context.Set<T>().Remove(entity);
           _context.SaveChanges();
            
           
        }


        public void Delete(IEnumerable<T> entity)
        {


            _context.Set<T>().RemoveRange(entity);
            _context.SaveChanges();
          
        }
        public T GetID(int ID)
        {
            return _context.Set<T>().Find(ID);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
