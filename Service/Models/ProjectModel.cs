using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class ProjectModel
    {

        //[Key]
        public int ID { get; set; }
        public int IDtest { get; set; } 
        [Required]
        [Display(Name = "Project Name")]
        public string Name { get; set; }
        public string mesage { get; set; }
        public int?  ManagerID { get; set; }  
        public virtual ICollection<ProjectEmpModel> ProjectEmp { get; set; }
        public virtual ICollection<ProjectClientModel> ProjectClient { get; set; }


     //   public IEnumerable<SelectListItem> Values { get; set; }
        //  public List<string> SelectedValues { get; set; }
        public List<string> SelectedValues { get; set; }
    }
}