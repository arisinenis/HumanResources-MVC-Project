using FluentValidation;
using HumanResources.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_ManagementProject.ValidationRules
{
    public class EmployeeCreateValidator : AbstractValidator<Employee>
    {
        public EmployeeCreateValidator()
        {

            RuleFor(x => x.BirthDate.Date.Year).LessThanOrEqualTo(DateTime.Now.Year+18).WithMessage("Doğum tarihi 18 yaşından küçük olanlar çalışamaz");
        }
    }
}
