using HumanResources.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.BLL.Abstract
{
    public interface IEmployeeService:IService<Employee>
    {
        Employee GetByEmailAndPassword(string email, string password);
        bool CreateCMAreaEmployee(Employee _employee);

    }
}
