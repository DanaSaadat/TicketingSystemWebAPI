using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RepositoryProjectEmp : IRepositoryProjectEmp
    {
        private readonly DataContext _context;

        public RepositoryProjectEmp(DataContext context)
        {
            this._context = context;
        }
        public IEnumerable<ProjectEmp> GetALL()
        {
            try
            {
                //return _context.PermissionUser.Include(x => x.Permission).ToList();
                //return _context.ProjectEmps.ToList();
                return _context.ProjectEmps.Include(x => x.Login).ToList();

                // return _context.PermissionUser.Include(x => x.Login).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        public void Insert(ProjectEmp entity)
        {
            //_context.Set<Department>().Add(entity);
            _context.ProjectEmps.Add(entity);
            _context.SaveChanges();
        }

        public void Update(ProjectEmp entity)
        {
            _context.ProjectEmps.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }


        public void Delete(int ID)
        {
            ProjectEmp obj = _context.ProjectEmps.Find(ID);

            _context.ProjectEmps.Remove(obj);
            _context.SaveChanges();
        }

        public void Delete(ProjectEmp entity)
        {


            _context.ProjectEmps.Remove(entity);
            _context.SaveChanges();


        }


        public void Delete(IEnumerable<ProjectEmp> entity)
        {


            _context.ProjectEmps.RemoveRange(entity);
            _context.SaveChanges();

        }
        public ProjectEmp GetID(int ID)
        {
            return _context.ProjectEmps.Find(ID); 
        }

    }
}
