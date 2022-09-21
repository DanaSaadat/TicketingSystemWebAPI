
using Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketingSystemNew2.enums;
using Service.Models;
using TicketingSystemNew2.CustomFilter;

namespace TicketingSystemNew2.Controllers
{
    [AuditAttribute]
    public class TicketController : Controller
    {

        private ITicketService ITicketService;
        private IProjectClientService IProjectClientService;
        private IProjectService IProjectService;
        public TicketController(TicketService ITicketService, ProjectClientService iProjectClientService, ProjectService iProjectService)
        {
            this.ITicketService = ITicketService;
            IProjectClientService = iProjectClientService;
            IProjectService = iProjectService;
        }

        //[Authorize(Roles = "Ticket")]
        //public ActionResult Index1()
        //{

        //    var id = Session["UserID"].ToString();
        //    var DepartmentID = Session["DepartmentID"].ToString();


        //    int clientID = Convert.ToInt32(id.ToString());
        //    List<Ticket> lst = new List<Ticket>();

        //    if (Convert.ToInt32(id) != (int)perRole.SuperAdmin && DepartmentID == null || (Convert.ToInt32(id) != (int)perRole.SuperAdmin && DepartmentID == ""))
        //    {
        //        var result = from t1 in db.Tickets
        //                     join pc in db.ProjectClients
        //                     on t1.ProjectID equals pc.ProjectID


        //                     where pc.ClientID == clientID
        //                     && t1.ClientID == clientID
        //                     select t1;
                            
        //        lst = result.ToList();
        //    }
        //    if (!string.IsNullOrEmpty(Session["DepartmentID"].ToString()))
        //    {


        //        if (Convert.ToInt32(DepartmentID) == (int)perRole.BA) // ba 
        //        {
        //            Session["UserID"] = id;
        //            Session["DepartmentID"] = DepartmentID;
        //            long EmpID = Convert.ToInt64(id.ToString());

        //            var result = from t1 in db.Tickets
        //                         join pe in db.ProjectEmps
        //                         on t1.ProjectID equals pe.ProjectID
        //                         where pe.EmpID == EmpID && (t1.statusID == (int)enums.Status.waitingforBA 
        //                         || (t1.AssignTo == EmpID && t1.statusID == (int)enums.Status.pending))   // test  waiting for ba 
                                                                                                                                                                                    
        //                         select t1;
        //            lst = result.ToList();
        //        }

        //        if (Convert.ToInt32(DepartmentID) == (int)perRole.Developer) // dev
        //        {
        //            Session["UserID"] = id;
        //            long EmpID = Convert.ToInt64(id.ToString());
        //            var result = from t1 in db.Tickets
        //                         join pe in db.ProjectEmps
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
        //        lst = db.Tickets.ToList();

        //    }
        //    return View(lst);
        //}



        //[Authorize(Roles = "Ticket")]
        //public ActionResult Index1()
        //{

        //    var id = Session["UserID"].ToString();
        //    var DepartmentID = Session["DepartmentID"].ToString();


        //    int clientID = Convert.ToInt32(id.ToString());
        //    List<Ticket> lst = new List<Ticket>();

        //    if (Convert.ToInt32(id) != (int)perRole.SuperAdmin && DepartmentID == null || (Convert.ToInt32(id) != (int)perRole.SuperAdmin && DepartmentID == ""))
        //    {
        //        var result = from t1 in db.Tickets
        //                     join pc in db.ProjectClients
        //                     on t1.ProjectID equals pc.ProjectID


        //                     where pc.ClientID == clientID
        //                     && t1.ClientID == clientID
        //                     select t1;

        //        lst = result.ToList();
        //    }
        //    if (!string.IsNullOrEmpty(Session["DepartmentID"].ToString()))
        //    {


        //        if (Convert.ToInt32(DepartmentID) == (int)perRole.BA) // ba 
        //        {
        //            Session["UserID"] = id;
        //            Session["DepartmentID"] = DepartmentID;
        //            long EmpID = Convert.ToInt64(id.ToString());

        //            var result = from t1 in db.Tickets
        //                         join pe in db.ProjectEmps
        //                         on t1.ProjectID equals pe.ProjectID
        //                         where pe.EmpID == EmpID && (t1.statusID == (int)enums.Status.waitingforBA
        //                         || (t1.AssignTo == EmpID && t1.statusID == (int)enums.Status.pending))   // test  waiting for ba 

        //                         select t1;
        //            lst = result.ToList();
        //        }

