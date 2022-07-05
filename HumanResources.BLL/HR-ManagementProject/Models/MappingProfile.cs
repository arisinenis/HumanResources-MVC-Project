using AutoMapper;
using HR_ManagementProject.Areas.Employee.Models;
using HumanResources.Core.Entities;

namespace HR_ManagementProject.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeEditVM, Employee>();
            CreateMap<Employee, EmployeeEditVM>();

            CreateMap<PermissionEmployeeVM, Employee>();
            CreateMap<Employee, PermissionEmployeeVM>();

            CreateMap<PermissionEmployeeVM, Permission>();
            CreateMap<Permission, PermissionEmployeeVM>();

            
        }
    }
}
