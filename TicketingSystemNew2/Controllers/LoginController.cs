using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Service.Models;
using Service;
using TicketingSystemNew2.CustomFilter;
using Newtonsoft.Json;

namespace TicketingSystemNew2.Controllers
{
    [AuditAttribute]

    public class LoginController : Controller
    {
        private ILoginService ILoginService; 

        public LoginController(LoginService LoginService)
        {
            this.ILoginService = LoginService;
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // i
            return RedirectToAction("Login");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(LoginModel objLogin)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        objLogin.Password = HashPass(objLogin.Password);
        //        var obj = db.Logins.Where(x => x.UserName.Equals(objLogin.UserName) && x.Password.Equals(objLogin.Password)).FirstOrDefault();




        //        var userRoles2 = (from user in db.Logins
        //                          join roleMapping in db.PermissionUser
        //                          on user.UserID equals roleMapping.UserID
        //                          join role in db.Permissions
        //                          on roleMapping.PermissionID equals role.ID
        //                          where user.UserName == objLogin.UserName
        //                          select role.ID).ToList(); // get permision id for user 


        //        var data = (from user in db.Logins
        //                    join Department in db.Departments
        //                    on user.DepartmentID equals Department.ID
        //                    //where user.UserID == objLogin.UserID
        //                    where user.UserName == objLogin.UserName
        //                    select Department.ManagerID).FirstOrDefault();

        //        if (obj != null && userRoles2 != null)
        //        {
        //            FormsAuthentication.SetAuthCookie(objLogin.UserName, false);
        //            Session["UserID"] = obj.UserID.ToString();
        //            Session["userRoles"] = userRoles2;
        //            Session["DepartmentID"] = obj.DepartmentID.ToString();
        //            Session["data"] = data;

        //            var NotificationCount = (from Approval in db.Approval
        //                                     where Approval.ManagerID == obj.UserID
        //                                     && Approval.statusID == 7
        //                                     select Approval).ToList().Count();

        //            TempData["AlertMsg"] = NotificationCount;
        //            return RedirectToAction("Loggedin");
        //        }


        //    }
        //    return View(objLogin);
        //}

        public static string Key = "dsfdf@@cdczsd@";
        public static string ConvertToEncrypt(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            password += Key;
            var passwordByBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordByBytes);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel Model)
        
        {
            int AttemptCount = 0;
            //int val = 0;
            //val = val / 0;

            var Password = AESEncrytDecry.DecryptStringAES(Model.Password);

            var UserName = AESEncrytDecry.DecryptStringAES(Model.UserName);




            //Model.Password = ConvertToEncrypt(Model.Password);

            var EncryptPassword = ConvertToEncrypt(Password); 
            var obj = ILoginService.GetALL().Where(x => x.UserName.Trim() == UserName.Trim() && x.Password == EncryptPassword).FirstOrDefault();
            //var obj = ILoginService.GetALL().Where(x => x.UserName.Trim() == Model.UserName.Trim() && x.Password == Model.Password).FirstOrDefault();
            //var obj = ILoginService.GetALL().Where(x => x.UserName == UserName && x.Password == Password).FirstOrDefault();

            if (ModelState.IsValid && obj != null)
            {
                Model.UserName = obj.UserName;
                Model.Password = EncryptPassword;
                Model = ILoginService.login(Model);

                //var obj = ILoginService.GetALL().Where(x => x.UserName.Trim()==Model.UserName.Trim() && x.Password==Model.Password).FirstOrDefault();
               
                if (obj != null  && Model.lstRole != null)
                {
                    FormsAuthentication.SetAuthCookie(Model.UserName, false);
                    Session["UserID"] = obj.UserID.ToString();
                    Session["userRoles"] = Model.lstRole;
                    Session["DepartmentID"] = obj.DepartmentID.ToString();
                    Session["data"] = Model.ManangerID;

                 
                    TempData["AlertMsg"] = Model.NotificationCount;
                    return Json( true,JsonRequestBehavior.AllowGet);
                    //     return RedirectToAction("Loggedin");
                }


            }

            if ( obj == null)
            {


                //Session["mloginAttempts"] = 0;

                //int mLoginAttempt = Convert.ToInt32(Session["mloginAttempts"]);


                //Session["mloginAttempts"] = Convert.ToInt32(Session["mloginAttempts"]) + 1;
                ////AttemptCount = AttemptCount + 1;
                //AttemptCount = Convert.ToInt32(Session["mloginAttempts"]);

               //if ( AttemptCount == 5)
               // {
               //     return Json(new { AttemptCount = AttemptCount, test = false });  
               // }
                TempData["Validation"] = "User Name Or Password Is Not Valid!";
                TempData["action"] = "/Login/Login";
                TempData["object"] = JsonConvert.SerializeObject(Model);
                //ViewData["msg"] = "User Name Or Password Is Not Valid! ";
                //ModelState.AddModelError("", "User Name Or Password Is Not Valid!");
                //throw new Exception("invalid Username or Password");
                return Json(false, JsonRequestBehavior.AllowGet);


            }


            //return View(Model); 
            return Json(Model,JsonRequestBehavior.AllowGet);


        }

        //public string HashPass(string password)
        //{

        //    byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
        //    byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
        //    string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

        //    return encoded;//returns hashed version of password
        //}
        public ActionResult Loggedin()
        {
            return View();

        }
    }
}