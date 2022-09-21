using Service;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;

namespace TicketingSystemWebApi.Controllers
{
    [RoutePrefix("api/Department")]

    public class DepartmentController : ApiController
    {

        private IDepartmentService IDepartmentService;
        public DepartmentController(DepartmentService IDepartmentService)
        {
            this.IDepartmentService = IDepartmentService;
          
        }

        [Route("GetDepartment")]

        public HttpResponseMessage Get()
        {
            try
            {
                //int Value = 0;
                //Value = Value / 0;

                var xx = IDepartmentService.GetALL();
                return Request.CreateResponse(HttpStatusCode.OK, new { isSuccess = true, Data = xx });

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { isSuccess = false, Error = ex.Message });
            }       
        }


        [Route("GetDepartmentJson")]

        public IHttpActionResult Getc()
        {
            try
            {
                int Value = 0;
                Value = Value / 0;

                var xx = IDepartmentService.GetALL();
                //return Json(xx);
                return Json(new { IsSuccess = true, Data = xx });
                //return StatusCode(HttpStatusCode.OK);

            }
            catch (Exception ex)

            {
                //return StatusCode(HttpStatusCode.BadRequest);
                return Json(new { IsSuccess = false, Error = ex.Message });

            }
        }

        [HttpPost]
        //[Route("api/Employees/Create")]
        [Route("CreateDepartment")] 
        public HttpResponseMessage CreateDepartment([FromBody] DepartmentModel DepartmentModel)
        {
            if (ModelState.IsValid)
            {
                DepartmentModel = IDepartmentService.Insert(DepartmentModel);
                if (DepartmentModel.IsDepartmentNameExist)
                {
                    //ViewData["msg"] = "Department Name Already Exists";
                    //return Json(new { IsSuccess = false, mesage = "Department Name Already Exists" });
                    return Request.CreateResponse(HttpStatusCode.OK, new { IsSuccess = false, mesage = "Department Name Already Exists" });
                }
                //return Json(new { IsSuccess = true, mesage = "danaaa" });
                return Request.CreateResponse(HttpStatusCode.OK, new { IsSuccess = true, mesage = "danaaa" });


            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, new { IsSuccess = false });
        }

        [Route("EditDepartment")]
        [HttpGet]
        public HttpResponseMessage EditDepartment(int? id)
        {
            try
            {
                var GetDepartment = IDepartmentService.GetID(id);
                return Request.CreateResponse(HttpStatusCode.OK, new { IsSuccess = true, data = GetDepartment });
            }
           
         

             catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { IsSuccess = false, Mesage = ex.Message });

                throw;
            }
        }




        [Route("UpdateDepartment")]
        [HttpPut]
        public HttpResponseMessage UpdateDepartment(DepartmentModel DepartmentModel)
        {
            try
            {
                DepartmentModel = IDepartmentService.Update(DepartmentModel);
                if (DepartmentModel.IsDepartmentNameExist)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { IsSuccess = false, mesage = "Department Name Already Exists" });

                }
                return Request.CreateResponse(HttpStatusCode.OK, new { IsSuccess = true, mesage = "danaaa" });
            }



            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { IsSuccess = false, Mesage = ex.Message });

                throw;
            }
        }




        [Route("deleteDepartment")]
        [HttpGet]
        public HttpResponseMessage deleteDepartment(int? id)
        {
            try
            {
                DepartmentModel Model = IDepartmentService.GetIDDelete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new { IsSuccess = true, data = Model });
            }



            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { IsSuccess = false, Mesage = ex.Message });

                throw;
            }
        }



        [Route("deleteDepartmentConfirm")]
        [HttpDelete]
        public HttpResponseMessage deleteDepartmentConfirm(int id)
        {
            try
            {
                IDepartmentService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new { IsSuccess = true });
            }



            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { IsSuccess = false, Mesage = ex.Message });

                throw;
            }
        }
    }
}
