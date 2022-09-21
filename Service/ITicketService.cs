using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Models;


namespace Service
{
    public interface ITicketService
    {

        //IEnumerable<TicketModel> GetALL();
        void Insert(TicketModel Model, string id);

        void Update(TicketModel Model, string id);

        void Delete(int ID);
        //void Delete(TicketModel Model);
        //void Delete(IEnumerable<TicketModel> Model); 
        TicketModel GetID(int ID);

        //IEnumerable<TicketModel> GetALL1(int clientId);
        //IEnumerable<TicketModel> GetALLnew(string id, string DepartmentID, int clientID);
        IEnumerable<TicketModel> GetALLTicketModel(string id, string DepartmentID, int clientID);
        IEnumerable<ProjectModel> selectListProject(int clientID); 
        void InsertEditStatusBA(int id);
        void InsertEditRejectStatus(int id);
        void InsertEditStatusDeveloper(int id); 
        void InsertEditStatusPending(int id,string UserID);  
    }
}
