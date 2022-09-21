
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
using PagedList;
using PagedList.Mvc;
using TicketingSystemNew2.CustomFilter;
using Newtonsoft.Json;

namespace TicketingSystemNew2.Controllers
{
    [AuditAttribute]
    public class UserController : Controller
    {
        private IUserService IUserService;
        private IDepartmentService IDepartmentService;
        private IPermissionUserService IPermissionUserService;
        private IProjectEmpService IProjectEmpService;
        private ITicketService ITicketService;
        private IProjectClientService IProjectClientService;
        private IPermissionService IPermissionService;

        public UserController(UserService IUserService, PermissionUserService IUserService1, ProjectEmpService IUserService2,TicketService iTicketService, ProjectClientService iProjectClientService, DepartmentService iDepartmentService, PermissionService iPermissionService)
        {
            this.IUserService = IUserService;
            this.IPermissionUserService = IUserService1;
            this.IProjectEmpService = IUserService2;
            ITicketService = iTicketService;
            IProjectClientService = iProjectClientService;
            IDepartmentService = iDepartmentService;
            IPermissionService = iPermissionService;
        }


        public IQueryable<LoginModel> QueryableUser() 
        {
            return IUserService.GetALLIQueryable().AsQueryable();
            //return IUserService.GetALL().AsQueryable();

        }
        [Authorize(Roles = "User")]
        public ActionResult Index(int pageNumber = 1)
        {
           var a = QueryableUser();
            ViewBag.totalPages = Math.Ceiling(QueryableUser().Count() / 10.0);
            //const int pageSize = 10;

            var Queryable = QueryableUser();

            var users  = Queryable.Skip((pageNumber - 1) * 10).Take(10).ToList(); 


            return View(users);

            //ViewBag.totalPages = Math.Ceiling(IUserService.GetALL().Count() / 10.0);
            //var users = IUserService.GetALLPaged(pageNumber, 10).ToList();
            //return View(users);


          
            //var users = IUserService.GetALL();
            //users = IUserService.GetALL().Skip((pageNumber - 1) * 10).Take(10).ToList();

            //var users = IUserService.GetALLPaged(pageNumber, 10).ToList();

            //return View(users2);




        }

        //[Authorize(Roles = "User")]
        //public ActionResult Index(int? page)
        //{

        //    int pageSize = 5;
        //    int pageNumber = (page == null ? pageNumber = 1 : pageNumber = (int)page + 1);

        //    var users = IUserService.GetALL();
        //    return View(IUserService.GetALLPaged(pageNumber, pageSize).ToPagedList(pageNumber, pageSize));





        //}
        public ActionResult Indexpage(int? page)
        {

            int pageSize = 5;
            //   int pageNumber = (page ?? 1);
            int pageNumber = (page == null ? pageNumber = 1 : pageNumber = (int)page + 1);

            IUserService.GetALLPaged(pageNumber, pageSize).ToPagedList(pageNumber, pageSize);
            //     return Json(pageNumber,JsonRequestBehavior.AllowGet);
            //return View("Index", IUserService.GetALLPaged(pageNumber, pageSize).ToPagedList(pageNumber, pageSize));
            return RedirectToAction("Index", new { page = pageNumber });
            //return View(IUserService.GetALLPaged(pageNumber, pageSize).ToPagedList(pageNumber, pageSize));



        }


        [Authorize]
        public ActionResult Create()
        {

            ViewBag.DepartmentID = new SelectList(IDepartmentService.GetALL(), "ID", "Name");
            ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
            return View();
        }





        //[Authorize(Roles = "AddUser")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(LoginModel model, int[] Permission)
        //{
        //    model = IUserService.Insert2(model, Permission);

        //    if (model.IsUserNameExist == true)
        //    {
        //        ViewData["msg"] = "User name already exists";
        //        ViewBag.DepartmentID = new SelectList(IDepartmentService.GetALL(), "ID", "Name");
        //        ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
        //        return View();

        //    }

        //    if (ModelState.IsValid)
        //    {
        //        if (!model.IsUserNameExist)
        //        {

                 
        //            IUserService.Insert(model,Permission); 

        //            TempData["AlertMsg"] = "Saved successfully";
        //            if (Permission == null)
        //            {
        //                return RedirectToAction("Index");
        //            }
        //        }
        //    }
        //    if (Permission != null)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.DepartmentID = new SelectList(IDepartmentService.GetALL(), "ID", "Name");
        //    ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
        //    return View(model);
        //}




