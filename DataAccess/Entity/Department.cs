using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataAccess.Entity
{
    public class Department
    {
        [Key]
        public int ID { get; set; }
      
        public string Name { get; set; }

        [ForeignKey("Login")]
        public int? ManagerID { get; set; }
        public virtual ICollection<Login> Login { get; set; }
    }
}
