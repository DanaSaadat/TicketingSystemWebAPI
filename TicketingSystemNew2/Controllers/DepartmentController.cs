
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;

using System.Net.Http;
using System.Text;
//using System.Net.HttpClient;
//using System.Net.Http.Formatting;

using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TicketingSystemNew2.CustomFilter;


namespace TicketingSystemNew2.Controllers
{
    [AuditAttribute]
    //[MyExceptionHandler]
    public class DepartmentController : Controller
    {
        readonly string apiBaseAddress = ConfigurationManager.AppSettings["apiBaseAddress"];

        private IDepartmentService IDepartmentService;
        private IUserService IUserService;
        private IAuditService _IAuditService;

        public DepartmentController(DepartmentService IDepartmentService, UserService iUserService, AuditService iAuditService)
        {
            this.IDepartmentService = IDepartmentService;
            IUserService = iUserService;
            _IAuditService = iAuditService;
        }



        //[Authorize(Roles = "Department")]
        //public ActionResult Index()
        //{
        //    try
        //    {


        //        var xx = IDepartmentService.GetALL();
        //        return View(xx);

        //    }
        //    catch (Exception ex)
        //    {
        //        var xx = IDepartmentService.GetALL();



        //        TempData["model"] = xx;

        //        throw new Exception(ex.Message);
        //    }

        //}


        //[Authorize(Roles = "Department")]
        //public  ActionResult IndexAsync()
        //{

        //    IEnumerable<DepartmentModel> Departments = null;

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(apiBaseAddress);

        //        var responseTask = client.GetAsync("Department/GetDepartment");
        //        responseTask.Wait();

        //        var result = responseTask.Result;

        //        //var result =  client.GetAsync("Department/GetDepartment");

        //        if (result.IsSuccessStatusCode)
        //        {
        //            var xx = result.Content.ReadAsAsync<IList<DepartmentModel>>();
        //        }
        //        else
        //        {
        //            Departments = Enumerable.Empty<DepartmentModel>();
        //            ModelState.AddModelError(string.Empty, "Server error try after some time.");
        //        }
        //    }
        //}




        [Authorize(Roles = "Department")]

