using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity
{
    public class Login
    {

        [Key]
        public int UserID { get; set; }

      
        public string UserName { get; set; }

       
        public string Password { get; set; }

        
        public string FirstName { get; set; }

       
        public string LastName { get; set; } 

      
       
        public string Email { get; set; }
        public string Mobile { get; set; }

        public bool IsClient { get; set; }
        public bool IsDelete { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<ProjectEmp> ProjectEmp { get; set; }
        public virtual ICollection<ProjectClient> ProjectClient { get; set; }
        public virtual ICollection<PermissionUser> PermissionUser { get; set; }
    }
}
