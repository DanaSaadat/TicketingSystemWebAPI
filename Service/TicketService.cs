using DataAccess;
using DataAccess.Entity;
using Service.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Models;

namespace Service
{
    public class TicketService : ITicketService
    {

        private IRepository<Ticket> IRepository;
        private IRepository<ProjectClient> _IRepositoryprojectClinet;
        private IRepository<ProjectEmp> _IRepositoryProjectEmp;
        private IRepository<Project> _IRepositoryProject;

        public TicketService(Repository<Ticket> userRepository, Repository<ProjectClient> projectClintIRepository, Repository<ProjectEmp> ProjectEmpIRepository, Repository<Project> projectRepository)
        {

            this.IRepository = userRepository;
            _IRepositoryprojectClinet = projectClintIRepository;
            _IRepositoryProjectEmp = ProjectEmpIRepository;
            _IRepositoryProject = projectRepository;
        }

        public void Delete(int ID)
        {
            IRepository.Delete(ID);
        }

        //public IEnumerable<TicketModel> GetALL()
        //{

        //    return IRepository.GetALL();
        //}
        //public IEnumerable<Ticket> GetALL1(int clientId)
        //{
        //    var result = from t1 in IRepository.GetALL()
        //                 join pc in _projectClintIRepository.GetALL()
        //                 on t1.ProjectID equals pc.ProjectID


        //                 where pc.ClientID == clientId
        //                 && t1.ClientID == clientId
        //                 select t1;

        //    return result;
        //}


        public IEnumerable<TicketModel> GetALLTicketModel(string id, string DepartmentID, int clientID)
        {
            List<Ticket> lst = new List<Ticket>();

            if (Convert.ToInt32(id) != (int)perRole.SuperAdmin && DepartmentID == null || (Convert.ToInt32(id) != (int)perRole.SuperAdmin && DepartmentID == ""))
            {
                var result = from t1 in IRepository.GetALL()
                             join pc in _IRepositoryprojectClinet.GetALL()
                             on t1.ProjectID equals pc.ProjectID


                             where pc.ClientID == clientID
                             && t1.ClientID == clientID
                             select t1;

                lst = result.ToList();
            }
            if (!string.IsNullOrEmpty(DepartmentID))
            {


                if (Convert.ToInt32(DepartmentID) == (int)perRole.BA) // ba 
                {
                  
                    long EmpID = Convert.ToInt64(id.ToString());

                    var result = from t1 in IRepository.GetALL()
                                 join pe in _IRepositoryProjectEmp.GetALL()
                                 on t1.ProjectID equals pe.ProjectID
                                 where pe.EmpID == EmpID && (t1.statusID == (int)enums.Status.waitingforBA
                                 || (t1.AssignTo == EmpID && t1.statusID == (int)enums.Status.pending))   // test  waiting for ba 

                                 select t1;
                    lst = result.ToList();
                }

                if (Convert.ToInt32(DepartmentID) == (int)perRole.Developer) // dev
                {
                    long EmpID = Convert.ToInt64(id.ToString());

                    var result = from t1 in IRepository.GetALL()
                                 join pe in _IRepositoryProjectEmp.GetALL()
                                 on t1.ProjectID equals pe.ProjectID
                                 where pe.EmpID == EmpID &&
                               (t1.statusID == (int)enums.Status.GotoDeveloper
                               || t1.statusID == (int)enums.Status.approve
                               || (t1.AssignTo == EmpID && t1.statusID == (int)enums.Status.pending))//  
                                 select t1;
                    lst = result.ToList();
                }
            }
            else if (Convert.ToInt32(id) == (int)perRole.SuperAdmin)
            {
                lst = IRepository.GetALL().ToList();

            }

            IEnumerable<TicketModel> Tickets = lst.Select(u => new TicketModel
            {
                id = u.id,
                ProjectID = u.ProjectID,

                ProjectName = u.Project.Name,
                Description = u.Description,

                statusID = u.statusID,
                statusName = u.Status.Name,
                ClientID = u.ClientID,
                AssignTo = u.AssignTo
            });

            return Tickets;
        }
        //public IEnumerable<Ticket> GetALLnew(string id,string DepartmentID,int clientID)
        //{
        //    List<Ticket> lst = new List<Ticket>();

        //    if (Convert.ToInt32(id) != (int)perRole.SuperAdmin && DepartmentID == null || (Convert.ToInt32(id) != (int)perRole.SuperAdmin && DepartmentID == ""))
        //    {
        //        var result = from t1 in IRepository.GetALL()
        //                     join pc in _projectClintIRepository.GetALL()
        //                     on t1.ProjectID equals pc.ProjectID


        //                     where pc.ClientID == clientID
        //                     && t1.ClientID == clientID
        //                     select t1;

        //        lst = result.ToList();
        //    }
        //    if (!string.IsNullOrEmpty(DepartmentID))
        //    {


        //        if (Convert.ToInt32(DepartmentID) == (int)perRole.BA) // ba 
        //        {

        //            long EmpID = Convert.ToInt64(id.ToString());

