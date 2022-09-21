using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

//namespace TicketingSystemNew2.Models
namespace Service.Models
{
    public class LoginModel
    {
        //[Key]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

     
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        public string Mobile { get; set; }

        public bool IsClient { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUserNameExist { get; set; }

        public bool IsUserNameaAndPassword { get; set; } 
         
        public List<int> lstRole { get; set; }
        public int? ManangerID { get; set; } 
        public int? NotificationCount { get; set; }  


        //[ForeignKey("Department")]
        public int? DepartmentID { get; set; }
        public string  DepartmentName { get; set; } 
        public string[] userRoles { get; set; }  

        public virtual DepartmentModel Department { get; set; }

        public virtual ICollection<ProjectEmpModel> ProjectEmp { get; set; }
        public virtual ICollection<ProjectClientModel> ProjectClient { get; set; }
        public virtual ICollection<PermissionUserModel> PermissionUser { get; set; }
    }
}