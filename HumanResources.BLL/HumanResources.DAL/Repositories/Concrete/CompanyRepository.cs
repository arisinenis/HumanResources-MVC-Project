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
    public class CompanyRepository : GenericRepository<Company>, ICompanyDal
    {
        private readonly ApplicationDbContext db;

        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public bool UpdateCompanyManger(Company _company)
        {
            try
            {
                var updateCompany = new Company();
                updateCompany.Name = _company.Name;
                updateCompany.Address = _company.Address;
                updateCompany.PhoneNumber = _company.PhoneNumber;
                updateCompany.Photo = _company.Photo;
                db.Set<Company>().Update(updateCompany);
                return db.SaveChanges() > 0;
            }
            catch
            {

                return false;
            }
        }
        public int GetpackagesByCompanyID(int id)
        {
            return db.Set<Company>().Where(x=>x.Id==id).SelectMany(y=>y.Packages).Where(x=>x.PackageStatus==true).Count();
        }
        
    }
}