        //public async Task<ActionResult> Index()
        public async Task<ActionResult> Index()
        {
            IEnumerable<DepartmentModel> Departments = null;

            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(apiBaseAddress);

                client.BaseAddress = new Uri("https://localhost:44360/api/");

                //var responseTask = client.GetAsync("Department/GetDepartment").Result;
                var responseTask = await client.GetAsync("Department/GetDepartment");

                //var result = responseTask.Result;
                //if (result.IsSuccessStatusCode)
                if (responseTask.IsSuccessStatusCode)
                {
                    //var readTask = await result.Content.ReadAsAsync<IList<DepartmentModel>>();

                    //var readTask = await result.Content.ReadAsStringAsync();
                    var readTask= await responseTask.Content.ReadAsStringAsync();
                    JObject Jobject = JObject.Parse(readTask); //convert string to json /// Convert JSON String to JSON Object c#
                    if (Jobject["isSuccess"] != null)
                    {
                        if (Jobject["isSuccess"].ToObject<bool>() == true)
                        {
                            Departments = Jobject["Data"].ToObject<List<DepartmentModel>>();

                        }
                        else
                        {
                            var error = Jobject["Error"].ToObject<string>();
                            Departments = Enumerable.Empty<DepartmentModel>();

                            ModelState.AddModelError(string.Empty, error);

                        }
                    }

                    //var readTask = result.Content.ReadAsAsync<IList<DepartmentModel>>();

                    //readTask.Wait();

                    //Departments = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    Departments = Enumerable.Empty<DepartmentModel>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(Departments);
        }


        [Authorize(Roles = "AddDepartment")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DepartmentModel DepartmentModel)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    string json = JsonConvert.SerializeObject(DepartmentModel);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");


                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44360/api/");

                        //var response = await client.PostAsJsonAsync("CreateDepartment", DepartmentModel);
                        var response = await client.PostAsync("Department/CreateDepartment", content);

                       
                        var readTask = await response.Content.ReadAsStringAsync(); // here you can read content as string

                        JObject Jobject = JObject.Parse(readTask); // Convert JSON String to JSON Object c#

                        if (Jobject["IsSuccess"] != null)
                        {
                            if (Jobject["IsSuccess"].ToObject<bool>() == true)
                            {
                                var xx=  Jobject["mesage"].ToObject<string>();
                                TempData["AlertMsg"] = "Saved Successfully"; 
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                var error = Jobject["mesage"].ToObject<string>();
                                //DepartmentModel = Enumerable.Empty<DepartmentModel>().FirstOrDefault();

                                ModelState.AddModelError(string.Empty, error);
                                //ModelState.Clear();
                                return View(DepartmentModel);

                            }
                        }


                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error try after some time.");
                        }
                    }

                }

                return View(DepartmentModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        //[Authorize]
        //public ActionResult Edit(int? id)
        //{
        //    try
        //    {


        //        ViewBag.ManagerID = new SelectList(IUserService.GetALLbyDepartment(id.Value).ToList(), "UserID", "UserName");

        //        var GetDepartment = IDepartmentService.GetID(id);

        //        return View(GetDepartment);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        [Authorize]
        public async Task<ActionResult> Edit(int? id) 
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DepartmentModel DepartmentModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44360/api/");

                //var result = await client.GetAsync("Department/EditDepartment/{id}");
                var result = await client.GetAsync("Department/EditDepartment?id=" + id);
                //var responseTask = client.GetAsync("student?id=" + id.ToString());

                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    JObject Jobject = JObject.Parse(readTask);

                    if (Jobject["IsSuccess"] != null)
                    {
                        if (Jobject["IsSuccess"].ToObject<bool>() == true)
                        {
                            DepartmentModel = Jobject["data"].ToObject<DepartmentModel>();

                        }
                        else
                        {
                            var error = Jobject["Error"].ToObject<string>();
                            DepartmentModel = Enumerable.Empty<DepartmentModel>().FirstOrDefault();

                            ModelState.AddModelError(string.Empty, error);

                        }
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            if (DepartmentModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManagerID = new SelectList(IUserService.GetALLbyDepartment(id.Value).ToList(), "UserID", "UserName");

            return View(DepartmentModel);
        }



        [Authorize(Roles = "UpdateDepartment")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>  Edit(DepartmentModel DepartmentModel)
        {
            try
            {
                string json = JsonConvert.SerializeObject(DepartmentModel);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                if (ModelState.IsValid)
                {

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44360/api/");
                        var response = await client.PutAsync("Department/UpdateDepartment", content);
                        if (response.IsSuccessStatusCode)
                        {

                            var readTask = await response.Content.ReadAsStringAsync();
                            JObject Jobject = JObject.Parse(readTask);

                            if (Jobject["IsSuccess"] != null)
                            {
                                if (Jobject["IsSuccess"].ToObject<bool>() == true)
                                {
                                    var xx = Jobject["mesage"].ToObject<string>();
                                    TempData["AlertMsg"] = "Updated Successfully";
                                    return RedirectToAction("Index");
                                }
                                else
                                {
                                    var error = Jobject["mesage"].ToObject<string>();

                                    ModelState.AddModelError(string.Empty, error);
                                     ViewBag.ManagerID = new SelectList(IUserService.GetALLbyDepartment(DepartmentModel.ID).ToList(), "UserID", "UserName");

                                    return View(DepartmentModel);

                                }
                            }





                            //return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error try after some time.");
                        }
                    }
                    return RedirectToAction("Index");
                 
                }
                return View(DepartmentModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }





        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                DepartmentModel DepartmentModel = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44360/api/");

                    var result = await client.GetAsync("Department/deleteDepartment?id=" + id);

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = await result.Content.ReadAsStringAsync();//read result from api
                        JObject Jobject = JObject.Parse(readTask);//convert json string to  json object 

                        if (Jobject["IsSuccess"] != null)
                        {
                            if (Jobject["IsSuccess"].ToObject<bool>() == true)
                            {
                                DepartmentModel = Jobject["data"].ToObject<DepartmentModel>();
                                if (DepartmentModel.UserCount != 0)
                                {
                                    TempData["AlertMsg2"] = "This Department Contains Users ";

                                }
                            }
                            else
                            {
                                var error = Jobject["Mesage"].ToObject<string>();
                                DepartmentModel = Enumerable.Empty<DepartmentModel>().FirstOrDefault();

                                ModelState.AddModelError(string.Empty, error);

                            }
                        }

                    }

                    //DepartmentModel Model = IDepartmentService.GetIDDelete(id);


                    //if (Model.UserCount != 0)
                    //{
                    //    TempData["AlertMsg2"] = "This Department Contains Users ";

                    //}

                    if (DepartmentModel == null)
                    {
                        return HttpNotFound();
                    }
                    return View(DepartmentModel);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        [Authorize(Roles = "DeleteDepartment")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>  Delete(int id)
        {
            try
            {


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44360/api/");

                    var response = await client.DeleteAsync("Department/deleteDepartmentConfirm?id=" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["AlertMsg"] = "Deleted successfully";

                        return RedirectToAction("Index");
                    }
                    else
                        ModelState.AddModelError(string.Empty, "Server error ...try after some time.");
                }
                return View();

                //IDepartmentService.Delete(id);

                //TempData["AlertMsg"] = "Deleted successfully";
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }






        [Authorize(Roles = "Department")]
        public ActionResult Index1(string sortOrder)
        {
            try
            {
                //if ( Session["xyz"] != null )
                //{
                //    ViewBag.SortName = Convert.ToString(Session["xyz"]);

                //}

                //int value = 0;

                //value /= value;
                ViewBag.TotalDepartment = IDepartmentService.GetALL().Count();
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }





        public JsonResult List(string sortOrder)
        {
            var Departments = IDepartmentService.GetALL();

            //ViewBag.SortName = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            //Session["xyz"] = String.IsNullOrEmpty(sortOrder) ? "Name" : "";

            //switch (sortOrder)
            //{
            //    case "Name":
            //        Departments = Departments.OrderByDescending(s => s.Name);
            //        break;

              

            //    default:
            //        Departments = Departments.OrderBy(s => s.Name);
            //        break;
            //}





            //return Json(IDepartmentService.GetALL(), JsonRequestBehavior.AllowGet);

            var sortOrderName= String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            //return Json(Departments, JsonRequestBehavior.AllowGet);
            return Json( new { cc = Departments ,xx = sortOrderName }, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "AddDepartment")]
        [HttpPost]
        public JsonResult Add1(DepartmentModel DepartmentModel)
        {

            try
            {
                //{
                //    int val = 0;
                //    val = val / 0;


                if (DepartmentModel.Name == "" || DepartmentModel.Name == null)
                {
                    AuditModel AuditModel = new AuditModel()
                    {
                        // Your Audit Identifier     
                        AuditID = Guid.NewGuid(),
                        UserName = "Admin",
                        IPAddress = Request.UserHostAddress,
                        AreaAccessed = Request.Url.ToString(),
                        Time = DateTime.UtcNow,

                        Response = "Faild",


                        Bug = "username is required"
                    };
                    _IAuditService.Insert(AuditModel);
                }
                else
                {
                    AuditModel AuditModel = new AuditModel()
                    {
                        // Your Audit Identifier     
                        AuditID = Guid.NewGuid(),
                        UserName = "Admin",
                        IPAddress = Request.UserHostAddress,
                        AreaAccessed = Request.Url.ToString(),
                        Time = DateTime.UtcNow,

                        Response = "ok",


                        Bug = null
                    };
                    _IAuditService.Insert(AuditModel);
                    DepartmentModel.ManagerID = 0;
                    var response = IDepartmentService.Insert(DepartmentModel);
                    if (response.IsDepartmentNameExist)
                    {

                        throw new Exception("Department Name Already Exists");
                        //throw new Exception("");
                        return Json(new { Error = "Department Name already  Exists", IsSuccess = false });
                    }
                }
                //int value = 0;

                //value /= value;

                //     DepartmentModel.ManagerID = 0;
                //var   response = IDepartmentService.Insert(DepartmentModel);
                // if (response.IsDepartmentNameExist)
                // {

                //         throw new Exception("Department Name Already Exists");
                //         //throw new Exception("");
                //         return Json(new { Error = "Department Name already  Exists", IsSuccess = false });
                //     }
                return Json(new { IsSuccess = true });
            }
            catch (Exception ex)
            {

                throw ex;
                //throw new Exception(ex.Message);
            }
        }










        //[AuditAttribute]
        //[SkipMyGlobalActionFilter]

        [Authorize(Roles = "AddDepartment")]
        [HttpPost]
        public JsonResult Add(DepartmentModel DepartmentModel)
        {

            try
            {
                //{
                //int val = 0;
                //val = val / 0;



                TempData["object"] = JsonConvert.SerializeObject(DepartmentModel);
                TempData["action"] = "/Department/Add";

                DepartmentModel.ManagerID = 0;
                var response = IDepartmentService.Insert(DepartmentModel);
                if (response.IsDepartmentNameExist)
                {
                    TempData["Validation"] = "Department Name Already Exists";

                    TempData["object"] = JsonConvert.SerializeObject(DepartmentModel);
                    TempData["action"] = "/Department/Add";

                    //throw new Exception("Department Name Already Exists");
                    return Json(new { Error = "Department Name already  Exists", IsSuccess = false });
                }

                //else
                //{
                //    TempData["object"] = JsonConvert.SerializeObject(DepartmentModel);
                //    //var dept = JsonConvert.DeserializeObject<DepartmentModel>(JsonConvert.SerializeObject(DepartmentModel));
                //    TempData["action"] = "/Department/Add";
                //}
                return Json(new { IsSuccess = true });

            }


            catch (Exception ex)
            {
                //TempData["object"] = "Department Name Already Exists";
                //TempData["action"] = "/Department/Add";
                throw ex;
                //throw new Exception(ex.Message);
            }
        }


        public JsonResult checkDepartmentName(DepartmentModel DepartmentModel)
        {
            DepartmentModel = IDepartmentService.Insert(DepartmentModel);

            return Json(DepartmentModel.IsDepartmentNameExist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetbyID(int ID)
        {

            //ViewBag.ManagerID = new SelectList(IUserService.GetALLbyDepartment(ID).ToList(), "UserID", "UserName");
            return Json(IDepartmentService.GetID(ID), JsonRequestBehavior.AllowGet);
        }

        public ActionResult getManager(int ID)
        {
            return Json(IUserService.GetALLbyDepartment(ID).ToList().Select(x => new
            {
                UserID = x.UserID,
                UserName = x.UserName
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "UpdateDepartment")]
        [HttpPost]
        public JsonResult Update(DepartmentModel DepartmentModel)
        {


            IDepartmentService.Update(DepartmentModel);
            return Json(JsonRequestBehavior.AllowGet);
        }



        public JsonResult Delete2(int ID)
        {
            try
            {
                //int vall = 0;
                //vall = vall / 0;

                //Student userEntity = IStudentSevice.GetID(ID);
                IDepartmentService.Delete(ID);
                return Json(JsonRequestBehavior.AllowGet);

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }


        [Authorize(Roles = "AddDepartment")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1(DepartmentModel DepartmentModel)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    //{
               

                    DepartmentModel = IDepartmentService.Insert(DepartmentModel);
                    if (DepartmentModel.IsDepartmentNameExist)
                    {
                        //ViewData["msg"] = "Department Name Already Exists";
                        //ModelState.AddModelError("", "Department Name Already Exists");
                        throw new Exception("Department Name Already Exists");
                        // return View();
                    }

                    TempData["AlertMsg"] = "Saved Successfully";

                    return RedirectToAction("Index");
                }

                return View(DepartmentModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




       

        //[Authorize]
        //public ActionResult Edit(int? id)
        //{
        //    try
        //    {


        //        ViewBag.ManagerID = new SelectList(IUserService.GetALLbyDepartment(id.Value).ToList(), "UserID", "UserName");

        //        var GetDepartment = IDepartmentService.GetID(id);

        //        return View(GetDepartment);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}










        //[Authorize(Roles = "UpdateDepartment")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(DepartmentModel DepartmentModel)
        //{
        //    try
        //    {


        //        if (ModelState.IsValid)
        //        {
        //            DepartmentModel = IDepartmentService.Update(DepartmentModel);
        //            if (DepartmentModel.IsDepartmentNameExist)
        //            {
        //                ViewData["msg"] = "Department Name already exists";
        //                ViewBag.ManagerID = new SelectList(IUserService.GetALLbyDepartment(DepartmentModel.ID).ToList(), "UserID", "UserName");

        //                return View();
        //            }
        //            TempData["AlertMsg"] = "Updated Successfully";
        //            return RedirectToAction("Index");
        //        }
        //        return View(DepartmentModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        [Authorize]
        public ActionResult Delete1(int? id)
        {
            try
            {


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                DepartmentModel Model = IDepartmentService.GetIDDelete(id);


                if (Model.UserCount != 0)
                {
                    TempData["AlertMsg2"] = "This Department Contains Users ";

                }

                if (Model == null)
                {
                    return HttpNotFound();
                }
                return View(Model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }






        [Authorize(Roles = "DeleteDepartment")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete1(int id)
        {
            try
            {
               

                IDepartmentService.Delete(id);

                TempData["AlertMsg"] = "Deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}