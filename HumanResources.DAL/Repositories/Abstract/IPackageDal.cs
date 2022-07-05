using HumanResources.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.DAL.Repositories.Abstract
{
    public interface IPackageDal : IRepository<Package>
    {
        public IEnumerable<Package> GetByUsageAmount(int companyId);
        bool DeleteforPackage(int packageId);
        IEnumerable<Package> GetActivePackages();
    }
}
