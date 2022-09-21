using DataAccess;
using DataAccess.Entity;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ClientService :  IClientService
    {
        private IRepository<Login> IRepository;
        private IRepository<PermissionUser> IRepositoryPermissionUser;
        private IRepository<ProjectEmp> IRepositoryProjectEmp;
        private IRepository<ProjectClient> IRepositoryProjectClient;
        private IRepository<Ticket> IRepositoryTicket;
        private IRepositoryUser IRepositoryUser;

        public ClientService(Repository<Login> userRepository, Repository<PermissionUser> iRepositoryPermissionUser, Repository<ProjectEmp> iRepositoryProjectEmp, Repository<ProjectClient> RepositoryProjectClient, Repository<Ticket> iRepositoryTicket, RepositoryUser iRepositoryUser)
        {

            this.IRepository = userRepository;
            IRepositoryPermissionUser = iRepositoryPermissionUser;
            IRepositoryProjectEmp = iRepositoryProjectEmp;
            IRepositoryProjectClient = RepositoryProjectClient;
            IRepositoryTicket = iRepositoryTicket;
            IRepositoryUser = iRepositoryUser;
        }


        public IEnumerable<LoginModel> GetALL()
        {


            IEnumerable<LoginModel> LstUser = IRepository.GetALL().Where(x => x.IsClient.Equals(true)).Where(x => x.IsDelete.Equals(false)).Select(u => new LoginModel
            {

                UserID = u.UserID,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Password = u.Password,

                Email = u.Email,
                Mobile = u.Mobile,



            });
            return LstUser;
        }


        public LoginModel Insert2(LoginModel Model, int[] Permission)
        {
            Model.IsUserNameExist = IRepository.GetALL().Any(x => x.UserName.Trim() == Model.UserName.Trim() && x.UserID != Model.UserID);
           
            return Model;
        }

        public void Insert(LoginModel Model, int[] Permission)
        {
            Model.IsUserNameExist = IRepository.GetALL().Any(x => x.UserName == Model.UserName && x.UserID != Model.UserID);
            PermissionUser obj1 = new PermissionUser();
            Login obj = new Login();

            if (!Model.IsUserNameExist)
            {

                obj.UserName = Model.UserName;
                obj.Password = Model.Password;
                obj.UserID = Model.UserID;
                obj.FirstName = Model.FirstName;
                obj.LastName = Model.LastName;
                obj.Email = Model.Email;
                obj.Mobile = Model.Mobile;
                obj.DepartmentID = Model.DepartmentID;
                obj.IsClient = Model.IsClient;

                //obj.Password = HashPass(obj.Password);
                obj.Password = ConvertToEncrypt(obj.Password);

                IRepository.Insert(obj);

                if (Permission != null)
                {

                    foreach (var xx in Permission)
                    {

                        obj1.PermissionID = xx;


                        obj1.UserID = obj.UserID;
                        IRepositoryPermissionUser.Insert(obj1);


                    }
                }
            }
        }


        public string HashPass(string password)
        {

            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

            return encoded;//returns hashed version of password
        }
        public static string Key = "dsfdf@@cdczsd@";

        public static string ConvertToEncrypt(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            password += Key;
            var passwordByBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordByBytes);
        }
        public static string ConvertToDecrypt(string base64EncodeData)
        {
            if (string.IsNullOrEmpty(base64EncodeData)) return "";
            var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
            var result = Encoding.UTF8.GetString(base64EncodeBytes);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
        }
        public LoginModel GetID(int ID)
        {
            LoginModel Model = new LoginModel();

            Login LoginEntity = IRepository.GetID(ID);

            Model.UserID = LoginEntity.UserID;
            //Model.Password = LoginEntity.Password;
            Model.Password = ConvertToDecrypt(LoginEntity.Password);

            Model.UserName = LoginEntity.UserName;

            Model.Email = LoginEntity.Email;
            Model.FirstName = LoginEntity.FirstName;
            Model.LastName = LoginEntity.LastName;
            Model.Mobile = LoginEntity.Mobile;
            return Model;
        }

        public LoginModel Update(LoginModel Model, int[] Permission)
        {

            var S = IRepository.GetALL().Where(X => X.UserName.Trim() == Model.UserName.Trim() && X.UserID != Model.UserID).Count();
            if (S != 0)
            {
                Model.IsUserNameExist = true;

              
                return Model;
            }
            Model.IsClient = true;
            PermissionUser obj1 = new PermissionUser();

            var permissionold = IRepositoryPermissionUser.GetALL().Where(x => x.UserID == Model.UserID).ToList();
            foreach (var oldPer in permissionold)
            {
                IRepositoryPermissionUser.Delete(oldPer);
          
            }
            if (Permission != null)
            {


                foreach (var xx in Permission)
                {

                    obj1.PermissionID = xx;

                    obj1.UserID = Model.UserID;
                    IRepositoryPermissionUser.Insert(obj1);


                }
            }

            Login obj = new Login();
            obj.UserName = Model.UserName;
            //obj.Password = Model.Password;
            obj.Password = ConvertToEncrypt(Model.Password);

            obj.UserID = Model.UserID;
            obj.FirstName = Model.FirstName;
            obj.LastName = Model.LastName;
            obj.Email = Model.Email;
            obj.Mobile = Model.Mobile;
            obj.IsClient = Model.IsClient;

           
            IRepositoryUser.Update(obj);
            return Model;
        }



        public void Delete(int ID)
        {
            Login Login = IRepository.GetID(ID);
            var userPermission = IRepositoryPermissionUser.GetALL().Where(x => x.UserID == ID);
            if (userPermission.Any())
            {
                IRepositoryPermissionUser.Delete(userPermission);
               
            }
           
            var empID2 = IRepositoryTicket.GetALL().Where(x => x.AssignTo == ID);
            if (empID2.Any())
            {
                IRepositoryTicket.Delete(empID2);
             
            }

            var empID3 = IRepositoryProjectClient.GetALL().Where(x => x.ClientID == ID);
            if (empID3.Any())
            {
                IRepositoryProjectClient.Delete(empID3);
              
            } 
            Login.IsDelete = true;
            IRepository.Update(Login);
        }
    }
}
