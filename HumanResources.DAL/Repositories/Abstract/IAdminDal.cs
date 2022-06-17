using HumanResources.Core.Entities;
using HumanResources.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.DAL.Repositories.Abstract
{
    public interface IAdminDal : IRepository<Admin>
    {
        Admin GetByEmailAndPassword(string email, string password);
    }
}
