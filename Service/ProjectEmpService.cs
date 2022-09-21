using DataAccess;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Models;


namespace Service
{
    public class ProjectEmpService : IProjectEmpService
    {

        private IRepository<ProjectEmp> IRepository;
        private IRepositoryProjectEmp IRepositoryProjectEmp;

        public ProjectEmpService(Repository<ProjectEmp> userRepository, RepositoryProjectEmp iRepositoryProjectEmp)
        {

            this.IRepository = userRepository;
            IRepositoryProjectEmp = iRepositoryProjectEmp;
        }




        public void Delete(int ID)
        {
            IRepository.Delete(ID);
        }

        public IEnumerable<ProjectEmpModel> GetALL()
        {
            IEnumerable<ProjectEmpModel> ProjectEmps = IRepositoryProjectEmp.GetALL().Select(u => new ProjectEmpModel
            {

               id = u.id,
               ProjectID = u.ProjectID,
               EmpID = u.EmpID,
               Login = new LoginModel()
               {
                   UserID = u.Login.UserID
               }

            }); 
            return ProjectEmps; 
        }

        //public ProjectEmpModel GetID(int ID)
        //{
        //    return IRepository.GetID(ID);
        //}

        //public void Insert(ProjectEmpModel Model)
        //{
        //    IRepository.Insert(Model);
        //}

        //public void Update(ProjectEmpModel Model)
        //{
        //    IRepository.Update(Model);
        //}


        //public void Delete(ProjectEmpModel Model)
        //{
        //    IRepository.Delete(Model);
        //}

        //public void Delete(IEnumerable<ProjectEmpModel> Model)
        //{
        //    IRepository.Delete(Model);
        //}
    }
}
