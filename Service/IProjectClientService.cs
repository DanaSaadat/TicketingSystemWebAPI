using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Models;

namespace Service
{
    public interface IProjectClientService
    {
      IEnumerable<ProjectClientModel> GetALL(); 

        //void Insert(ProjectClientModel Model);

        //void Update(ProjectClientModel Model);

        void Delete(int ID);
        //void Delete(ProjectClientModel Model);
        //void Delete(IEnumerable<ProjectClientModel> Model); 
        //ProjectClientModel GetID(int ID);
    }
}
