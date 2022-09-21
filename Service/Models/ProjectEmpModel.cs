using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Service.Models
//namespace TicketingSystemNew2.Models
{
    public class ProjectEmpModel
    {
        //[Key]
        public int id { get; set; }


        //[ForeignKey("Project")]
        [Column(Order = 1)]
        public int? ProjectID { get; set; }


        //[ForeignKey("Login")]
        [Column(Order = 2)]
        public int? EmpID { get; set; }
        //public string EmpName { get; set; } /// <summary>
        /// /////
        /// </summary>



        public virtual ProjectModel Project { get; set; }
        public virtual LoginModel Login { get; set; }
    }
}