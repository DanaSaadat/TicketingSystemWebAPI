using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

//namespace TicketingSystemNew2.Models
namespace Service.Models 
{
    public class ProjectClientModel
    {

        //[Key]
        public int id { get; set; }


        //[ForeignKey("Project")]
        //[Column(Order = 1)]
        public int? ProjectID { get; set; }


        //[ForeignKey("Login")]
        //[Column(Order = 2)]
        public int? ClientID { get; set; }
        //public string ClientName { get; set; }   


        public virtual ProjectModel Project { get; set; }
        public virtual LoginModel Login { get; set; }
    }
}