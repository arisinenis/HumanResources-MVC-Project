using HumanResources.Core.Entities;
using HumanResources.DAL.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.DAL.Repositories.Abstract
{
    public interface ICompanyDal : IRepository<Company>
    {
        int GetpackagesByCompanyID(int id);
    }
}
