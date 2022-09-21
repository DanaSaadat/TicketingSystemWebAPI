using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class PermissionUserModel
    {
        //[Key]
        public int id { get; set; }

        //[ForeignKey("Permission")]
        public int? PermissionID { get; set; }


        //[ForeignKey("Login")]
        public int? UserID { get; set; }


        public virtual PermissionModel Permission { get; set; }
        public virtual LoginModel Login { get; set; }
    }
}