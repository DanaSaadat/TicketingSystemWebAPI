using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Models;


namespace Service
{
    public interface IPermissionService
    {
        IEnumerable<PermissionModel> GetALL();

        void Insert(PermissionModel Model); 

        //void Update(PermissionModel Model);

        //void Delete(int ID);

        //PermissionModel GetID(int ID);
    }
}
