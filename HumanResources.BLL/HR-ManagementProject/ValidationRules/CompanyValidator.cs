using FluentValidation;
using HumanResources.Core.Entities;

namespace HR_ManagementProject.ValidationRules
{
    public class CompanyValidator: AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen şirketin ismini giriniz.").NotNull().WithMessage("Lütfen şirketin ismini giriniz.").MaximumLength(50).WithMessage("İsim 50 karakterden uzun olamaz.");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Lütfen adresi giriniz.").NotNull().WithMessage("Lütfen adresi giriniz.").MaximumLength(200).WithMessage("Adres 200 karakterden uzun olamaz.");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Lütfen telefon numarasını giriniz.").NotNull().WithMessage("Lütfen telefon numarasını giriniz.");

            RuleFor(x => x.TaxNumber).NotEmpty().WithMessage("Lütfen vergi numarasını giriniz.").NotNull().WithMessage("Lütfen vergi numarasını giriniz.");

            RuleFor(x => x.TaxAdministration).NotEmpty().WithMessage("Lütfen vergi dairenizi giriniz.").NotNull().WithMessage("Lütfen vergi dairenizi giriniz.");

            RuleFor(x => x.MersisNo).NotEmpty().WithMessage("Lütfen MERSIS numaranızı giriniz.").NotNull().WithMessage("Lütfen MERSIS numaranızı giriniz.");
        }
    }
}
