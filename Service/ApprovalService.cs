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
    public class ApprovalService : IApprovalService
    {
        private IRepository<Approval> IRepository;
        private IRepository<Project> IRepositoryProject;

        public ApprovalService(Repository<Approval> ApprovalRepository, Repository<Project> iRepository)
        {

            this.IRepository = ApprovalRepository;
            this.IRepositoryProject = iRepository;
        }

        public IEnumerable<ApprovalModel> GetALL(int ID)
        {
            //List<ApprovalModel> lst1 = new List<ApprovalModel>();

            var lstApproval = from Approval in IRepository.GetALL()
                     where Approval.ManagerID == ID
                     select Approval;
             
            IEnumerable<ApprovalModel> Approvals = lstApproval.Select(u => new ApprovalModel
            {
                id = u.id,
                ProjectID = u.ProjectID,
                ManagerID = u.ManagerID,
                RejectReason = u.RejectReason,
                statusID = u.statusID,
                Status = new StatusModel()
                {
                    Name = u.Status.Name
                },
                Login = new LoginModel()
                {
                    UserName = u.Login.UserName
                },

                Project = new ProjectModel()
                {
                    ID = u.Project.ID,
                    Name = u.Project.Name,
                    ProjectEmp = u.Project.ProjectEmp.Select(x => new ProjectEmpModel()
                    {
                        EmpID = x.EmpID,
                        ProjectID = x.ProjectID,
                        Login = new LoginModel()
                        {

                            UserName = x.Login.UserName
                        }
                    }).ToList(),

                    ProjectClient = u.Project.ProjectClient.Select(y => new ProjectClientModel()
                    {
                        ClientID = y.ClientID,
                        ProjectID = y.ProjectID,
                        Login = new LoginModel()
                        {
                            UserName = y.Login.UserName
                        }
                    }).ToList()
                   
                }

            }) ;
            return Approvals;
        }


        public IEnumerable<ApprovalModel> GetALLstatus(int projectID)
        {
            IEnumerable<ApprovalModel> Approvals = IRepository.GetALL().Select(u => new ApprovalModel
            {
                ProjectID = u.ProjectID,
                ManagerID = u.ManagerID,
                RejectReason = u.RejectReason,
                statusID = u.statusID,

                Login = new LoginModel()
                {
                    UserName = u.Login.UserName
                },

                Project = new ProjectModel()
                {
                    Name = u.Project.Name
                },
                Status = new StatusModel()
                {
                    Name = u.Status.Name
                }

            });
            return Approvals;
        }


        public ApprovalModel GetID(int ID, int projectID)
        {
            ApprovalModel Model = new ApprovalModel();

            Approval ApprovalEntity = IRepository.GetID(ID);


            Model.ManagerID = ApprovalEntity.ManagerID;
            Model.RejectReason = ApprovalEntity.RejectReason;
          
            var GetProject = (from p in IRepositoryProject.GetALL() where p.ID == projectID select p).SingleOrDefault();
          
            Model.ProjectName = GetProject.Name;
            Model.ProjectID = ApprovalEntity.ProjectID;
            Model.statusID = ApprovalEntity.statusID;
            Model.Project = new ProjectModel()
            {
                Name = GetProject.Name,
            };
          
            return Model;
        }

        public ApprovalModel Insert(int? id, int projectID)
        {
            var GetApproval = (from p in IRepository.GetALL() where p.id == id select p).SingleOrDefault();
            GetApproval.statusID = (int)enums.Status.approve; // approve 
            IRepository.Update(GetApproval); 

            ApprovalModel Model = new ApprovalModel();

            var GetApprovalByProjectID  = (from p in IRepository.GetALL() where p.ProjectID == projectID select p).ToList();
            bool x = false;
            Model.CheckAllStatus = false;
            foreach (var item in GetApprovalByProjectID)
            {
                if (item.statusID == (int)enums.Status.approve)
                {
                    x = true;
                    Model.CheckAllStatus = true;
                }

                else
                {
                    x = false;
                    Model.CheckAllStatus = false;
                    return Model;
                }
            }
            if (x == true)
            {
                Project Project = IRepositoryProject.GetID(projectID);


                Project.statusID = (int)enums.Status.approve; // approve 

                IRepositoryProject.Update(Project);
            }

            return Model;

        }


        public ApprovalModel RejectManager(ApprovalModel Model)
        {
            if (Model.RejectReason != null)
            {

                var GetApproval = (from p in IRepository.GetALL() where p.id == Model.id select p).SingleOrDefault();
                GetApproval.statusID = (int)enums.Status.reject;
                GetApproval.RejectReason = Model.RejectReason;

                IRepository.Update(GetApproval);
              
                var GetApprovalByProjectID  = (from p in IRepository.GetALL() where p.ProjectID == Model.ProjectID select p).ToList();
                bool x = false;
                Model.CheckAllStatus = false;
                foreach (var item in GetApprovalByProjectID)
                {
                    if (item.statusID == (int)enums.Status.reject)
                    {
                        x = true;
                        Model.CheckAllStatus = true;
                    }

                    else
                    {
                        x = false;
                        Model.CheckAllStatus = false;
                        return Model;
                    }
                }

                if (x == true)
                {
                    Project Project = IRepositoryProject.GetID(Model.ProjectID.Value);
                    Project.statusID = (int)enums.Status.reject;
                    IRepositoryProject.Update(Project);
                 
                }
              
            }
            return Model;
        }
    }
}
