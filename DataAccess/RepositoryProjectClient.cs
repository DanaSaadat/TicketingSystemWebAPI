using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RepositoryProjectClient : IRepositoryProjectClient
    {
        private readonly DataContext _context;

        public RepositoryProjectClient(DataContext context)
        {
            this._context = context;
        }


        public IEnumerable<ProjectClient> GetALL()
        {
            try
            {
             var xx= _context.ProjectClients.Include(x => x.Login).ToList();
                //return _context.ProjectClients.Include("Login").ToList();
                //return _context.ProjectClients.Include("Login").ToList();
                //return _context.ProjectClients.Include(x => x.Login).ToList();
                return xx;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
