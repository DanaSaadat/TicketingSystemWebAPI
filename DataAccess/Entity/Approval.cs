using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Approval
    {
        [Key]
        public int id { get; set; }


        public string RejectReason { get; set; }

        [ForeignKey("Project")]
        public int? ProjectID { get; set; }


        [ForeignKey("Login")]
        public int? ManagerID { get; set; }

        [ForeignKey("Status")]
        public int? statusID { get; set; } = 7;
        public virtual Status Status { get; set; }

        public virtual Project Project { get; set; }
        public virtual Login Login { get; set; }
    }
}
