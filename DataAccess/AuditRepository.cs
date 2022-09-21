using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AuditRepository : IAuditRepository
    {
        private readonly DataContext _context;

        public AuditRepository(DataContext context)
        {
            this._context = context;
        }
        public void Insert(Audit audit)
        {
            _context.Audit.Add(audit);
            _context.SaveChanges();
        }

        public IEnumerable<Audit> GetALL()
        {
            try
            {
                return _context.Audit.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
