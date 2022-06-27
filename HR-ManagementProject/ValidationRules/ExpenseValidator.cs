using FluentValidation;
using HumanResources.Core.Entities;

namespace HR_ManagementProject.ValidationRules
{
    public class ExpenseValidator : AbstractValidator<Expense>
    {
        public ExpenseValidator()
        {
            RuleFor(x => x.Total).NotEmpty().WithMessage("Lütfen harcama miktarınızı giriniz.").NotNull().WithMessage("Lütfen harcama miktarınızı giriniz.").GreaterThan(0).WithMessage("Lütfen uygun bir harcama tutarı giriniz.");/*.Must(x => x % 1 != 0).WithMessage("Lütfen geçerli bir tutar giriniz.");*/
            RuleFor(x => x.Description).NotEmpty().WithMessage("Lütfen açıklamanızı giriniz.").NotNull().WithMessage("Lütfen açıklamanızı giriniz.");
        }
    }
}
