using HumanResources.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_ManagementProject.Areas.CompanyManager.Model
{
    public class EmployeePermissionVM
    {
        public HumanResources.Core.Entities.Employee Employees { get; set; }
        public IEnumerable<Permission> Permission { get; set; }
    }
}
