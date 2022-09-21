using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=TicketingSystemConnection")
        {

        }

        public DbSet<Login> Logins { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectEmp> ProjectEmps { get; set; }
        public DbSet<ProjectClient> ProjectClients { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionUser> PermissionUser { get; set; }
        public DbSet<Approval> Approval { get; set; }
        public DbSet<Audit> Audit { get; set; } 

      
    }
}
