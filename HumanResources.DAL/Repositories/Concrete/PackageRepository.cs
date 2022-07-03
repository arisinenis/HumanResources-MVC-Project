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
    public class PackageRepository : GenericRepository<Package>, IPackageDal
    {
        private readonly ApplicationDbContext db;

        public PackageRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }
        public IEnumerable<Package> GetByUsageAmount(int companyId)
        {
            Company company = db.Companies.FirstOrDefault(x => x.Id == companyId);
            List<Package> packages = new List<Package>();
            foreach (Package item in db.Packages)
            {
                if (company.PersonelSayisi <= item.UsageAmount)
                {
                    packages.Add(item);
                }
            }
            return packages;
        }
    }
}
