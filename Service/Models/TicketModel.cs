using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Service.Models 
{
    public class TicketModel
    {

        //[Key]
        public int id { get; set; }

        //[ForeignKey("Project")]
        [Required(ErrorMessage = "The Project field is required.")]
        [Display(Name ="Project")]

        public int? ProjectID { get; set; }
        public string ProjectName { get; set; } 

        public virtual ProjectModel Project { get; set; }
        [Required]
        public string Description { get; set; }

        //[ForeignKey("Status")]
        public int statusID { get; set; } = 3;
        public string statusName { get; set; }  

        public virtual StatusModel Status { get; set; }




        //[ForeignKey("Login2")]
        public int? ClientID { get; set; }
        public virtual LoginModel Login2 { get; set; }

        //[ForeignKey("Login")]
        public int? AssignTo { get; set; }
        public virtual LoginModel Login { get; set; }
    }
}