using HumanResources.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.DAL.Repositories.Abstract
{
    public interface IWalletDal : IRepository<Wallet>
    {
        Wallet GetWalletWithCompany(int id);
    }
}
