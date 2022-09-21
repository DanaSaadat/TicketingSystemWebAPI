using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RepositoryPermissionUser : IRepositoryPermissionUser
    {
        private readonly DataContext _context;

        public RepositoryPermissionUser(DataContext context)
        {
            this._context = context;
        }


        public IEnumerable<PermissionUser> GetALL()
        {
            try
            {
               return _context.PermissionUser.Include(x => x.Permission).ToList();
              // return _context.PermissionUser.Include(x => x.Login).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        public void Insert(PermissionUser entity)
        {
            //_context.Set<Department>().Add(entity);
            _context.PermissionUser.Add(entity);
            _context.SaveChanges();
        }

        public void Update(PermissionUser entity)
        {
            _context.PermissionUser.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }


        public void Delete(int ID)
        {
            PermissionUser obj = _context.PermissionUser.Find(ID);

            _context.PermissionUser.Remove(obj);
            _context.SaveChanges();
        }

        public void Delete(PermissionUser entity)
        {


            _context.PermissionUser.Remove(entity);
            _context.SaveChanges();


        }


        public void Delete(IEnumerable<PermissionUser> entity)
        {


            _context.PermissionUser.RemoveRange(entity);
            _context.SaveChanges();

        }
        public PermissionUser GetID(int ID)
        {
            return _context.PermissionUser.Find(ID);
        }

    }
}
