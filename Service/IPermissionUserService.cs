using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Models;


namespace Service
{
    public interface IPermissionUserService
    {
        IEnumerable<PermissionUserModel> GetALL();

        //void Insert(PermissionUserModel entity);

        //void Update(PermissionUserModel entity);

        void Delete(int ID);
        //void Delete(PermissionUserModel user);
        //void Delete(IEnumerable<PermissionUserModel> user);


        //PermissionUserModel GetID(int ID);
    }
}
