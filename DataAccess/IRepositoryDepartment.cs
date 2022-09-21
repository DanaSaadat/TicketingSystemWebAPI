using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepositoryDepartment
    {
        IEnumerable<Department> GetALL();

        void Insert(Department DepartmentEntity);

        void Update(Department DepartmentEntity);

        void Delete(int ID);

        Department GetID(int ID);


        void Delete(Department DepartmentEntity);
        void Delete(IEnumerable<Department> DepartmentEntity);
    }
}
