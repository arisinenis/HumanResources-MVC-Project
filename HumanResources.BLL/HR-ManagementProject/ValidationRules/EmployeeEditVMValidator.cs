using FluentValidation;
using HR_ManagementProject.Areas.Employee.Models;

namespace HR_ManagementProject.ValidationRules
{
    public class EmployeeEditVMValidator:AbstractValidator<EmployeeEditVM>
    {
        public EmployeeEditVMValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Lütfen isminizi giriniz.").NotNull().WithMessage("Lütfen isminizi giriniz.");
            RuleFor(x => x.SecondName).NotEmpty().WithMessage("Lütfen ikinci isminizi giriniz.").NotNull().WithMessage("Lütfen ikinci isminizi giriniz.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Lütfen soyadınızı giriniz.").NotNull().WithMessage("Lütfen soyadınızı girinizi");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Lütfen telefon numaranızı giriniz.").NotNull().WithMessage("Lütfen telefon numaranızı giriniz.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Lütfen adresinizi giriniz.").NotNull().WithMessage("Lütfen adresinizi giriniz.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Lütfen parolanızı giriniz.").NotNull().WithMessage("Lütfen parolanızı giriniz.");

        }
    }
}
