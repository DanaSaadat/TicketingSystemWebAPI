using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IApprovalService
    {

        IEnumerable<ApprovalModel> GetALL(int ID);
        IEnumerable<ApprovalModel> GetALLstatus(int projectID);
        ApprovalModel GetID(int ID, int projectID);
        ApprovalModel Insert(int? id, int projectID);
        ApprovalModel RejectManager(ApprovalModel Model);
    }
}
