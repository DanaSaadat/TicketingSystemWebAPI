
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Service.Models;
using TicketingSystemNew2.CustomFilter;

namespace TicketingSystemNew2.Controllers
{
    [AuditAttribute]
    public class ProjectController : Controller
    {


        private IProjectService IProjectService;
        private IProjectEmpService IProjectEmpService;
        private IProjectClientService IProjectClientService;
        private IUserService IUserService;
        private IApprovalService IApprovalService;

        public ProjectController(ProjectService IProjectService, ProjectEmpService iProjectEmpService, ProjectClientService iProjectClientService, UserService iUserService,ApprovalService iApprovalService)
        {
            this.IProjectService = IProjectService;
            IProjectEmpService = iProjectEmpService;
            IProjectClientService = iProjectClientService;
            IUserService = iUserService;
            IApprovalService = iApprovalService;
        }




        [Authorize(Roles = "Project")]
        public ActionResult Index()
        {
         
         return View(IProjectService.GetALL());
        }



        public ActionResult status(int id)
        {
           
            var Lststatus = IApprovalService.GetALLstatus(id).Where(x => x.ProjectID == id).ToList();
            return View(Lststatus); 
        }


        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ProjectEmp = new SelectList(IUserService.GetALL().Where(g => g.IsClient == false).Where(x => x.IsDelete.Equals(false)).ToList(), "UserID", "UserName");
            ViewBag.ProjectClient = new SelectList(IUserService.GetALLClient().Where(g => g.IsClient == true).Where(x => x.IsDelete.Equals(false)).ToList(), "UserID", "UserName");



            return View();
        }
        //[Authorize(Roles = "AddProject")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create1(ProjectModel model, int[] ProjectEmp, int[] ProjectClient)
        //{
        //    //    if (ModelState.IsValid)
        //    if (model.Name != null)
        //    {
              

        //        IProjectService.Insert(model);
        //        ProjectEmp obj1 = new ProjectEmp();
        //        ProjectClient obj2 = new ProjectClient();
               
        //        //db.Projects.Add(obj);
        //        //db.SaveChanges();
        //        TempData["AlertMsg"] = "Saved successfully";
        //        if (ProjectEmp != null)
        //        {


        //            foreach (var xx in ProjectEmp)
        //            {

        //                obj1.EmpID = xx;


        //                obj1.ProjectID = obj.ID;

        //                IProjectEmpService.Insert(obj1);
                      

        //            }
        //        }
        //        if (ProjectClient != null)
        //        {
        //            foreach (var xx in ProjectClient)
        //            {

        //                obj2.ClientID = xx;


        //                obj2.ProjectID = obj.ID;

        //                IProjectClientService.Insert(obj2);
                     

        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ProjectEmp = new SelectList(db.Logins.Where(g => g.IsClient == false).Where(x => x.IsDelete.Equals(false)).ToList(), "UserID", "UserName");
        //    ViewBag.ProjectClient = new SelectList(db.Logins.Where(g => g.IsClient == true).Where(x => x.IsDelete.Equals(false)).ToList(), "UserID", "UserName");
        //    return View(model);

        //}

        [Authorize(Roles = "AddProject")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectModel model, int[] ProjectEmp, int[] ProjectClient)
        {
            if (model.Name != null)
            {


              ProjectModel pm =  IProjectService.Insert(model, ProjectEmp, ProjectClient); 
                if (ProjectEmp != null &&  pm.ManagerID == null )
                {

                    var uu = pm.mesage;

                    TempData["AlertMsg2"] = "This department for this employee  " + ' ' + uu + ' ' + " has no manager..Please appoint a manager";
                    ViewBag.ProjectEmp = new SelectList(IUserService.GetALL().Where(g => g.IsClient == false).Where(x => x.IsDelete.Equals(false)).ToList(), "UserID", "UserName");
                    ViewBag.ProjectClient = new SelectList(IUserService.GetALLClient().Where(g => g.IsClient == true).Where(x => x.IsDelete.Equals(false)).ToList(), "UserID", "UserName");

                    return View(model);
                }
              

                TempData["AlertMsg"] = "Saved successfully";
              
                return RedirectToAction("Index");
            }

            ViewBag.ProjectEmp = new SelectList(IUserService.GetALL().Where(g => g.IsClient == false).Where(x => x.IsDelete.Equals(false)).ToList(), "UserID", "UserName");
            ViewBag.ProjectClient = new SelectList(IUserService.GetALLClient().Where(g => g.IsClient == true).Where(x => x.IsDelete.Equals(false)).ToList(), "UserID", "UserName");
            return View(model);

        }

