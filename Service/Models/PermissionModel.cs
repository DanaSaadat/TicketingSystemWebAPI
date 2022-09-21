using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class PermissionModel
    {
        //[Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Permission Name")]
        public string Name { get; set; }

        public virtual ICollection<PermissionUserModel> PermissionUser { get; set; }
    }
}