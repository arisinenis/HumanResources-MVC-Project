using FluentValidation;
using HumanResources.Core.Entities;
using System;

namespace HR_ManagementProject.ValidationRules
{
    public class PermissionValidator:AbstractValidator<Permission>
    {
        public PermissionValidator()
        {
            RuleFor(x => x.PermissionType).NotEmpty().WithMessage("Lütfen izin türünüzü giriniz.").NotNull().WithMessage("Lütfen izin türünüzü giriniz.");

            RuleFor(x => x.TotalDayOfPermissionType).NotEmpty().WithMessage("Lütfen toplam izin gününüzü giriniz.").NotNull().WithMessage("Lütfen toplam izin gününüzü giriniz.");

            RuleFor(x => x.RequestDate).NotEmpty().WithMessage("Lütfen izin talep tarihini giriniz.").NotNull().WithMessage("Lütfen izin talep tarihini giriniz.");

            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Lütfen izin başlangıç tarihini giriniz.").NotNull().WithMessage("Lütfen izin başlangıç giriniz.");
            RuleFor(x => x.StartDate).GreaterThanOrEqualTo(DateTime.Now).WithMessage("İzin başlangıç tarihi bugünün tarihinden önce olamaz.");

            RuleFor(x => x.EndDate).NotEmpty().WithMessage("Lütfen izin bitiş tarihini giriniz.").NotNull().WithMessage("Lütfen izin bitiş tarihini giriniz.");
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate).WithMessage("İzin bitiş tarihi başlangıç tarihinden önce olamaz.");
        }
    }
}
