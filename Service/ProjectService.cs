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
    public class ProjectService : IProjectService
    {

        private IRepository<Project> IRepository;
        private IRepository<ProjectEmp> IRepositoryProjectEmp; 
        private IRepository<ProjectClient> IRepositoryProjectClient;  
        private IRepository<Login> IRepositoryUser;   
        private IRepository<Department> IRepositoryDepartment;    
        private IRepository<Approval> IRepositoryApproval;     

        public ProjectService(Repository<Project> userRepository, Repository<ProjectEmp> iRepositoryProjectEmp, Repository<ProjectClient> iRepositoryProjectClient, Repository<Login> iRepositoryUser, Repository<Department> iRepositoryDepartment, Repository<Approval> iRepositoryApproval)
        {

            this.IRepository = userRepository;
            IRepositoryProjectEmp = iRepositoryProjectEmp;
            IRepositoryProjectClient = iRepositoryProjectClient;
            IRepositoryUser = iRepositoryUser;
            IRepositoryDepartment = iRepositoryDepartment;
            IRepositoryApproval = iRepositoryApproval;
        }



        public void Delete(int ID)
        {
            Project Project = IRepository.GetID(ID);

            var ProID = IRepositoryProjectEmp.GetALL().Where(x => x.ProjectID == ID);
            if (ProID.Any())
            {
                IRepositoryProjectEmp.Delete(ProID);
           
            }

            var proClient = IRepositoryProjectClient.GetALL().Where(x => x.ProjectID == ID);
            if (proClient.Any())
            {
                IRepositoryProjectClient.Delete(proClient);
             
            }
            var Approval = IRepositoryApproval.GetALL().Where(x => x.ProjectID == ID);
            if (Approval.Any())
            {
                IRepositoryApproval.Delete(Approval);
             
            }

            IRepository.Delete(ID);
        }

        public IEnumerable<ProjectModel> GetALL()
        {
            IEnumerable<ProjectModel> Projects = IRepository.GetALL().Select(u => new ProjectModel
            {

                ID = u.ID,
                Name = u.Name,
            });
            return Projects;
        }

        public ProjectModel GetID(int ID)
        {
            ProjectModel Model = new ProjectModel();

             Project ProjectEntity = IRepository.GetID(ID);


            Model.Name = ProjectEntity.Name;
            return Model;
        }

        public ProjectModel Insert(ProjectModel Model, int[] ProjectEmp, int[] ProjectClient)
        {
            if (Model.Name != null && ProjectEmp == null && ProjectClient == null)
            {
                Project obj = new Project();
                obj.Name = Model.Name;
                obj.ID = Model.ID;
                IRepository.Insert(obj);
            }


            ProjectEmp obj1 = new ProjectEmp();
            ProjectClient obj2 = new ProjectClient();
            Approval obj3 = new Approval();
         
            if (ProjectEmp != null)
            {


                foreach (var xx in ProjectEmp)
                {

                    Model.ManagerID = (from user in IRepositoryUser.GetALL()
                                join Department in IRepositoryDepartment.GetALL()
                                on user.DepartmentID equals Department.ID
                                where user.UserID == xx
                                select Department.ManagerID).SingleOrDefault();

                    if (Model.ManagerID == null)
                    {
                        Model.mesage = (from user in IRepositoryUser.GetALL()
                                        join Department in IRepositoryDepartment.GetALL()
                                        on user.DepartmentID equals Department.ID
                                        where user.UserID == xx
                                        select user.UserName).SingleOrDefault();


                            return Model;
                    }

                    else if (xx == ProjectEmp.Last() && Model.ManagerID != null) // 
                    {
                        Project objpro = new Project();
                        objpro.Name = Model.Name;
                        objpro.ID = Model.ID;
                        IRepository.Insert(objpro);
                        Model.IDtest = objpro.ID;



                        foreach (var xxx in ProjectEmp)
                    {
                        obj1.EmpID = xxx;
                        obj1.ProjectID = objpro.ID;
                               
                                IRepositoryProjectEmp.Insert(obj1);

                        var data3 = (from user in IRepositoryUser.GetALL()
                                     join Department in IRepositoryDepartment.GetALL()
                                     on user.DepartmentID equals Department.ID
                                     where user.UserID == xxx
                                     select Department.ManagerID).SingleOrDefault();


                        var Approval = IRepositoryApproval.GetALL().Where(x => x.ProjectID == objpro.ID && x.ManagerID == data3);
                        if (!Approval.Any())
                        {
                            obj3.ProjectID = objpro.ID;
                           obj3.ManagerID = data3;
                            //obj3.statusID = 7;
                            obj3.statusID = (int)enums.Status.New; 
                            IRepositoryApproval.Insert(obj3);
                           
                        }
                    }


                    }

                }
            }

            if (ProjectClient != null)
            {
                Project objpro = new Project();
                objpro.Name = Model.Name;
                objpro.ID = Model.ID;

                if (ProjectEmp == null)
                {
                  
                    IRepository.Insert(objpro);

                }

                foreach (var xx in ProjectClient)
                {
                   

                    obj2.ClientID = xx;
                   
                    if (ProjectEmp == null)
                    {
                        obj2.ProjectID = objpro.ID;
                    }
                    else
                    {
                   obj2.ProjectID = Model.IDtest;
                    }

                    IRepositoryProjectClient.Insert(obj2);

                }
                }

            return Model;
        }

        public void Update(ProjectModel Model, int[] ProjectEmp1, int[] ProjectClient1) 
        {


            ProjectEmp obj1 = new ProjectEmp();
            ProjectClient obj2 = new ProjectClient();

            var proj = IRepositoryProjectEmp.GetALL().Where(x => x.ProjectID == Model.ID).ToList();
            foreach (var oldEmp in proj)
            {
                IRepositoryProjectEmp.Delete(oldEmp);
              
            }
            var pro1 = IRepositoryProjectClient.GetALL().Where(x => x.ProjectID == Model.ID).ToList();
            foreach (var oldclient in pro1)
            {
                IRepositoryProjectClient.Delete(oldclient);
                
            }

            if (ProjectEmp1 != null)
            {

                foreach (var xx in ProjectEmp1)
                {

                    obj1.EmpID = xx;

                    obj1.ProjectID = Model.ID;
                    IRepositoryProjectEmp.Insert(obj1);
                

                }
            }

            if (ProjectClient1 != null)
            {
                foreach (var xx in ProjectClient1)
                {

                    obj2.ClientID = xx;


                    obj2.ProjectID = Model.ID;
                    IRepositoryProjectClient.Insert(obj2);
                 

                }
            }

            Project obj = new Project();
            obj.Name = Model.Name;
            obj.ID = Model.ID;
            IRepository.Update(obj);
        }


        public ProjectModel Update2(ProjectModel Model, int[] ProjectEmp1, int[] ProjectClient1)
        {


            ProjectEmp obj1 = new ProjectEmp();
            ProjectClient obj2 = new ProjectClient();
            Approval obj3 = new Approval();

            if (ProjectEmp1 != null && Model.Name != null)
            {

                foreach (var xx in ProjectEmp1)
                {

                    Model.ManagerID = (from user in IRepositoryUser.GetALL()
                                join Department in IRepositoryDepartment.GetALL()
                                on user.DepartmentID equals Department.ID
                                where user.UserID == xx
                                select Department.ManagerID).SingleOrDefault();


                    if (Model.ManagerID == null)
                    {
                        Model.mesage = (from user in IRepositoryUser.GetALL()
                                        join Department in IRepositoryDepartment.GetALL()
                                        on user.DepartmentID equals Department.ID
                                        where user.UserID == xx
                                        select user.UserName).SingleOrDefault();

                        //Model.mesage = (from user in IRepositoryUser.GetALL() where user.UserID== xx && user.DepartmentID == null select user.UserName).SingleOrDefault();
                        return Model;
                    }

                    else if (xx == ProjectEmp1.Last() && Model.ManagerID != null) // 
                    {

                        var proj = IRepositoryProjectEmp.GetALL().Where(x => x.ProjectID == Model.ID).ToList();
                        foreach (var oldEmp in proj)
                        {
                            IRepositoryProjectEmp.Delete(oldEmp);
                         
                        }

                        var Approval1 = IRepositoryApproval.GetALL().Where(x => x.ProjectID == Model.ID).ToList();
                        foreach (var item in Approval1)
                        {
                            IRepositoryApproval.Delete(item);

                        }
                        foreach (var xxx in ProjectEmp1)
                        {
                            obj1.EmpID = xxx;
                            obj1.ProjectID = Model.ID;
                           
                            IRepositoryProjectEmp.Insert(obj1);

                            var data3 = (from user in IRepositoryUser.GetALL()
                                         join Department in IRepositoryDepartment.GetALL()
                                         on user.DepartmentID equals Department.ID
                                         where user.UserID == xxx
                                         select Department.ManagerID).SingleOrDefault();


                            var Approval = IRepositoryApproval.GetALL().Where(x => x.ProjectID == Model.ID && x.ManagerID == data3);


                             
                            if (!Approval.Any())
                            {
                                obj3.ProjectID = Model.ID;
                                obj3.ManagerID = data3;
                                //obj3.statusID = 7;
                                obj3.statusID = (int)enums.Status.New;
                                IRepositoryApproval.Insert(obj3);
                              
                            }
                        }


                    }
                }
            }

            if (ProjectClient1 != null && Model.Name != null)
            {

                var pro1 = IRepositoryProjectClient.GetALL().Where(x => x.ProjectID == Model.ID).ToList();
                foreach (var oldclient in pro1)
                {
                    IRepositoryProjectClient.Delete(oldclient);

                }
                foreach (var client in ProjectClient1)
                {

                    obj2.ClientID = client; 


                    obj2.ProjectID = Model.ID;
                    IRepositoryProjectClient.Insert(obj2);

                }
            }

            Project obj = new Project();
            obj.Name = Model.Name;
            obj.ID = Model.ID;
            IRepository.Update(obj);

            return Model;
        }
    }
}
