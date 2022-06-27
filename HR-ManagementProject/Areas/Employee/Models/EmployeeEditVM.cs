using HumanResources.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace HR_ManagementProject.Areas.Employee.Models
{
    public class EmployeeEditVM
    {
        public int? Id { get; set; }
        [Display(Name = "İsim")]
        public string FirstName { get; set; }
        [Display(Name = "İkinci İsim (İsteğe bağlı)")]

        public string SecondName { get; set; }
        [Display(Name = "Soyisim")]
        public string LastName { get; set; }
        [Display(Name = "Telefon Numaras")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Adres")]
        public string Address { get; set; }
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}
