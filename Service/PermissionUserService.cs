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
    public class PermissionUserService : IPermissionUserService
    {

        private IRepository<PermissionUser> IRepository;
        private IRepositoryPermissionUser IRepositoryPermissionUser;

        public PermissionUserService(Repository<PermissionUser> userRepository, RepositoryPermissionUser iRepositoryPermissionUser)
        {

            this.IRepository = userRepository;
            IRepositoryPermissionUser = iRepositoryPermissionUser; 
        }






        public void Delete(int ID)
        {
            IRepository.Delete(ID);
        }

        public IEnumerable<PermissionUserModel> GetALL()
        {
            //IEnumerable<PermissionUserModel> PermissionUsers = IRepository.GetALL().Select(u => new PermissionUserModel
            var PeaarmissionUsers = IRepositoryPermissionUser.GetALL();


            IEnumerable<PermissionUserModel> PermissionUsers = PeaarmissionUsers.Select(u => new PermissionUserModel
            {

                id = u.id,
                PermissionID = u.PermissionID,
                UserID = u.UserID,
             
               Permission = new PermissionModel()
               {
                   ID = u.Permission.ID,
                   Name = u.Permission.Name
               }

            });

            return PermissionUsers; 
        }

        //public PermissionUser GetID(int ID)
        //{
        //    return IRepository.GetID(ID);
        //}

        //public void Insert(PermissionUserModel Model)
        //{
        //    IRepository.Insert(Model);
        //}


        //public void Delete(PermissionUserModel Model)
        //{
        //    IRepository.Delete(Model);
        //}

        //public void Delete(IEnumerable<PermissionUserModel> Model)
        //{
        //    IRepository.Delete(Model);
        //}
        //public void Update(PermissionUserModel Model)
        //{
        //    IRepository.Update(Model);
        //}
    }
}
