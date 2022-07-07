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
        [MaxLength(50, ErrorMessage = "İsim en fazla 50 karakter olmalıdır.")]
        [Required(ErrorMessage = "Lütfen isminizi giriniz.")]
        [Display(Name = "İsim")]
        [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ]+$", ErrorMessage = "Lütfen sayı ve özel karakter kullanmayınız.")]
        public string FirstName { get; set; }
        [MaxLength(50, ErrorMessage = "İkinci İsim en fazla 50 karakter olmalıdır.")]
        [Display(Name = "İkinci İsim (İsteğe Bağlı)")]
        [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ]+$", ErrorMessage = "Lütfen sayı ve özel karakter kullanmayınız.")]
        public string SecondName { get; set; }
        [MaxLength(50, ErrorMessage = "Soyisim en fazla 50 karakter olmalıdır.")]
        [Required(ErrorMessage = "Lütfen isminizi giriniz.")]
        [Display(Name = "Soyisim")]
        [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ]+$", ErrorMessage = "Lütfen sayı ve özel karakter kullanmayınız.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Lütfen TC Kimlik numaranızı giriniz.")]
        [Display(Name = "Kimlik Numarası")]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "TC Kimlik numarası 11 haneli olmalıdır.")]
        public string CitizenNo { get; set; }
        [Required(ErrorMessage = "Lütfen telefon numaranızı giriniz.")]
        [Display(Name = "Telefon Numarası")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string PhoneNumber { get; set; }
        //[Required(ErrorMessage = "Lütfen mail adresinizi giriniz.")]
        [Display(Name = "Email")]
        //[DataType(DataType.EmailAddress)]
        //[RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        //ErrorMessage = "Lütfen mail adresinizi kontrol ediniz.")]
        public string Email { get; set; }
        //public string Email { get { return FirstName + "." + LastName + "@" + Company.Name + "." + "com"; } }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [NotMapped] 
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Lütfen adresinizi giriniz.")]
        [Display(Name = "Adres")]
        [MaxLength(200, ErrorMessage = "Adres en fazla 200 karakter olmalıdır.")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        
        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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
        [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ]+$", ErrorMessage = "Lütfen sayı ve özel karakter kullanmayınız.")]
        public string JobTitle { get; set; }
        [Display(Name = "Meslek")]
        [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ]+$", ErrorMessage = "Lütfen sayı ve özel karakter kullanmayınız.")]
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
