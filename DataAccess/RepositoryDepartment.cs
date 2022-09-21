using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RepositoryDepartment : IRepositoryDepartment ,IRepository<Department>
    {
        private readonly DataContext _context;

        public RepositoryDepartment(DataContext context)
        {
            this._context = context;
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

        public IEnumerable<Department> GetALL()
        {
            try
            {
                return _context.Departments.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        public void Insert(Department entity)
        {
            _context.Departments.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Department entity)
        {
            _context.Departments.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }


        public void Delete(int ID)
        {
            Department obj = _context.Departments.Find(ID);

            _context.Departments.Remove(obj);
            _context.SaveChanges();
        }

        public void Delete(Department entity)
        {


            _context.Departments.Remove(entity);
            _context.SaveChanges();


        }


        public void Delete(IEnumerable<Department> entity)
        {


            _context.Departments.RemoveRange(entity);
            _context.SaveChanges();

        }
        public Department GetID(int ID)
        {
            return _context.Departments.Find(ID);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        //public IQueryable<Department> GetALLQueryable()
        //{
        //    return _context.Set<Department>();
        //}

        //IQueryable<Department> GetALLQueryable()
        //{
        //    return _context.Set<Department>();
        //}

        IQueryable<Department> IRepository<Department>.GetALLQueryable()
        {
            throw new NotImplementedException();
        }
        //public IQueryable<T> GetALLQueryable()
        //{

        //    return _context.Set<T>().AsQueryable();
        //}

    }
}
