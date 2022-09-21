using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepositoryTicket
    {
        IEnumerable<Ticket> GetALL();

        void Insert(Ticket TicketEntity); 

        void Update(Ticket TicketEntity);



        Ticket GetID(int ID);

        void Delete(int ID);
        void Delete(Ticket TicketEntity);
        void Delete(IEnumerable<Ticket> TicketEntity); 
    }
}
