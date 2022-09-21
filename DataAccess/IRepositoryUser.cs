using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepositoryUser
    {
        IEnumerable<Login> GetALL();

        void Insert(Login LoginEntity);

        void Update(Login LoginEntity); 

        void Delete(int ID);

        Login GetID(int ID);
    }
}
