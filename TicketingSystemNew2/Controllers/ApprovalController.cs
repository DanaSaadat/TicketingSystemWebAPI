using Service;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketingSystemNew2.CustomFilter;

namespace TicketingSystemNew2.Controllers
{
    [AuditAttribute]
    public class ApprovalController : Controller
    {
        private IApprovalService IApprovalService;

        public ApprovalController(ApprovalService ApprovalService)
        {
            this.IApprovalService = ApprovalService;
           
        }

        // GET: Approval
        //public ActionResult Index()
        //{
        //    List<Project> lst = new List<Project>();
        //    List<Approval> lst1 = new List<Approval>();



        //    var id = Session["UserID"].ToString();
        //    var DepartmentID = Session["DepartmentID"].ToString();
        //    long ID = Convert.ToInt64(id.ToString());
        //    long Department = Convert.ToInt64(DepartmentID.ToString());




        //    var dd = from Approval in db.Approvals
        //                 //where Approval.statusID ==7 
        //             where Approval.ManagerID == ID
        //             select Approval;
        //    lst1 = dd.ToList();

        //    return View(lst1);
        //}

        public ActionResult Index()
        {
            var id = Session["UserID"].ToString();
            int ID = Convert.ToInt32(id.ToString());
            var lstApproval = IApprovalService.GetALL(ID);

            return View(lstApproval);
        }
        public ActionResult ApproveManager(int? id, int projectID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            ApprovalModel  Approval  =   IApprovalService.GetID(id.Value, projectID);
            ApprovalModel Approval1 = IApprovalService.Insert(id, projectID);
            if(Approval1.CheckAllStatus == false)
            {
                return RedirectToAction("Index");
            }

            if (Approval == null)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index");

        }


        public ActionResult RejectManager(int? id, int projectID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApprovalModel Approval = IApprovalService.GetID(id.Value, projectID);

            if (Approval == null)
            {
                return HttpNotFound();
            }
            return View(Approval);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RejectManager(ApprovalModel Model)
        {
           
            if (Model.RejectReason != null)
            {

                ApprovalModel Approval1 = IApprovalService.RejectManager(Model);
                if (Approval1.CheckAllStatus == false)
                {
                    return RedirectToAction("Index");
                }

            TempData["AlertMsg"] = "Updated successfully";
                return RedirectToAction("Index");
            }
            return View(Model);
        }
    }
}