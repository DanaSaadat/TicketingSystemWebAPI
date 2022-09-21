using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ILoginService
    {
        //IEnumerable<LoginModel> GetALL();

        LoginModel login(LoginModel Model);

        IEnumerable<LoginModel> GetALL();
        string[] Rolee(string username);

    }
}
