using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Models;


namespace Service
{
    public interface IProjectEmpService
    {
        IEnumerable<ProjectEmpModel> GetALL();

        //void Insert(ProjectEmpModel Model); 

        //void Update(ProjectEmpModel Model);

        void Delete(int ID);
        //void Delete(ProjectEmpModel Model);
        //void Delete(IEnumerable<ProjectEmpModel> Model);

        //ProjectEmpModel GetID(int ID);
    }
}
