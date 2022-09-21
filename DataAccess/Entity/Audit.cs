using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class Audit
    {
        public Guid AuditID { get; set; }
        public string UserName { get; set; }
        public string IPAddress { get; set; }
        public string AreaAccessed { get; set; }
        public string Bug { get; set; } 
        public string Response { get; set; }  
        public DateTime Time { get; set; }

        public string Validation { get; set; } 

        public string ResponseObject { get; set; }

    }
}
