using HumanResources.Core.Entities;
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
    public class WalletRepository : GenericRepository<Wallet>, IWalletDal
    {
        private readonly ApplicationDbContext db;
        public WalletRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public Wallet GetWalletWithCompany(int id)
        {
            return db.Wallets.Include(w => w.Company).Where(w => w.CompanyID == id).FirstOrDefault();
        }
    }
}
