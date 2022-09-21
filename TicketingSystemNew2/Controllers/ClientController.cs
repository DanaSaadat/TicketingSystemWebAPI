
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Service.Models;
using TicketingSystemNew2.CustomFilter;

namespace TicketingSystemNew2.Controllers
{
    [AuditAttribute]
    public class ClientController : Controller
    {

        private IClientService IClientService;
        private IPermissionUserService IPermissionUserService;
        private IPermissionService IPermissionService;


        public ClientController(ClientService ClientService, PermissionUserService IUserService1, PermissionService iPermissionService)
        {
            this.IClientService = ClientService;
            this.IPermissionUserService = IUserService1;
            IPermissionService = iPermissionService;
        }

        [Authorize(Roles = "Client")]
        public ActionResult Index()
        {
            return View(IClientService.GetALL());
        }




        [Authorize(Roles = "Client")]
        public ActionResult Index1() 
        {
            ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
            ViewBag.TotalClient = IClientService.GetALL().Count();
            return View();
        }
        public JsonResult List()
        {

            //var xx = IStudentSevice.GetALL();
            return Json(IClientService.GetALL(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult getPermission()
        {
            return Json(IPermissionService.GetALL().ToList().Select(x => new
            {
                ID = x.ID,
                Name = x.Name
            }).ToList(), JsonRequestBehavior.AllowGet);



        }



        [Authorize(Roles = "AddClient")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Add(LoginModel Model, int[] Permission)
        {
            Model = IClientService.Insert2(Model, Permission);
            if (Model.IsUserNameExist == true)
            {
                //ViewData["msg"] = "User name already exists";

                //ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
                return Json(new { Error = "User Name already  Exists", IsSuccess = false });


            }

            if (!Model.IsUserNameExist)
            {
                Model.IsClient = true;
                IClientService.Insert(Model, Permission);

                


            }
            return Json(new { IsSuccess = true });
        }



        public JsonResult GetbyID(int ID)
        {
            ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
            ViewBag.PermissionUser = IPermissionUserService.GetALL().Where(x => x.UserID == ID).Select(x => x.Permission.ID).ToArray();

            return Json(IClientService.GetID(ID), JsonRequestBehavior.AllowGet);
        }


        public ActionResult getPermissionUser(int ID)
        {

            //var xx= IPermissionUserService.GetALL().Where(x => x.UserID == ID).Select(x => x.Permission.ID).ToArray();
            var xx= IPermissionUserService.GetALL().Where(x => x.UserID == ID).Select(x => new
            {
                ID = x.Permission.ID,
                Name= x.Permission.Name,

            }).ToArray();

            return Json(xx, JsonRequestBehavior.AllowGet);
           
        }






        [Authorize(Roles = "UpdateClient")]
        [HttpPost]
        public JsonResult Update(LoginModel Model, int[] Permission)
        {


            IClientService.Update(Model, Permission);
            return Json(JsonRequestBehavior.AllowGet);
        }




        public JsonResult Delete2(int ID)
        {

            //Student userEntity = IStudentSevice.GetID(ID);
            IClientService.Delete(ID);
            return Json(JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
            return View();
        }

        //[Authorize(Roles = "AddClient")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create1(LoginModel model, int[] Permission)
        //{
        //    bool IsUserNameExist = db.Logins.Any(x => x.UserName == model.UserName && x.UserID != model.UserID);

        //    if (IsUserNameExist == true)
        //    {



        //        ViewData["msg"] = "User name already exists";

        //        ViewBag.Permission = new SelectList(db.Permissions, "ID", "Name");
        //        return View();

        //    }
        //    Login obj = new Login();
        //    if (ModelState.IsValid)
        //    {
        //        if (!IsUserNameExist)
        //        {
        //            obj.UserName = model.UserName;
        //            obj.Password = model.Password;
        //            obj.UserID = model.UserID;
        //            obj.FirstName = model.FirstName;
        //            obj.LastName = model.LastName;
        //            obj.Email = model.Email;
        //            obj.Mobile = model.Mobile;
        //            obj.IsClient = model.IsClient;
        //            obj.Password = HashPass(obj.Password);
        //            IUserService.Insert(obj);
                  
        //            TempData["AlertMsg"] = "Saved successfully";
        //            if (Permission == null)
        //            {
        //                return RedirectToAction("Index");
        //            }


        //        }
        //    }
        //    PermissionUser obj1 = new PermissionUser();
        //    if (Permission != null)
        //    {

        //        foreach (var xx in Permission)
        //        {

        //            obj1.PermissionID = xx;


        //            obj1.UserID = obj.UserID;

        //            IPermissionUserService.Insert(obj1);
                

        //        }
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Permission = new SelectList(db.Permissions, "ID", "Name");
        //    return View(model);
        //}






        [Authorize(Roles = "AddClient")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoginModel Model, int[] Permission)
        {
            if (ModelState.IsValid)
            {
                Model = IClientService.Insert2(Model, Permission);
            if (Model.IsUserNameExist == true)
            {
                ViewData["msg"] = "User name already exists";

                ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
                return View();

            }
          
                if (!Model.IsUserNameExist)
                {
                  
                    IClientService.Insert(Model, Permission);

                    TempData["AlertMsg"] = "Saved successfully";
                    if (Permission == null)
                    {
                        return RedirectToAction("Index");
                    }


                }
            }
            if (Permission != null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
            return View(Model);
        }




        //[Authorize]
        //public ActionResult Edit1(int? id)
        //{
        //    LoginModel model = new LoginModel();

        //    ViewBag.Permission = new SelectList(db.Permissions, "ID", "Name");
        //    ViewBag.PermissionUser = db.PermissionUser.Include("Permission").Where(x => x.UserID == id).Select(x => x.Permission.ID).ToArray();



        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Login Login = IUserService.GetID(id.Value);
        //    model.UserID = Login.UserID;
        //    model.Password = Login.Password;
        //    model.UserName = Login.UserName;

        //    model.Email = Login.Email;
        //    model.FirstName = Login.FirstName;
        //    model.LastName = Login.LastName;
        //    model.DepartmentID = Login.DepartmentID;
        //    model.Mobile = Login.Mobile;
        //    if (Login == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(model);
        //}

        [Authorize]
        public ActionResult Edit(int? id)
        {

            ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
            ViewBag.PermissionUser = IPermissionUserService.GetALL().Where(x => x.UserID == id).Select(x => x.Permission.ID).ToArray();

            LoginModel Model = IClientService.GetID(id.Value);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
            if (Model == null)
            {
                return HttpNotFound();
            }
            return View(IClientService.GetID(id.Value));
        }






        //[Authorize(Roles = "UpdateClient")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit1(LoginModel model, int[] Permission)
        //{
        //    model.IsClient = true;
        //    PermissionUser obj1 = new PermissionUser();
        //    var permissionold = db.PermissionUser.Include("Permission").Where(x => x.UserID == model.UserID).ToList();
        //    foreach (var oldPer in permissionold)
        //    {
        //        db.PermissionUser.Remove(oldPer);
        //        db.SaveChanges();
        //    }


        //    foreach (var xx in Permission)
        //    {

        //        obj1.PermissionID = xx;

        //        obj1.UserID = model.UserID;
        //        IPermissionUserService.Insert(obj1);
              

        //    }

        //    if (ModelState.IsValid)
        //    {

        //        Login obj = new Login();
        //        obj.UserName = model.UserName;
        //        obj.Password = model.Password;
        //        obj.UserID = model.UserID;
        //        obj.FirstName = model.FirstName;
        //        obj.LastName = model.LastName;
        //        obj.Email = model.Email;
        //        obj.Mobile = model.Mobile;
        //        obj.IsClient = model.IsClient;

        //        obj.Password = HashPass(obj.Password);
        //        IUserService.Update(obj);
             
        //        TempData["AlertMsg"] = "Updated successfully";
        //        return RedirectToAction("Index");
        //    }
          
        //    return View(model);
        //}

        [Authorize(Roles = "UpdateClient")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LoginModel Model, int[] Permission)
        {
           

            if (ModelState.IsValid)
            {


                Model = IClientService.Update(Model,Permission);
                if (Model.IsUserNameExist)
                {


                    ViewData["msg"] = "User name already exists";
                    ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
                    ViewBag.PermissionUser = IPermissionUserService.GetALL().Where(x => x.UserID == Model.UserID).Select(x => x.Permission.ID).ToArray();

                    return View();

                }
                TempData["AlertMsg"] = "Updated successfully";
                return RedirectToAction("Index");
            }

            return View(Model);
        }

        //[Authorize]
        //public ActionResult Delete1(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LoginModel model = new LoginModel();
        //    Login Login = IUserService.GetID(id.Value);



        //    model.UserID = Login.UserID;
        //    model.Password = Login.Password;
        //    model.UserName = Login.UserName;

        //    model.Email = Login.Email;
        //    model.FirstName = Login.FirstName;
        //    model.LastName = Login.LastName;
        //    model.Mobile = Login.Mobile;
        //    if (Login == null)
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
            var LoginModel = IClientService.GetID(id.Value);
          
            if (LoginModel == null)
            {
                return HttpNotFound();
            }
            return View(LoginModel);
        }

        //[Authorize(Roles = "DeleteClient")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete1(int id)
        //{

        //    Login Login = IUserService.GetID(id);
        //    //  Login Login = db.Logins.Find(id);

        //    var userPermission = db.PermissionUser.Where(x => x.UserID == id);
        //    if (userPermission.Any())
        //    {
        //        db.PermissionUser.RemoveRange(userPermission);
        //        db.SaveChanges();
        //    }



        //    var ClientID = db.ProjectClients.Where(x => x.ClientID == id);
        //    if (ClientID.Any())
        //    {
        //        db.ProjectClients.RemoveRange(ClientID);
        //        db.SaveChanges();
        //    }

        //    var tt = db.Tickets.Where(x => x.ClientID == id);
        //    if (tt.Any())
        //    {
        //        db.Tickets.RemoveRange(tt);
        //        db.SaveChanges();
        //    }

        //    Login.IsDelete = true;

        //    IUserService.Update(Login);
        //    //    db.Logins.Remove(Login);
        //    //   db.SaveChanges();
        //    IClientService.Delete(id); 
        //    TempData["AlertMsg"] = "Deleted successfully";
        //    return RedirectToAction("Index");
        //}



        [Authorize(Roles = "DeleteClient")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            IClientService.Delete(id);
            TempData["AlertMsg"] = "Deleted successfully";
            return RedirectToAction("Index");
        }
        public string HashPass(string password)
        {

            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

            return encoded;//returns hashed version of password
        }
    }
}