        //        if (Convert.ToInt32(DepartmentID) == (int)perRole.Developer) // dev
        //        {
        //            Session["UserID"] = id;
        //            long EmpID = Convert.ToInt64(id.ToString());
        //            var result = from t1 in db.Tickets
        //                         join pe in db.ProjectEmps
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
        //        lst = db.Tickets.ToList();

        //    }
        //    return View(lst);
        //}


        [Authorize(Roles = "Ticket")]
        public ActionResult Index()
        {

            var id = Session["UserID"].ToString();
            var DepartmentID = Session["DepartmentID"].ToString();


            int clientID = Convert.ToInt32(id.ToString());


          var GetALLTicketModel = ITicketService.GetALLTicketModel(id, DepartmentID, clientID); 

          

            return View(GetALLTicketModel);  
        }


        //[Authorize]
        //public ActionResult Create1()
        //{

        //    var id = Session["UserID"].ToString();
        //    long clientID = Convert.ToInt64(id.ToString());

        //    var data = from pc in db.ProjectClients
        //               join p in db.Projects on pc.ProjectID equals p.ID
        //               where pc.ClientID == clientID
        //               select new
        //               {
        //                   roleID = p.ID,
        //                   roleNom = p.Name
        //               };

        //    SelectList list = new SelectList(data, "roleID", "roleNom");
        //    if (Convert.ToInt32(id) != (int)perRole.SuperAdmin)
        //    //if (id != "1")
        //    {
        //        ViewBag.ProjectID = list;
        //    }
        //    else
        //    {
        //        ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Name");

        //    }

        //    return View();
        //}

        //[Authorize]
        //public ActionResult Create()
        //{

        //    var id = Session["UserID"].ToString();
        //    long clientID = Convert.ToInt64(id.ToString());

        //    var data = from pc in IProjectClientService.GetALL()
        //               join p in IProjectService.GetALL() on pc.ProjectID equals p.ID
        //               where pc.ClientID == clientID
        //               select new
        //               {
        //                   roleID = p.ID,
        //                   roleNom = p.Name
        //               };

        //    SelectList list = new SelectList(data, "roleID", "roleNom");
        //    if (Convert.ToInt32(id) != (int)perRole.SuperAdmin)
        //    {
        //        ViewBag.ProjectID = list;
        //    }
        //    else
        //    {
        //        ViewBag.ProjectID = new SelectList(IProjectService.GetALL(), "ID", "Name");

        //    }

        //    return View();
        //}


        [Authorize]
        public ActionResult Create()
        {

            var id = Session["UserID"].ToString();
            int clientID = Convert.ToInt32(id.ToString());

           
            var lstProject = ITicketService.selectListProject(clientID);


            SelectList list = new SelectList(lstProject, "ID", "Name");
            if (Convert.ToInt32(id) != (int)perRole.SuperAdmin)
            {
                ViewBag.ProjectID = list;
            }
            else
            {
                ViewBag.ProjectID = new SelectList(IProjectService.GetALL(), "ID", "Name");

            }

            return View();
        }


        //[Authorize(Roles = "AddTicket")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create1(Ticket obj)
        //{

        //    var id = Session["UserID"].ToString();
        //    if (ModelState.IsValid)
        //    {
        //        if (Convert.ToInt32(id) != (int)perRole.SuperAdmin)
        //        {
        //            obj.ClientID = int.Parse(id);
        //        }
        //        db.Tickets.Add(obj);
        //        db.SaveChanges();
        //        TempData["AlertMsg"] = "Saved successfully";
        //        return RedirectToAction("Index");
        //    }
        //    long clientID = Convert.ToInt64(id.ToString());

        //    var data = from pc in db.ProjectClients
        //               join p in db.Projects on pc.ProjectID equals p.ID
        //               where pc.ClientID == clientID
        //               select new
        //               {
        //                   roleID = p.ID,
        //                   roleNom = p.Name
        //               };

        //    SelectList list = new SelectList(data, "roleID", "roleNom");


        //    if (Convert.ToInt32(id) != (int)perRole.SuperAdmin)
        //    {
        //        ViewBag.ProjectID = list;
        //    }
        //    else
        //    {
        //        ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Name");

        //    }


        //    return View(obj);
        //}


        //[Authorize(Roles = "AddTicket")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(TicketModel Model)
        //{

        //    var id = Session["UserID"].ToString();
        //    if (ModelState.IsValid)
        //    {
        //        Ticket obj = new Ticket();
        //        obj.id = Model.id;
        //        obj.Description = Model.Description;
        //        obj.ProjectID = Model.ProjectID;
        //        obj.statusID = Model.statusID;
        //     if (Convert.ToInt32(id) != (int)perRole.SuperAdmin)
        //        {
        //            obj.ClientID = int.Parse(id);

