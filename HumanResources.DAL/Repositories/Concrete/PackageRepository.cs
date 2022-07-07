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
            IEnumerable<Package> packages = db.Packages.Where(z => z.UsageAmount >= company.PersonelSayisi).ToList();
            
            //foreach (Package item in db.Packages)
            //{
            //    if (company.PersonelSayisi <= item.UsageAmount)
            //    {
            //        packages.Add(item);
            //    }
            //}
            return packages;
        }
        public bool DeleteforPackage(int packageId)
        {
            Package deletedValue = db.Packages.FirstOrDefault(x => x.Id == packageId);
            deletedValue.PackageStatus = false;
            Update(deletedValue);
            return db.SaveChanges() > 0;
        }
        public IEnumerable<Package> GetActivePackages()
        {
            
            return db.Packages.Where(x => x.PackageStatus==true).ToList();
        }

        
    }
}
