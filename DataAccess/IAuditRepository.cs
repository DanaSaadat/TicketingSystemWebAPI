using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IAuditRepository
    {
        void Insert(Audit audit);
        IEnumerable<Audit> GetALL();

    }


}
