using HumanResources.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_ManagementProject.Areas.CompanyManager.Model
{
    public class WalletCreditCardVM
    {
        public Wallet Wallet { get; set; }
        public IEnumerable<CreditCard> CreditCard { get; set; }
    }
}
