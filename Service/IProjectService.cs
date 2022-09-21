using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Models;

namespace Service
{
    public interface IProjectService
    {
        IEnumerable<ProjectModel> GetALL();

        //void Insert(ProjectModel Model, int[] ProjectEmp, int[] ProjectClient);
        ProjectModel Insert(ProjectModel Model, int[] ProjectEmp, int[] ProjectClient);
        ProjectModel Update2(ProjectModel Model, int[] ProjectEmp1, int[] ProjectClient1);
        void Update(ProjectModel Model,int[] ProjectEmp, int[] ProjectClient);

        void Delete(int ID);

        ProjectModel GetID(int ID); 
    }
}
