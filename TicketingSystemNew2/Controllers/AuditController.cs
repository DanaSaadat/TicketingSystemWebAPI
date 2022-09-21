using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketingSystemNew2.Controllers
{
    public class AuditController : Controller
    {
        private IAuditService IAuditService;
        public AuditController(AuditService AuditService)
        {
            this.IAuditService = AuditService;
           
        }
        public ActionResult Index()
        {
            return View(IAuditService.GetALL());
        }
    }
}