        //            var result = from t1 in IRepository.GetALL()
        //                         join pe in _ProjectEmpIRepository.GetALL()
        //                         on t1.ProjectID equals pe.ProjectID
        //                         where pe.EmpID == EmpID && (t1.statusID == (int)enums.Status.waitingforBA
        //                         || (t1.AssignTo == EmpID && t1.statusID == (int)enums.Status.pending))   // test  waiting for ba 

        //                         select t1;
        //            lst = result.ToList();
        //        }

        //        if (Convert.ToInt32(DepartmentID) == (int)perRole.Developer) // dev
        //        {
        //            long EmpID = Convert.ToInt64(id.ToString());
        //            var result = from t1 in IRepository.GetALL()
        //                         join pe in _ProjectEmpIRepository.GetALL()
        //                         on t1.ProjectID equals pe.ProjectID
        //                         where pe.EmpID == EmpID &&
        //                       (t1.statusID == (int)enums.Status.GotoDeveloper
        //                       || t1.statusID == (int)enums.Status.approve
        //                       || (t1.AssignTo == EmpID && t1.statusID == (int)enums.Status.pending))//  
        //                         select t1;
        //            lst = result.ToList();
        //        }
        //    }
        //    else if (Convert.ToInt32(id) == (int)perRole.SuperAdmin)
        //    {
        //        //lst = /*db.Tickets.ToList();*/
        //        lst = IRepository.GetALL().ToList();

        //    }
        //    return lst;
        //}

        //public IEnumerable<Project> Create(int clientID)
        //{
        //    var data = from pc in _projectClintIRepository.GetALL()
        //               join p in _ProjectRepository.GetALL() on pc.ProjectID equals p.ID
        //               where pc.ClientID == clientID
        //               select new
        //               {
        //                   roleID = p.ID,
        //                   roleNom = p.Name
        //               };
        //    return data;
        //    //SelectList list = new SelectList(data, "roleID", "roleNom");
        //}

        public IEnumerable<ProjectModel> selectListProject(int clientID) 
        {
            var data = from pc in _IRepositoryprojectClinet.GetALL()
                       join p in _IRepositoryProject.GetALL()
                       on pc.ProjectID equals p.ID
                       where pc.ClientID == clientID
                        && p.statusID == (int)enums.Status.approve // new 
                       select new
                       {
                           ProjectID = p.ID,
                           ProjectName = p.Name
                       };
            IEnumerable<ProjectModel> projects = data.Select(u => new ProjectModel
            {
                ID = u.ProjectID,
                Name = u.ProjectName,
            });
            return projects;
        }
        public TicketModel GetID(int ID)
        {
            TicketModel Model = new TicketModel();

            Ticket Ticket = IRepository.GetID(ID);

            Model.Description = Ticket.Description;
            Model.ProjectName = Ticket.Project.Name;
            Model.statusName = Ticket.Status.Name;
            Model.ProjectID = Ticket.ProjectID;
            Model.ClientID = Ticket.ClientID;
            Model.AssignTo = Ticket.AssignTo;
            Model.id = Ticket.id;


            return Model;
        }

        public void Insert(TicketModel Model, string id)
        {
            Ticket obj = new Ticket();
            obj.id = Model.id;
            obj.Description = Model.Description;
            obj.ProjectID = Model.ProjectID;
            obj.statusID = Model.statusID;

            if (Convert.ToInt32(id) != (int)perRole.SuperAdmin)
            {
                obj.ClientID = int.Parse(id);

            }
                IRepository.Insert(obj);
           
        }

        public void Update(TicketModel Model, string id)
        {
            Ticket obj = new Ticket();
            obj.id = Model.id;
            obj.Description = Model.Description;
            obj.ProjectID = Model.ProjectID;
            obj.statusID = Model.statusID;

            if (Convert.ToInt32(id) != (int)perRole.SuperAdmin)
            {
                obj.ClientID = int.Parse(id);

            }
            IRepository.Update(obj);
        }

        public void InsertEditStatusBA(int id) 
        {

            var resultado = (from p in IRepository.GetALL() where p.id == id select p).SingleOrDefault();
            resultado.statusID = (int)enums.Status.GotoDeveloper;
            IRepository.Update(resultado); 
           
        }

        public void InsertEditRejectStatus(int id) 
        {
            var resultado = (from p in IRepository.GetALL() where p.id == id select p).SingleOrDefault();
            resultado.statusID = (int)enums.Status.reject;
            IRepository.Update(resultado);
        }

        public void InsertEditStatusDeveloper(int id)
        {
            var resultado = (from p in IRepository.GetALL() where p.id == id select p).SingleOrDefault();
            resultado.statusID = (int)enums.Status.closed; // closed 
            IRepository.Update(resultado); 
        }

        public void InsertEditStatusPending(int id, string UserID)
        {
            var resultado = (from p in IRepository.GetALL() where p.id == id select p).SingleOrDefault();
            //resultado.statusID = 6; // pending 
            resultado.statusID = (int)enums.Status.pending; // pending 
            resultado.AssignTo = Convert.ToInt32(UserID);
            IRepository.Update(resultado);

        }
    }
}
