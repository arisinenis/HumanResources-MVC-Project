using HumanResources.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.DAL.Repositories.Abstract
{
    public interface IPermissionDal : IRepository<Permission>
    {
        IEnumerable<Permission> GetAllPermissionById(int id);
        IEnumerable<Permission> GetAllWaitingPermission();
        bool ApprovePermission(Permission permission);
        bool RejectPermission(Permission permission);

    }
}
