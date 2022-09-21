using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity
{
    public class ProjectEmp
    {
        
        public int id { get; set; }


        [ForeignKey("Project")]
        [Column(Order = 1)]
        public int? ProjectID { get; set; }


        [ForeignKey("Login")]
        [Column(Order = 2)]
        public int? EmpID { get; set; }
     


        public virtual Project Project { get; set; }
        public virtual Login Login { get; set; }
    }
}
