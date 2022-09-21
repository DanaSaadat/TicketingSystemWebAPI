using Ninject;
using Service;
using Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace TicketingSystemNew2.CustomFilter
{
    public class AuditAttribute : ActionFilterAttribute , IExceptionFilter
    {
        string ErrorMesage; 
        public void OnException(ExceptionContext filterContext)
        {


            var object1 = filterContext.Controller.TempData["object"];
            var action1 = filterContext.Controller.TempData["action"];



            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];

            var model = filterContext.Controller.TempData["model"];
            var xx = filterContext.Controller.ViewBag;
            var uy = filterContext.Controller.ViewData;
            //var viewData = new ViewDataDictionary<HandleErrorInfo>(filterContext.Controller.ViewData);

            //viewData.Model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);





            string action = filterContext.RouteData.Values["action"].ToString();
            Exception e = filterContext.Exception;
            filterContext.ExceptionHandled = true;

            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.ExceptionHandled = true;
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        message = filterContext.Exception.Message,
                      
                        
                    },
                  JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {

                var result = new ViewResult()
                {
                    ViewName = action,
                    //ViewName = "Error"


                    ViewData = new ViewDataDictionary()
                    {

                        Model = model// set the model
                    }



                };
                result.ViewBag.MyErrorMessage = e.Message;

                filterContext.Result = result;
                //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "area", "" }, { "action", "Error" }, { "controller", "Error" } });

            }




            var exception = filterContext.Exception;



            //var view = new ViewResult();
            //view.ViewName = "Error";
            //view.ViewData = new ViewDataDictionary();
            //view.ViewData.Model = model;


            //view.ExecuteResult(filterContext);


            ErrorMesage = e.Message;
            var user = DependencyResolver.Current.GetService<IAuditService>();
            var request = filterContext.HttpContext.Request;
            AuditModel AuditModel = new AuditModel()
            {
                // Your Audit Identifier     
                AuditID = Guid.NewGuid(),
                UserName = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous",
                IPAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                AreaAccessed = request.RawUrl,
                //AreaAccessed = action1 == null ? request.RawUrl : action1.ToString(),

                Time = DateTime.UtcNow,

                Response = "Faild",
                //Response = object1 == null ? filterContext.HttpContext.Response.StatusDescription : object1.ToString(),


                Bug = ErrorMesage
                //Bug = null
            };
            user.Insert(AuditModel);




        }

        [Inject]

        private IAuditService _IAuditService;
       
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var request = filterContext.RequestContext.HttpContext.Request;
            //int idValue = System.Security.Authentication.SomeMethod();

            //filterContext.Controller.ViewData.Add/*("*/Id", idValue);

            var object1 = filterContext.Controller.TempData["object"];
            var action = filterContext.Controller.TempData["action"];
            var validation = filterContext.Controller.TempData["Validation"];

            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(SkipMyGlobalActionFilterAttribute), false).Any())
            {
                return;
            }

            else
            {

          

            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];

        


            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;
            var exciptionError = filterContext.HttpContext.Error;

            var user = DependencyResolver.Current.GetService<IAuditService>();
         


                AuditModel AuditModel = new AuditModel()
                {
                    AuditID = Guid.NewGuid(),
                    UserName = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous",
                    IPAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                    //AreaAccessed = request.RawUrl,
                    AreaAccessed = action == null ? request.RawUrl : action.ToString(),
                    Time = DateTime.UtcNow,
               
                    //Response = filterContext.HttpContext.Response.StatusDescription,
                    //Response = object1 == null? filterContext.HttpContext.Response.StatusDescription : object1.ToString(),
                    Response = validation != null ? "Failed" : filterContext.HttpContext.Response.StatusDescription,
                    ResponseObject = object1 == null ? filterContext.HttpContext.Response.StatusDescription : object1.ToString(),
                    Validation = validation == null ? null : validation.ToString(),
                    Bug = null
            };
            user.Insert(AuditModel);
          

           
            base.OnActionExecuting(filterContext);
        }
        }


    }


    //public class MyExceptionHandler : HandleErrorAttribute
    public class MyExceptionHandler : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {

            
            string action = filterContext.RouteData.Values["action"].ToString();
            Exception e = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error"
            };
            
        }
    }

    public class SkipMyGlobalActionFilterAttribute : Attribute
    {
    }
}