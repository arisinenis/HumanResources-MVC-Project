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
    public class PermissionRepository : GenericRepository<Permission>, IPermissionDal
    {
        private readonly ApplicationDbContext db;

        public PermissionRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public bool ApprovePermission(Permission permission)
        {
            //var permissionDb = db.Permissions.Find(permission.Id);
            permission.PermissionStatus = PermissionStatus.Onaylandi;
            db.Permissions.Update(permission);
            return db.SaveChanges() > 0;
        }

        public IEnumerable<Permission> GetAllPermissionById(int id)
        {
            return db.Permissions.Where(p => p.EmployeeId == id).ToList();
        }

        public IEnumerable<Permission> GetAllWaitingPermission()
        {
            return db.Permissions.Include(x=>x.Employee).Where(p =>  p.PermissionStatus == PermissionStatus.Bekliyor).ToList();
        }

        public bool RejectPermission(Permission permission)
        {
            var permissionDb = db.Permissions.Find(permission.Id);
            permissionDb.PermissionStatus = PermissionStatus.Reddedildi;
            db.Permissions.Update(permission);
            return db.SaveChanges() > 0;
        }
    }
}
