using DataAccess;
using DataAccess.Entity;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuditService : IAuditService
    {

        private IAuditRepository IAuditRepository;

        public AuditService(AuditRepository AuditRepository)
        {
            this.IAuditRepository = AuditRepository;

        }
        public void Insert(AuditModel Model)
        {
            Audit obj = new Audit();

            obj.AuditID = Model.AuditID;
            obj.AreaAccessed = Model.AreaAccessed;
            obj.IPAddress = Model.IPAddress;
            obj.Time = Model.Time;
            obj.UserName = Model.UserName;
            obj.Response = Model.Response;
            obj.Bug = Model.Bug;
            obj.Validation = Model.Validation;
            obj.ResponseObject = Model.ResponseObject;

            IAuditRepository.Insert(obj);



        }


        public IEnumerable<AuditModel> GetALL()
        {

            IEnumerable<AuditModel> Audits = IAuditRepository.GetALL().Select(u => new AuditModel
            {
                UserName = u.UserName,
                IPAddress = u.IPAddress,
                AreaAccessed = u.AreaAccessed,
                Time = u.Time,
                Bug = u.Bug,
                Response = u.Response,
                ResponseObject = u.ResponseObject,
                Validation = u.Validation,

            });
            return Audits;
        }
    }
}
