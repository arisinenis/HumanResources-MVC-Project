using FluentValidation;
using HumanResources.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_ManagementProject.ValidationRules
{
    public class CreditCardValidator: AbstractValidator<CreditCard>
    {
        public CreditCardValidator()
        {
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Lütfen kart üstündeki isim soyisim bilgilerini giriniz.").NotNull().WithMessage("Lütfen isim soyisim giriniz.");
            RuleFor(x => x.CardNumber).NotEmpty().WithMessage("Lütfen kart numarası  bilgilerini giriniz.").NotNull().WithMessage("Lütfen kart numarası giriniz.");
            RuleFor(x => x.CVV).NotEmpty().WithMessage("Lütfen kart arkasındaki CCV kodu giriniz.").NotNull().WithMessage("Lütfen kart arkasındaki CCV kodu giriniz.");
            RuleFor(x => x.ExpirationDate).NotEmpty().WithMessage("Lütfen son kullanma tarihini giriniz.").NotNull().WithMessage("Lütfen son kullanma tarihini giriniz.");
        }
    }
}
