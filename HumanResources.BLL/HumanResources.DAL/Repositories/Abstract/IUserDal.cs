using HumanResources.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.DAL.Repositories.Abstract
{
    public interface IUserDal : IRepository<User>
    {
       User GetByEmailAndPassword(string email, string password);
        List<Permission> GetManagerWaitingPermissions(int CompanyID);
    }
}