        //        }
        //        ITicketService.Insert(obj);
              
        //        TempData["AlertMsg"] = "Saved successfully";
        //        return RedirectToAction("Index");
        //    }
        //    long clientID = Convert.ToInt64(id.ToString());

        //    var data = from pc in IProjectClientService.GetALL()
        //               join p in IProjectService.GetALL() on pc.ProjectID equals p.ID
        //               where pc.ClientID == clientID
        //               select new
        //               {
        //                   roleID = p.ID,
        //                   roleNom = p.Name
        //               };

        //    SelectList list = new SelectList(data, "roleID", "roleNom");


        //    if (Convert.ToInt32(id) != (int)perRole.SuperAdmin)
        //    {
        //        ViewBag.ProjectID = list;
        //    }
        //    else
        //    {
        //        ViewBag.ProjectID = new SelectList(IProjectService.GetALL(), "ID", "Name");

        //    }


        //    return View(Model);
        //}



        [Authorize(Roles = "AddTicket")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TicketModel Model)
        {

            var id = Session["UserID"].ToString();
                if (Model.Description != null && Model.ProjectID != null)
                
                    {
            
                ITicketService.Insert(Model,id);

                TempData["AlertMsg"] = "Saved successfully";
                return RedirectToAction("Index");
                   }
            int clientID = Convert.ToInt32(id.ToString());

            var lstProject = ITicketService.selectListProject(clientID);

            SelectList list = new SelectList(lstProject, "ID", "Name");


            if (Convert.ToInt32(id) != (int)perRole.SuperAdmin)
            {
                ViewBag.ProjectID = list;
            }
            else
            {
                ViewBag.ProjectID = new SelectList(IProjectService.GetALL(), "ID", "Name");

            }


            return View(Model);
        }


        //[Authorize]
        //public ActionResult Edit1(int? id)
        //{
        //    var clientid = Session["UserID"].ToString();
        //    long clientID = Convert.ToInt64(clientid.ToString());
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Ticket Ticket = db.Tickets.Find(id);
        //    if (Ticket == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var data = from pc in db.ProjectClients
        //               join p in db.Projects on pc.ProjectID equals p.ID
        //               where pc.ClientID == clientID
        //               select new
        //               {
        //                   roleID = p.ID,
        //                   roleNom = p.Name
        //               };

        //    SelectList list = new SelectList(data, "roleID", "roleNom");
        //    if (clientID != (int)perRole.SuperAdmin)
        //    {
        //        ViewBag.ProjectID = list;
        //    }
        //    else
        //    {
        //        ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Name");

        //    }
        //    return View(Ticket);
        //}



        //[Authorize]
        //public ActionResult Edit(int? id)
        //{
        //    TicketModel Model = new TicketModel();
        //    var clientid = Session["UserID"].ToString();
        //    long clientID = Convert.ToInt64(clientid.ToString());
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Ticket Ticket = ITicketService.GetID(id.Value);
        //    Model.Description = Ticket.Description;
        //    Model.ProjectID = Ticket.ProjectID;
        //    if (Ticket == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var data = from pc in IProjectClientService.GetALL()
        //               join p in IProjectService.GetALL() on pc.ProjectID equals p.ID
        //               where pc.ClientID == clientID
        //               select new
        //               {
        //                   roleID = p.ID,
        //                   roleNom = p.Name
        //               };

        //    SelectList list = new SelectList(data, "roleID", "roleNom");
        //    if (clientID != (int)perRole.SuperAdmin)
        //    {
        //        ViewBag.ProjectID = list;
        //    }
        //    else
        //    {
        //        ViewBag.ProjectID = new SelectList(IProjectService.GetALL(), "ID", "Name");

        //    }
        //    return View(Model);
        //}


        [Authorize]
        public ActionResult Edit(int? id)
        {
            var clientid = Session["UserID"].ToString();
            int clientID = Convert.ToInt32(clientid.ToString());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
         
            TicketModel Model = ITicketService.GetID(id.Value);
            if (Model == null)
            {
                return HttpNotFound();
            }
           

            var lstProject = ITicketService.selectListProject(clientID);
            SelectList list = new SelectList(lstProject, "ID", "Name"); 

            if (clientID != (int)perRole.SuperAdmin)
            {
                ViewBag.ProjectID = list;
            }
            else
            {
                ViewBag.ProjectID = new SelectList(IProjectService.GetALL(), "ID", "Name");

            }
            return View(Model);
        }


