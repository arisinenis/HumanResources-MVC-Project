using HumanResources.Core.Entities;
using HumanResources.DAL.Context;
using HumanResources.DAL.Repositories.Abstract;
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

        public IEnumerable<Permission> GetAllPermissionById(int id)
        {
            return db.Permissions.Where(p => p.Id == id).ToList();
        }
    }
}
