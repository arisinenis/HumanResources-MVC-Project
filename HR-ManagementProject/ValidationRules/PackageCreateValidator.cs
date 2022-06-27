using FluentValidation;
using HumanResources.Core.Entities;
using HR_ManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_ManagementProject.ValidationRules
{
    public class PackageCreateValidator : AbstractValidator<Package>
    {
        public PackageCreateValidator()
        {
            // Datelerin diğer kontrolleri business katmanında yapılabilir.
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen Paket ismi giriniz.").NotNull().WithMessage("Lütfen Paket ismi giriniz.").Matches("^(?=.*?[A-Za-z])[A-Za-z+]+$").WithMessage("Paket ismi yalnızca harflerden oluşabilir.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Lütfen Paket açıklaması giriniz.").NotNull().WithMessage("Lütfen Paket açıklaması giriniz.");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Lütfen kampanya başlangıç tarihi giriniz.").NotNull().WithMessage("Lütfen kampanya başlangıç tarihi giriniz.");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("Lütfen kampanya bitiş tarihi giriniz.").NotNull().WithMessage("Lütfen kampanya bitiş tarihi giriniz.");
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x=>x.StartDate).WithMessage("Kampanya başlangıç tarihi bitiş tarihinden ileride olamaz.");
            //RuleFor(x => x.Package.StartDate).LessThanOrEqualTo(DateTime.Now).WithMessage("Kampanya başlangıç tarihi bugünün tarihinden eski olamaz.");
            RuleFor(x => x.EndDate).LessThanOrEqualTo(x => x.StartDate.AddYears(1)).WithMessage("Paket başlangıç ​​tarihi için geçersiz giriş.");
            RuleFor(x => x.Cost).NotEmpty().WithMessage("Lütfen paket ücreti giriniz.").NotNull().WithMessage("Lütfen paket ücreti giriniz.");
            RuleFor(x => x.Cost).GreaterThanOrEqualTo(0).WithMessage("Paket ücreti sıfırdan küçük olamaz.");
            RuleFor(x => x.UsageAmount).GreaterThanOrEqualTo(0).WithMessage("Kullanıcı sayısı 0'dan küçük olamaz.");

            //JKJJKHGB


            //RuleFor(x => x.Package.Occupancy).NotNull().WithMessage("Please enter package occupancy date").NotEmpty().WithMessage("Please enter package occupancy date");
            //RuleFor(x => x.Package.Photo).NotEmpty().WithMessage("Please enter package photo").NotNull().WithMessage("Please enter package photo");
            //RuleFor(x => x.Package.MinimumCost).NotNull().WithMessage("Please enter package minimum cost").NotEmpty().WithMessage("Please enter package minimum cost");
        }
    }
}
