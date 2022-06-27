using FluentValidation;
using HumanResources.Core.Entities;

namespace HR_ManagementProject.ValidationRules
{
    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Lütfen isminizi giriniz.").NotNull().WithMessage("Lütfen isminizi giriniz.").MaximumLength(50).WithMessage("İsim 50 karakterden uzun olamaz.");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Lütfen soyisminizi giriniz.").NotNull().WithMessage("Lütfen soyisminizi giriniz.").MaximumLength(50).WithMessage("Soyisim 50 karakterden uzun olamaz.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Lütfen emailinizi giriniz.").NotNull().WithMessage("Lütfen emailinizi giriniz.").EmailAddress().WithMessage("Lütfen geçerli bir email adresi giriniz.");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Lütfen adresinizi giriniz.").NotNull().WithMessage("Lütfen adresinizi giriniz.").MaximumLength(200).WithMessage("Adres 200 karakterden uzun olamaz.");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Lütfen telefon numaranızı giriniz.").NotNull().WithMessage("Lütfen telefon numaranızı giriniz.");

            //RuleFor(x => x.Password).NotEmpty().WithMessage("Lütfen şifrenizi giriniz.").NotNull().WithMessage("Lütfen şifrenizi giriniz.");

            RuleFor(x => x.CitizenNo).NotEmpty().WithMessage("Lütfen TC Kimlik numaranızı giriniz.").NotNull().WithMessage("Lütfen TC Kimlik numaranızı giriniz.");

            // Bugünden az olmalı ?
            RuleFor(x => x.BirthDate).NotEmpty().WithMessage("Lütfen doğum tarihinizi giriniz.").NotNull().WithMessage("Lütfen doğum tarihinizi giriniz.").WithMessage("Lütfen doğum tarihinizi giriniz.").LessThan(System.DateTime.Now);

            RuleFor(x => x.JobTitle).NotEmpty().WithMessage("Lütfen ünvanınızı giriniz.").NotNull().WithMessage("Lütfen ünvanınızı giriniz.").MaximumLength(50).WithMessage("Ünvan 100 karakterden uzun olamaz.");

            RuleFor(x => x.Profession).NotEmpty().WithMessage("Lütfen mesleğinizi giriniz.").NotNull().WithMessage("Lütfen mesleğinizi giriniz.").MaximumLength(50).WithMessage("Meslek 100 karakterden uzun olamaz.");


        }
    }
}
