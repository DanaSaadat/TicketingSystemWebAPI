using DataAccess;
using DataAccess.Entity;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DepartmentService : IDepartmentService
    {

        private IRepository<Login> IRepositoryLogin;
        private IRepositoryDepartment IRepositoryDepartment;
        private IRepository<Department> IRepositoryGeneric;
         


        public DepartmentService(RepositoryDepartment RepositoryDepartment, Repository<Login> iRepositoryLogin, Repository<Department> iRepositoryGeneric)
        {
            IRepositoryDepartment = RepositoryDepartment;
            IRepositoryLogin = iRepositoryLogin;
            IRepositoryGeneric = iRepositoryGeneric;
        }
        public IEnumerable<DepartmentModel> GetALL()
        {

            IEnumerable<DepartmentModel> Departments = IRepositoryDepartment.GetALL().Select(u => new DepartmentModel
            {

                ID = u.ID,
                Name = u.Name,
            });
            return Departments;
        }

        public DepartmentModel Insert(DepartmentModel Model)
        {
            var CheckDepartmentName = IRepositoryDepartment.GetALL().Where(X => X.Name.Trim() == Model.Name.Trim()).Count();
            if (CheckDepartmentName !=  0)
            {
                Model.IsDepartmentNameExist = true;


                return Model;
            }

            Department obj = new Department();
                obj.ID = Model.ID;

                obj.Name = Model.Name;
            obj.ManagerID = Model.ManagerID;

            IRepositoryDepartment.Insert(obj);
            return Model;
           
        }
        public DepartmentModel Update(DepartmentModel Model)
        {

            var CheckDepartmentName = IRepositoryGeneric.GetALL().Where(X => X.Name.Trim() == Model.Name.Trim() && X.ID != Model.ID).Count();
            //var CheckDepartmentName = IRepositoryDepartment.GetALL().Where(X => X.Name.Trim() == Model.Name.Trim()).Count();

            //IRepositoryGeneric.Save();
            if (CheckDepartmentName != 0)
            {
                Model.IsDepartmentNameExist = true;


                return Model;
            }


            Department obj = new Department();
            obj.ID = Model.ID;

            obj.Name = Model.Name;
            obj.ManagerID = Model.ManagerID;
            IRepositoryDepartment.Update(obj);
            return Model;
        }
        public DepartmentModel GetID(int? ID)
        {
            DepartmentModel Model = new DepartmentModel();
            Department DepartmentEntity = IRepositoryDepartment.GetID(ID.Value);
            Model.Name = DepartmentEntity.Name;
           Model.ManagerID = DepartmentEntity.ManagerID;

            Model.ID = ID.Value;

            return Model;
        }

        public DepartmentModel GetIDDelete(int? ID)
        {
          
            DepartmentModel Model = new DepartmentModel();
            Department DepartmentEntity = IRepositoryDepartment.GetID(ID.Value);
            Model.Name = DepartmentEntity.Name;
           Model.UserCount = (from p in IRepositoryLogin.GetALL() where p.DepartmentID == ID select p).Count();
            
           
            return Model;
        }
        public void Delete(int ID)
        {
            Department DepartmentEntity = IRepositoryDepartment.GetID(ID);

            var result= (from p in IRepositoryLogin.GetALL() where p.DepartmentID == ID select p).ToList();

            foreach (var i in result)
            {
                i.DepartmentID = null;
                IRepositoryLogin.Update(i);
            }
           
            IRepositoryDepartment.Delete(ID);
        
        }

      

      

     

      
    }
}
