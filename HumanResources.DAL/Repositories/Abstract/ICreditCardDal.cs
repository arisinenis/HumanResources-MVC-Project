using HumanResources.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.DAL.Repositories.Abstract
{
    public interface ICreditCardDal: IRepository<CreditCard>
    {
        //CreditCard GetCreditCardWithCompany(int id);
        public IEnumerable<CreditCard> GetAllCreditCardById(int companyId);
    }
}