        //[Authorize]
        //public ActionResult Edit(int? id)
        //{


        //    ViewBag.ProjectEmp1 = new SelectList(db.Logins.Where(g => g.IsClient == false).Where(x => x.IsDelete.Equals(false)).ToList(), "UserID", "UserName");

        //    ViewBag.ProjectEmp = db.ProjectEmps.Include("Login").Where(x => x.ProjectID == id).Select(x => x.Login.UserID).ToArray();



        //    ViewBag.ProjectClient1 = new SelectList(db.Logins.Where(g => g.IsClient == true).Where(x => x.IsDelete.Equals(false)).ToList(), "UserID", "UserName");
        //    ViewBag.ProjectClient = db.ProjectClients.Include("Login").Where(x => x.ProjectID == id).Select(x => x.Login.UserID).ToArray();


        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProjectModel model = new ProjectModel();
        //    Project Project = IProjectService.GetID(id.Value);
        //    model.Name = Project.Name;

        //    if (Project == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(model);
        //}


        [Authorize]
        public ActionResult Edit(int? id)
        {


            ViewBag.ProjectEmp1 = new SelectList(IUserService.GetALL().Where(g => g.IsClient == false).Where(x => x.IsDelete.Equals(false)).ToList(), "UserID", "UserName");

            ViewBag.ProjectEmp = IProjectEmpService.GetALL().Where(x => x.ProjectID == id).Select(x => x.Login.UserID).ToArray();



            ViewBag.ProjectClient1 = new SelectList(IUserService.GetALLClient().Where(g => g.IsClient == true).Where(x => x.IsDelete.Equals(false)).ToList(), "UserID", "UserName");
            ViewBag.ProjectClient = IProjectClientService.GetALL().Where(x => x.ProjectID == id).Select(x => x.Login.UserID).ToArray();


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
         
            ProjectModel model = IProjectService.GetID(id.Value);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        //[Authorize(Roles = "UpdateProject")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit1(ProjectModel model, int[] ProjectEmp1, int[] ProjectClient1)
        //{

        //    ProjectEmp obj1 = new ProjectEmp();
        //    ProjectClient obj2 = new ProjectClient();


        //    var proj = db.ProjectEmps.Include("Login").Where(x => x.ProjectID == model.ID).ToList();
        //    foreach (var oldEmp in proj)
        //    {
        //        db.ProjectEmps.Remove(oldEmp);
        //        db.SaveChanges();
        //    }


        //    var pro1 = db.ProjectClients.Include("Login").Where(x => x.ProjectID == model.ID).ToList();
        //    foreach (var oldclient in pro1)
        //    {
        //        db.ProjectClients.Remove(oldclient);
        //        db.SaveChanges();
        //    }
        //    if (ProjectEmp1 != null)
        //    {

        //        foreach (var xx in ProjectEmp1)
        //        {

        //            obj1.EmpID = xx;

        //            obj1.ProjectID = model.ID;
        //            db.ProjectEmps.Add(obj1);
        //            db.SaveChanges();

        //        }
        //    }

        //    if (ProjectClient1 != null)
        //    {
        //        foreach (var xx in ProjectClient1)
        //        {

        //            obj2.ClientID = xx;


        //            obj2.ProjectID = model.ID;
        //            db.ProjectClients.Add(obj2);
        //            db.SaveChanges();

        //        }
        //    }
        //    Project obj = new Project();
        //    obj.Name = model.Name;
        //    obj.ID = model.ID;

        //    IProjectService.Update(obj);
          
        //    TempData["AlertMsg"] = "Updated successfully";

        //    return RedirectToAction("Index");
          
        //}


        //[Authorize(Roles = "UpdateProject")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editold(ProjectModel model, int[] ProjectEmp1, int[] ProjectClient1)
        {

            //ProjectEmp obj1 = new ProjectEmp();
            //ProjectClient obj2 = new ProjectClient();


            //var proj = db.ProjectEmps.Include("Login").Where(x => x.ProjectID == model.ID).ToList();
            //foreach (var oldEmp in proj)
            //{
            //    db.ProjectEmps.Remove(oldEmp);
            //    db.SaveChanges();
            //}


            //var pro1 = db.ProjectClients.Include("Login").Where(x => x.ProjectID == model.ID).ToList();
            //foreach (var oldclient in pro1)
            //{
            //    db.ProjectClients.Remove(oldclient);
            //    db.SaveChanges();
            //}
            //if (ProjectEmp1 != null)
            //{

            //    foreach (var xx in ProjectEmp1)
            //    {

            //        obj1.EmpID = xx;

            //        obj1.ProjectID = model.ID;
            //        db.ProjectEmps.Add(obj1);
            //        db.SaveChanges();

            //    }
            //}

            //if (ProjectClient1 != null)
            //{
            //    foreach (var xx in ProjectClient1)
            //    {

            //        obj2.ClientID = xx;


            //        obj2.ProjectID = model.ID;
            //        db.ProjectClients.Add(obj2);
            //        db.SaveChanges();

            //    }
            //}
            //Project obj = new Project();
            //obj.Name = model.Name;
            //obj.ID = model.ID;

            IProjectService.Update(model, ProjectEmp1, ProjectClient1);

            TempData["AlertMsg"] = "Updated successfully";

            return RedirectToAction("Index");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectModel model, int[] ProjectEmp1, int[] ProjectClient1)
        {

            if (model.Name != null)
            {
                ProjectModel pm = IProjectService.Update2(model, ProjectEmp1, ProjectClient1);
                if (ProjectEmp1 != null && pm.ManagerID == null)
                {

                    var uu = pm.mesage;

                    TempData["AlertMsg2"] = "This department for this employee  " + ' ' + uu + ' ' + " has no manager..Please appoint a manager";


                    ViewBag.ProjectEmp1 = new SelectList(IUserService.GetALL().Where(g => g.IsClient == false).Where(x => x.IsDelete.Equals(false)).ToList(), "UserID", "UserName");
                    ViewBag.ProjectClient1 = new SelectList(IUserService.GetALLClient().Where(g => g.IsClient == true).Where(x => x.IsDelete.Equals(false)).ToList(), "UserID", "UserName");

                    ViewBag.ProjectEmp = IProjectEmpService.GetALL().Where(x => x.ProjectID == model.ID).Select(x => x.Login.UserID).ToArray();
                    ViewBag.ProjectClient = IProjectClientService.GetALL().Where(x => x.ProjectID == model.ID).Select(x => x.Login.UserID).ToArray();
                    return View(model);
                }


                TempData["AlertMsg"] = "Saved successfully";

                return RedirectToAction("Index");
            }
            ViewBag.ProjectEmp = new SelectList(IUserService.GetALL().Where(g => g.IsClient == false).Where(x => x.IsDelete.Equals(false)).ToList(), "UserID", "UserName");
            ViewBag.ProjectClient = new SelectList(IUserService.GetALLClient().Where(g => g.IsClient == true).Where(x => x.IsDelete.Equals(false)).ToList(), "UserID", "UserName");
            ViewBag.ProjectEmp1 = new SelectList(IUserService.GetALL().Where(g => g.IsClient == false).Where(x => x.IsDelete.Equals(false)).ToList(), "UserID", "UserName");
            ViewBag.ProjectClient1 = new SelectList(IUserService.GetALLClient().Where(g => g.IsClient == true).Where(x => x.IsDelete.Equals(false)).ToList(), "UserID", "UserName");

            return View(model);
        

        }


        //[Authorize]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    ProjectModel model = new ProjectModel();
        //    Project Project = IProjectService.GetID(id.Value);
        //    model.Name = Project.Name;


        //    if (Project == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(model);
        //}


        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ProjectModel = IProjectService.GetID(id.Value); 

            if (ProjectModel == null)
            {
                return HttpNotFound();
            }
            return View(ProjectModel);
        }


        //[Authorize(Roles = "DeleteProject")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id)
        //{
        //    Project Project = IProjectService.GetID(id);

        //    var ProID = db.ProjectEmps.Where(x => x.ProjectID == id);
        //    if (ProID.Any())
        //    {
        //        db.ProjectEmps.RemoveRange(ProID);
        //        db.SaveChanges();
        //    }

        //    var proClient = db.ProjectClients.Where(x => x.ProjectID == id);
        //    if (proClient.Any())
        //    {
        //        db.ProjectClients.RemoveRange(proClient);
        //        db.SaveChanges();
        //    }
        //    IProjectService.Delete(Project.ID);

        //    TempData["AlertMsg"] = "Deleted successfully";
        //    return RedirectToAction("Index");
        //}
        [Authorize(Roles = "DeleteProject")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
           
            IProjectService.Delete(id);

            TempData["AlertMsg"] = "Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}