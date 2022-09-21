using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepositoryPermissionUser
    {
        IEnumerable<PermissionUser> GetALL();

        void Insert(PermissionUser PermissionUserEntity);

        void Update(PermissionUser PermissionUserEntity); 

        void Delete(int ID);
        void Delete(PermissionUser PermissionUserEntity); 
        void Delete(IEnumerable<PermissionUser>  PermissionUserEntity); 
         
        PermissionUser GetID(int ID);

    }
}
