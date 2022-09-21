using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class ApprovalModel
    {
        //[Key]
        public int id { get; set; }

        [Required]
        [Display(Name = "Reject Reason")]
        public string RejectReason { get; set; }

        //[ForeignKey("Project")]
        public int? ProjectID { get; set; }
        //[Required]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; } 


        //[ForeignKey("Login")]
        public int? ManagerID { get; set; }

        //[ForeignKey("Status")]
        public int? statusID { get; set; } = 7;
        //public bool  test { get; set; }   
        public bool  CheckAllStatus { get; set; }    
        public virtual StatusModel Status { get; set; }

        public virtual ProjectModel Project { get; set; }
        public virtual LoginModel Login { get; set; } 
        public List<string> lstname { get; set; }
    }
}
