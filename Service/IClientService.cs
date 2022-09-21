using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IClientService
    {

        IEnumerable<LoginModel> GetALL();

        void Insert(LoginModel Model, int[] Permission);

        //void Update(LoginModel Model, int[] Permission);
        LoginModel Update(LoginModel Model, int[] Permission);
        void Delete(int ID);

        LoginModel GetID(int ID);
        LoginModel Insert2(LoginModel Model, int[] Permission);
    }
}
