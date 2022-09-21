using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Project
    {
       
        public int ID { get; set; }
       
        public string Name { get; set; }

        [ForeignKey("Status")]
        public int? statusID { get; set; } = 7;
        public virtual Status Status { get; set; }
        public virtual ICollection<ProjectEmp> ProjectEmp { get; set; }
        public virtual ICollection<ProjectClient> ProjectClient { get; set; }
        public virtual ICollection<Approval> Approval { get; set; }




    }
}
