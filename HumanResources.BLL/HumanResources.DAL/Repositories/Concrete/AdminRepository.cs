using HumanResources.Core.Entities;
using HumanResources.DAL.Context;
using HumanResources.DAL.Repositories.Abstract;
using HumanResources.DAL.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.DAL.Repositories.Concrete
{
    public class AdminRepository : GenericRepository<Admin>, IAdminDal
    {
        private readonly ApplicationDbContext db;

        public AdminRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public Admin GetByEmailAndPassword(string email, string password)
        {
            return db.Admins.Where(a => a.Email == email && a.Password == password).FirstOrDefault();
        }
    }
}
