using HumanResources.Core.Entities;
using HumanResources.Core.Enums;
using HumanResources.DAL.Context;
using HumanResources.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.DAL.Repositories.Concrete
{
    public class AdvancePaymentRepository : GenericRepository<AdvancePayment>, IAdvancePaymentDal
    {
        private readonly ApplicationDbContext db;

        public AdvancePaymentRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public List<AdvancePayment> GetAllWaitingAdvancePayments(int id)
        {
            return db.AdvancePayments.Include(x=>x.Employee).Where(x => x.Status == PermissionStatus.Bekliyor && x.Employee.CompanyId==id).ToList();
        }

        public List<AdvancePayment> GetByEmployeeId(int id)
        {
            return db.AdvancePayments.Include(x => x.Employee).Where(x => x.EmployeeId == id).ToList();
        }
        public bool ApproveAdvancePayment(AdvancePayment advancePayment)
        {
            //var permissionDb = db.Permissions.Find(permission.Id);
            advancePayment.Status = PermissionStatus.Onaylandi;
            db.AdvancePayments.Update(advancePayment);
            return db.SaveChanges() > 0;
        }

        public bool RejectAdvancePayment(AdvancePayment advancePayment)
        {
            //var permissionDb = db.Permissions.Find(permission.Id);
            advancePayment.Status = PermissionStatus.Reddedildi;
            db.AdvancePayments.Update(advancePayment);
            return db.SaveChanges() > 0;
        }

    }
   
}