        //[Authorize(Roles = "UpdateTicket")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit1(Ticket obj)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        db.Entry(obj).State = EntityState.Modified;
        //        db.SaveChanges();
        //        TempData["AlertMsg"] = "Updated successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View(obj);
        //}


        //[Authorize(Roles = "UpdateTicket")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(TicketModel Model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Ticket obj = new Ticket();
        //        obj.id = Model.id;
        //        obj.Description = Model.Description;
        //        obj.ProjectID = Model.ProjectID;
        //        obj.statusID = Model.statusID;
        //        ITicketService.Update(obj);
            
        //        TempData["AlertMsg"] = "Updated successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View(Model);
        //}

        [Authorize(Roles = "UpdateTicket")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TicketModel Model)
        {
            var id = Session["UserID"].ToString();
            if (ModelState.IsValid)
            {
               
                ITicketService.Update(Model, id); 

                TempData["AlertMsg"] = "Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(Model);
        }


        //[Authorize]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Ticket Ticket = db.Tickets.Find(id);

        //    if (Ticket == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(Ticket);
        //}

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             
            var TicketModel = ITicketService.GetID(id.Value);

            if (TicketModel == null)
            {
                return HttpNotFound();
            }
            return View(TicketModel);
        }


        //[Authorize(Roles = "DeleteTicket")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id)
        //{
        //    Ticket Ticket = db.Tickets.Find(id);
        //    db.Tickets.Remove(Ticket);
        //    db.SaveChanges();
        //    TempData["AlertMsg"] = "Deleted successfully";

        //    return RedirectToAction("Index");
        //}


        [Authorize(Roles = "DeleteTicket")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
          
            ITicketService.Delete(id);
            TempData["AlertMsg"] = "Deleted Successfully";

            return RedirectToAction("Index");
        }
        //public ActionResult EditStatusBA(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Ticket Ticket = db.Tickets.Find(id);

        //    var resultado = (from p in db.Tickets where p.id == id select p).SingleOrDefault();
        //    resultado.statusID = (int)enums.Status.GotoDeveloper;
        //    db.SaveChanges();

        //    if (Ticket == null)
        //    {
        //        return HttpNotFound();
        //    }
     
        //    return RedirectToAction("Index");
        //}

        public ActionResult EditStatusBA(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           TicketModel Model = ITicketService.GetID(id.Value);

            ITicketService.InsertEditStatusBA(id.Value);
          

            if (Model == null)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index");
        }

        //public ActionResult EditRejectStatus(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Ticket Ticket = db.Tickets.Find(id);

        //    var resultado = (from p in db.Tickets where p.id == id select p).SingleOrDefault();
        //    resultado.statusID = (int)enums.Status.reject;
        //    db.SaveChanges();

        //    if (Ticket == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return RedirectToAction("Index");

        //}
        public ActionResult EditRejectStatus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketModel Model = ITicketService.GetID(id.Value);

            ITicketService.InsertEditRejectStatus(id.Value);
          
            if (Model == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");

        }

        //public ActionResult EditStatusDeveloper(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Ticket Ticket = db.Tickets.Find(id);

        //    var resultado = (from p in db.Tickets where p.id == id select p).SingleOrDefault();
        //    resultado.statusID = (int)enums.Status.closed; // closed 
        //    db.SaveChanges();

        //    if (Ticket == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return RedirectToAction("Index");

        //}


        public ActionResult EditStatusDeveloper(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TicketModel Model = ITicketService.GetID(id.Value);
            
          ITicketService.InsertEditStatusDeveloper(id.Value);

          
            if (Model == null) 
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index");

        }
        //public ActionResult EditStatusPending(int? id) // ball action 
        //{
        //    var UserID = Session["UserID"].ToString();
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Ticket Ticket = db.Tickets.Find(id);

        //    var resultado = (from p in db.Tickets where p.id == id select p).SingleOrDefault();
        //    resultado.statusID = 6; // pending 
        //    resultado.AssignTo = Convert.ToInt32(UserID);
        //    db.SaveChanges();

        //    if (Ticket == null)
        //    {
        //        return HttpNotFound();
        //    }
       
        //    return RedirectToAction("Index");
        //}

        public ActionResult EditStatusPending(int? id) // ball action 
        {
            var UserID = Session["UserID"].ToString();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TicketModel Model = ITicketService.GetID(id.Value);

            ITicketService.InsertEditStatusPending(id.Value, UserID);
           

            if (Model == null)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index");
        }
    }
}