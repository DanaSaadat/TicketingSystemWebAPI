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
    public class ProjectClientService : IProjectClientService
    {


        private IRepository<ProjectClient> IRepository;
        private IRepositoryProjectClient IRepositoryProjectClient;

        public ProjectClientService(Repository<ProjectClient> userRepository, RepositoryProjectClient iRepositoryProjectClient)
        {

            this.IRepository = userRepository;
            IRepositoryProjectClient = iRepositoryProjectClient;
        }





        public void Delete(int ID)
        {
            IRepository.Delete(ID);
        }

        public IEnumerable<ProjectClientModel> GetALL()
        {
            IEnumerable<ProjectClientModel> ProjectClients = IRepositoryProjectClient.GetALL().Select(u => new ProjectClientModel
            {

                id = u.id,
               ProjectID = u.ProjectID,
               ClientID = u.ClientID,
                Login = new LoginModel()
                {
                    UserID = u.Login.UserID
                }

            });
            return ProjectClients; 
        }

        //public ProjectClientModel GetID(int ID)
        //{
        //    return IRepository.GetID(ID);
        //}

        //public void Insert(ProjectClientModel Model)
        //{
        //    IRepository.Insert(entity);
        //}

        //public void Update(ProjectClientModel Model)
        //{
        //    IRepository.Update(entity);
        //}

        //public void Delete(ProjectClientModel Model)
        //{
        //    IRepository.Delete(entity);
        //}

        //public void Delete(IEnumerable<ProjectClientModel> Model)
        //{
        //    IRepository.Delete(entity);
        //}
    }
}
