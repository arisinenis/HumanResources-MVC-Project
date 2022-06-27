using HumanResources.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Core.Entities
{
    public class Employee : BaseEntity
    {
        public Employee()
        {
            Permissions = new HashSet<Permission>();
            Expenses = new HashSet<Expense>();
            advancePayments= new HashSet<AdvancePayment>();
        }
        [Display(Name = "İsim")]
        public string FirstName { get; set; }
        [Display(Name = "İkinci İsim (İsteğe bağlı)")]
        public string SecondName { get; set; }
        [Display(Name = "Soyisim")]
        public string LastName { get; set; }
        [Display(Name = "TC Kimlik No")]
        public string CitizenNo { get; set; }
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        //public string Email { get { return FirstName + "." + LastName + "@" + Company.Name + "." + "com"; } }

        public string Password { get; set; }
        [NotMapped] 
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public Gender Gender { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }
        
        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        
        [Display(Name = "İşe Giriş Tarihi")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "İşten Çıkış Tarihi")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Display(Name = "Durumu")]
        public bool Status { get; set; }
        [Display(Name = "Ünvan")]
        public string JobTitle { get; set; }
        [Display(Name = "Meslek")]
        public string Job { get; set; }
        public string PhotoPath { get; set; }

        [NotMapped]
        [Display(Name = "Fotoğraf")]
        public IFormFile Photo { get; set; }
        // Nav. property
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        [Display(Name = "Maaş")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        //public int PermissionId { get; set; }
        //public Permission Permission { get; set; }
        public string Role { get; set; } = "Employee";
        //public int PermissionId { get; set; }
        //public Permission Permission { get; set; }
        [NotMapped]
        public IEnumerable<Permission> Permissions { get; set; }
        [DefaultValue(true)]
        public bool IsFirstTime { get; set; } = true;

        public IEnumerable<Expense> Expenses { get; set; }

        public IEnumerable<AdvancePayment>advancePayments { get; set; }

    }
}
