using HumanResources.BLL.Abstract;
using HumanResources.Core.Entities;
using HumanResources.Core.Enums;
using HumanResources.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.BLL.Concrete
{
    public class PermissionManager: IPermissionService
    {
        private readonly IPermissionDal permissionRepository;
        public PermissionManager(IPermissionDal permissionRepository)
        {
            this.permissionRepository = permissionRepository;
        }

        public bool Add(Permission entity)
        {
            return permissionRepository.Add(entity);
        }

        public bool ApprovePermission(Permission permission)
        {
            if (permission != null)
            {
                return permissionRepository.ApprovePermission(permission);
            }
            else
            {
                return false;
            }
        }

        public bool Delete(Permission entity)
        {
            return permissionRepository.Delete(entity);
        }

        public bool Exists(int id)
        {
            return permissionRepository.Exists(id);
        }

        public IEnumerable<Permission> GetAll()
        {
            return permissionRepository.GetAll().ToList();
        }

        public IEnumerable<Permission> GetAllPermissionById(int id)
        {
            return permissionRepository.GetAllPermissionById(id);
        }

        public IEnumerable<Permission> GetAllWaitingPermission()
        {
            return permissionRepository.GetAllWaitingPermission();
        }

        public Permission GetById(int id)
        {
            return permissionRepository.GetById(id);
        }

        public bool RejectPermission(Permission permission)
        {
            if (permission != null)
            {
                return permissionRepository.RejectPermission(permission);
            }
            else
            {
                return false;
            }
        }

        public bool Update(Permission entity)
        {
            return permissionRepository.Update(entity);
        }
    }
}
