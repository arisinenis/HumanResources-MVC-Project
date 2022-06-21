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
    public class UserRepository : GenericRepository<User>, IUserDal
    {
        private readonly ApplicationDbContext db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public User GetByEmailAndPassword(string email, string password)
        {
            return db.Users.Where(p => p.Email == email && p.Password == password).FirstOrDefault();
        }
        public List<Permission> GetManagerWaitingPermissions(int CompanyID)
        {
            return db.Permissions.Where(x => x.Employee.CompanyId == CompanyID).Where(x => x.PermissionStatus == Core.Enums.PermissionStatus.Bekliyor).ToList();
        }
    }
}
