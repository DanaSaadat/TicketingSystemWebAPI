using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepositoryProjectEmp
    {
        IEnumerable<ProjectEmp> GetALL();

        void Insert(ProjectEmp ProjectEmpEntity);

        void Update(ProjectEmp ProjectEmpEntity);

        void Delete(int ID);
        void Delete(ProjectEmp ProjectEmpEntity);
        void Delete(IEnumerable<ProjectEmp> ProjectEmpEntity);

        ProjectEmp GetID(int ID); 
    }
}
