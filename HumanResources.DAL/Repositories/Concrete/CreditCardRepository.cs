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
    public class CreditCardRepository : GenericRepository<CreditCard>, ICreditCardDal
    {
        private readonly ApplicationDbContext db;

        public CreditCardRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }
        //public CreditCard GetCreditCardWithCompany(int id) => db.CreditCards.Where(w => w. == id).FirstOrDefault();

        public IEnumerable<CreditCard> GetAllCreditCardById(int companyId) => db.CreditCards.Include(x=>x.Wallet).Where(a => a.Wallet.CompanyID == companyId).ToList();
    }
}
