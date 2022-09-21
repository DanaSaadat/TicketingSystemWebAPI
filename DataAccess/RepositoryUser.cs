using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RepositoryUser : IRepositoryUser
    {
        private readonly DataContext _context;

        public RepositoryUser(DataContext context)
        {
            this._context = context;
        }


        public IEnumerable<Login> GetALL()
        {
            try
            {
              //  return _context.Logins.ToList();
            return _context.Logins.Include(x=>x.Department).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        public void Insert(Login entity)
        {
            _context.Logins.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Login entity)
        {
            _context.Logins.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }


        public void Delete(int ID)
        {
            Login obj = _context.Logins.Find(ID);

            _context.Logins.Remove(obj);
            _context.SaveChanges();
        }

        //public void Delete(Department entity)
        //{


        //    _context.Set<Department>().Remove(entity);
        //    _context.SaveChanges();


        //}


        //public void Delete(IEnumerable<Department> entity)
        //{


        //    _context.Set<Department>().RemoveRange(entity);
        //    _context.SaveChanges();

        //}
        public Login GetID(int ID)
        {
            return _context.Logins.Find(ID); 
        }
    }
}
