using DataAccess.Entity;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentModel> GetALL();

        //void Insert(DepartmentModel Model);
        DepartmentModel Insert(DepartmentModel Model);
        //void Update(DepartmentModel Model); 
        DepartmentModel Update(DepartmentModel Model);
        void Delete(int ID);

        DepartmentModel GetID(int? ID);
        DepartmentModel GetIDDelete(int? ID); 
    }
}
