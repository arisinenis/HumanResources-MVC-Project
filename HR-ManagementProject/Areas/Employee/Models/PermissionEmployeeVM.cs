using HumanResources.Core.Entities;

namespace HR_ManagementProject.Areas.Employee.Models
{
    public class PermissionEmployeeVM
    {
        public Permission Permission { get; set; }
        public HumanResources.Core.Entities.Employee Employee { get; set; }
    }
}
