using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity
{
    public class PermissionUser
    {
       
        public int id { get; set; }

        [ForeignKey("Permission")]
        //[Column(Order = 1)]
        public int? PermissionID { get; set; }


        [ForeignKey("Login")]
        //[Column(Order = 2)]
        public int? UserID { get; set; }


        public virtual Permission Permission { get; set; }
        public virtual Login Login { get; set; }
    }
}
