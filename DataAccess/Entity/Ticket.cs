using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity
{
    public class Ticket
    {


        //public int id { get; set; }


        //public int ProjectID { get; set; }
        //public virtual Project Project { get; set; }

        //public string Description { get; set; }


        //public int statusID { get; set; } = 3;

        //public virtual Status Status { get; set; }




        //[ForeignKey("Login2")]
        //public int? ClientID { get; set; }
        //public virtual Login Login2 { get; set; }

        //[ForeignKey("Login")]
        //public int? AssignTo { get; set; }
        //public virtual Login Login { get; set; }
        [Key]
        public int id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [ForeignKey("Project")]
        public int? ProjectID { get; set; }



        [ForeignKey("Status")]
        public int statusID { get; set; } = 3;


        [ForeignKey("Login2")]
        public int? ClientID { get; set; }

        [ForeignKey("Login")]
        public int? AssignTo { get; set; }
        public virtual Login Login { get; set; }
        public virtual Login Login2 { get; set; }

        public virtual Status Status { get; set; }
        public virtual Project Project { get; set; }
    }
}
