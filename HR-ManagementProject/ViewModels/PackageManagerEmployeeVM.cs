using HumanResources.Core.Entities;
using System.Collections.Generic;

namespace HR_ManagementProject.ViewModels
{
    public class PackageManagerEmployeeVM
    {
        public PackageManagerEmployeeVM()
        {
            Employees = new List<Employee>();
            Managers = new List<User>();
            Packages = new List<Package>();
        }
        public IEnumerable<HumanResources.Core.Entities.Employee> Employees { get; set; }
        public IEnumerable<User> Managers { get; set; }
        public IEnumerable<Package> Packages { get; set; }
    }
}
