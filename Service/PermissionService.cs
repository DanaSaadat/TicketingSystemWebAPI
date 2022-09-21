using DataAccess;
using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Models;


namespace Service
{
    public class PermissionService : IPermissionService
    {
        private IRepository<Permission> IRepository;

        public PermissionService(Repository<Permission> userRepository)
        {

            this.IRepository = userRepository;
        }

        //public void Delete(int ID)
        //{
        //    IRepository.Delete(ID);
        //}

        public IEnumerable<PermissionModel> GetALL()
        {

            IEnumerable<PermissionModel> Permissions = IRepository.GetALL().Select(u => new PermissionModel
            {

                ID = u.ID,
                Name = u.Name,
            });
            return Permissions;
        }

      

        public void Insert(PermissionModel Model)
        {
            Permission obj = new Permission();
            obj.ID = Model.ID;

            obj.Name = Model.Name;
            IRepository.Insert(obj);
        }

      
    }
}
