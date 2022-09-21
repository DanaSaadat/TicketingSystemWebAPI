
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Models;
using TicketingSystemNew2.CustomFilter;

namespace TicketingSystemNew2.Controllers
{
    [AuditAttribute]
    public class PermissionController : Controller
    {
        private IPermissionService IPermissionService;

        public PermissionController(PermissionService IPermissionService)
        {
            this.IPermissionService = IPermissionService;
        }
        // GET: Permission
        public ActionResult Index()
        {
           
            return View(IPermissionService.GetALL());

        }


        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PermissionModel Model)
        {
            if (ModelState.IsValid)
            {
               
                IPermissionService.Insert(Model);
            
                return RedirectToAction("Index");
            }

            return View(Model);
        }


    }
}