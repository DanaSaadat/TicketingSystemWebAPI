using DataAccess.Entity;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserService
    {
        IEnumerable<LoginModel> GetALL();
        IQueryable<LoginModel> GetALLIQueryable();

        IEnumerable<LoginModel> GetALLQueryable();
        IEnumerable<LoginModel> GetALLClient();

        IEnumerable<LoginModel> GetALLbyDepartment(int id); 

        void Insert(LoginModel Model, int[] Permission);

        LoginModel Update(LoginModel Model, int[] Permission);
         
        void Delete(int ID);

        LoginModel GetID(int ID);
        //LoginModel Insert1(LoginModel Model, int[] Permission);
        LoginModel Insert2(LoginModel Model, int[] Permission);
        IEnumerable<LoginModel> GetALL1();
        void InsertAdo(LoginModel Model, int[] Permission);
        LoginModel GetID1(int ID);
        LoginModel Update1(LoginModel Model, int[] Permission);
        void Delete1(int ID);

        IEnumerable<LoginModel> GetALLPaged(int PageNumber, int PageSize);
    }
}
