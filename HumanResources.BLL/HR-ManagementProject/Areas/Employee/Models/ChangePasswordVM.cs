using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_ManagementProject.Areas.Employee.Models
{
    [NotMapped]
    public class ChangePasswordVM
    {
        [Display(Name = "Eski Şifre")]
        [Required(ErrorMessage = "Lütfen eski şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Display(Name = "Yeni Şifre")]
        [Required(ErrorMessage = "Lütfen yeni şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Display(Name = "Yeni Şifreyi Doğrula")]
        [Required(ErrorMessage = "Lütfen yeni şifrenizi tekrar giriniz.")]
        [Compare(otherProperty: "NewPassword", ErrorMessage = "Girdiğiniz şifre yeni şifrenizle eşleşmedi.")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
