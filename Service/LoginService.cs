using DataAccess;
using DataAccess.Entity;
using Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class LoginService : ILoginService
    {
        private IRepository<Login> IRepository;
        private IRepository<PermissionUser> IRepositoryPermissionUser;
        private IRepository<Permission> IRepositoryPermission;
        private IRepository<Department> IRepositoryDepartment;
        private IRepository<Approval> IRepositoryApproval; 
        private IRepositoryUser IRepositoryUser; 
        public LoginService(Repository<Login> IRepositoryLogin, Repository<PermissionUser> iRepositoryPermissionUser, Repository<Permission> iRepositoryPermission, Repository<Department> iRepositoryDepartment, Repository<Approval> iRepositoryApproval, RepositoryUser RepositoryUser)
        {
            IRepository = IRepositoryLogin;
            IRepositoryPermissionUser = iRepositoryPermissionUser;
            IRepositoryPermission = iRepositoryPermission;
            IRepositoryDepartment = iRepositoryDepartment;
            IRepositoryApproval = iRepositoryApproval;
            IRepositoryUser = RepositoryUser;
        }
        public static string Key = "dsfdf@@cdczsd@";
        public static string ConvertToEncrypt(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            password += Key;
            var passwordByBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordByBytes);
        }
        public LoginModel login(LoginModel Model)
        {
            //Model.Password = HashPass(Model.Password);
            //Model.Password = ConvertToEncrypt(Model.Password);

            //var Password = DecryptStringAES(Model.Password);

            //var UserName =DecryptStringAES(Model.UserName);
            //Model.Password = ConvertToEncrypt(Model.Password);

            var resultado = (from p in IRepository.GetALL() where p.UserName.Trim() == Model.UserName.Trim() && p.Password == Model.Password select p).SingleOrDefault();
            Model.UserID = resultado.UserID;

            Model.lstRole = (from user in IRepository.GetALL()
                              join roleMapping in IRepositoryPermissionUser.GetALL()
                              on user.UserID equals roleMapping.UserID
                              join role in IRepositoryPermission.GetALL()
                              on roleMapping.PermissionID equals role.ID
                              where user.UserName.Trim() == Model.UserName.Trim()
                             select role.ID).ToList();

            Model.ManangerID = (from user in IRepository.GetALL()
                                join Department in IRepositoryDepartment.GetALL()
                        on user.DepartmentID equals Department.ID
                        //where user.UserID == objLogin.UserID
                        where user.UserName.Trim() == Model.UserName.Trim()
                                select Department.ManagerID).FirstOrDefault();

            Model.NotificationCount = (from Approval in IRepositoryApproval.GetALL()
                                     where Approval.ManagerID == Model.UserID
                                     && Approval.statusID == 7
                                     select Approval).ToList().Count();
            return Model;
        }

        public static string DecryptStringAES(string cipherText)
        {

            cipherText = cipherText.Replace(" ", "+");

            int mod4 = cipherText.Length % 4;

            if (mod4 > 0)
            {
                cipherText += new string('=', 4 - mod4);
            }

            var keybytes = Encoding.UTF8.GetBytes("8080808080808080");
            var iv = Encoding.UTF8.GetBytes("8080808080808080");

            var encrypted = Convert.FromBase64String(cipherText);




            var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
            return string.Format(decriptedFromJavascript);







        }






        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.  
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold  
            // the decrypted text.  
            string plaintext = null;

            // Create an RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings  
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for decryption.  
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        //ing(var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {

                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream  
                                // and place them in a string.  
                                plaintext = srDecrypt.ReadToEnd();

                            }

                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }

















        public string HashPass(string password)
        {

            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

            return encoded;//returns hashed version of password
        }


        public IEnumerable<LoginModel> GetALL()
        {


            IEnumerable<LoginModel> Users = IRepository.GetALL().Where(x => x.IsDelete.Equals(false)).Select(u => new LoginModel
            {

                UserID = u.UserID,
                UserName = u.UserName,
                Password =u.Password,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Mobile = u.Mobile,
                DepartmentID = u.DepartmentID,

            });
            return Users;
        }

     public   string[] Rolee(string username)
        {
            var userRoles = (from user in IRepository.GetALL()
                             join roleMapping in IRepositoryPermissionUser.GetALL()
                             on user.UserID equals roleMapping.UserID
                             join role in IRepositoryPermission.GetALL()
                             on roleMapping.PermissionID equals role.ID
                             where user.UserName == username
                             select role.Name).ToArray();

            return userRoles;
        }
    }
}
