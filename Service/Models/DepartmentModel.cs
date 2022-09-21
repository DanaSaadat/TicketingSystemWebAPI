using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Service.Models 
{
    public class DepartmentModel
    {
        //[Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Department Name")]
        public string Name { get; set; }
        public int UserCount { get; set; }
        public bool IsDepartmentNameExist { get; set; }  


        //[ForeignKey("Login")]
        public int? ManagerID { get; set; }
        public virtual ICollection<LoginModel> Login { get; set; }
    }
}