        [Authorize(Roles = "AddUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoginModel model, int[] Permission)
        {
            if (ModelState.IsValid)
            {




                TempData["object"] = JsonConvert.SerializeObject(model);
                TempData["action"] = "/User/Create";
                model = IUserService.Insert2(model, Permission);

            if (model.IsUserNameExist == true)
            {
                ViewData["msg"] = "User name already exists";
                ViewBag.DepartmentID = new SelectList(IDepartmentService.GetALL(), "ID", "Name");
                ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
                return View();

            }

           
                if (!model.IsUserNameExist)
                {


                    IUserService.Insert(model, Permission);

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

            ViewBag.DepartmentID = new SelectList(IDepartmentService.GetALL(), "ID", "Name");
            ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
            return View(model);
        }
        public string HashPass(string password)
        {

            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

            return encoded;//returns hashed version of password
        }

        [Authorize]

        public ActionResult Edit(int? id)
        {

            ViewBag.DepartmentID = new SelectList(IDepartmentService.GetALL(), "ID", "Name");

            ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
            ViewBag.PermissionUser = IPermissionUserService.GetALL().Where(x => x.UserID == id).Select(x => x.Permission.ID).ToArray();
            LoginModel Model = IUserService.GetID(id.Value);


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Model == null)
            {
                return HttpNotFound();
            }
            return View(IUserService.GetID(id.Value));
        }

        [Authorize(Roles = "UpdateUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LoginModel model, int[] Permission)
        {
            if (ModelState.IsValid)
            {
              
            model = IUserService.Update(model, Permission);
                if (model.IsUserNameExist)
                {
                    ViewData["msg"] = "User name already exists";
                    ViewBag.DepartmentID = new SelectList(IDepartmentService.GetALL(), "ID", "Name");
                    ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
                    ViewBag.PermissionUser = IPermissionUserService.GetALL().Where(x => x.UserID == model.UserID).Select(x => x.Permission.ID).ToArray();

                    return View();

                }
                TempData["AlertMsg"] = "Updated successfully";
                return RedirectToAction("Index");
              
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var LoginModel = IUserService.GetID(id.Value);
          
            if (LoginModel == null)
            {
                return HttpNotFound();
            }
            return View(LoginModel); 
        }



        [Authorize(Roles = "DeleteUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
           
            IUserService.Delete(id);
            TempData["AlertMsg"] = "Deleted successfully";
            return RedirectToAction("Index");
        }



        // ado.net 
        [Authorize(Roles = "User")]
        public ActionResult Index1()
        {



            return View(IUserService.GetALL1());
        }



        [Authorize]
        public ActionResult Create1()
        {
             
            ViewBag.DepartmentID = new SelectList(IDepartmentService.GetALL(), "ID", "Name");
            ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
            return View();
        }


        [Authorize(Roles = "AddUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1(LoginModel model, int[] Permission) 
        {
            if (ModelState.IsValid)
            {
                model = IUserService.Insert2(model, Permission);

                if (model.IsUserNameExist == true)
                {
                    ViewData["msg"] = "User name already exists";
                    ViewBag.DepartmentID = new SelectList(IDepartmentService.GetALL(), "ID", "Name");
                    ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
                    return View();

                }


                if (!model.IsUserNameExist)
                {

                     
                    //IUserService.Insert(model, Permission);
                    IUserService.InsertAdo(model, Permission);

                    TempData["AlertMsg"] = "Saved successfully";
                    if (Permission == null)
                    {
                        return RedirectToAction("Index1");
                    }
                }
            }
            if (Permission != null)
            {
                return RedirectToAction("Index1");
            }

            ViewBag.DepartmentID = new SelectList(IDepartmentService.GetALL(), "ID", "Name");
            ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
            return View(model);
        }

        [Authorize]

        public ActionResult Edit1(int? id)
        {

            ViewBag.DepartmentID = new SelectList(IDepartmentService.GetALL(), "ID", "Name");

            ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
            ViewBag.PermissionUser = IPermissionUserService.GetALL().Where(x => x.UserID == id).Select(x => x.Permission.ID).ToArray();
          
            //LoginModel Model = IUserService.GetID(id.Value);
            LoginModel Model = IUserService.GetID1(id.Value);


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Model == null)
            {
                return HttpNotFound();
            }
            return View(IUserService.GetID1(id.Value));
        }


        [Authorize(Roles = "UpdateUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1(LoginModel model, int[] Permission)
        {
            if (ModelState.IsValid)
            {

                //model = IUserService.Update(model, Permission);
                model = IUserService.Update1(model, Permission);
                if (model.IsUserNameExist)
                {
                    ViewData["msg"] = "User name already exists";
                    ViewBag.DepartmentID = new SelectList(IDepartmentService.GetALL(), "ID", "Name");
                    ViewBag.Permission = new SelectList(IPermissionService.GetALL(), "ID", "Name");
                    ViewBag.PermissionUser = IPermissionUserService.GetALL().Where(x => x.UserID == model.UserID).Select(x => x.Permission.ID).ToArray();

                    return View();

                }
                TempData["AlertMsg"] = "Updated successfully";
                return RedirectToAction("Index1");

            }
            return View(model);
        }
        [Authorize]
        public ActionResult Delete1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //      var LoginModel = IUserService.GetID(id.Value);
            var LoginModel = IUserService.GetID1(id.Value);

            if (LoginModel == null)
            {
                return HttpNotFound();
            }
            return View(LoginModel);
        }


        [Authorize(Roles = "DeleteUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete1(int id)
        {

            IUserService.Delete1(id);
            TempData["AlertMsg"] = "Deleted successfully";
            return RedirectToAction("Index1");
        }

    }
}