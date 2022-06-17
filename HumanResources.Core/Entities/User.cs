using HumanResources.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Core.Entities
{
    public class User : BaseEntity
    {
        //[Required]
        //[MaxLength(50, ErrorMessage = "İsim en fazla 3 karakter olmalıdır.")]
        [Display(Name = "İsim")]
        public string FirstName { get; set; }

        //[MaxLength(50, ErrorMessage = "İkinci İsim en fazla 3 karakter olmalıdır.")]
        [Display(Name = "İkinci İsim (İsteğe Bağlı)")]
        public string? SecondName { get; set; }

        //[Required]
        //[MaxLength(50, ErrorMessage = "Soyisim en fazla 3 karakter olmalıdır.")]
        [Display(Name = "Soyisim")]
        public string LastName { get; set; }

        //[Required]
        //[Display(Name = "Email")]
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Adres")]
        //[MaxLength(200, ErrorMessage = "Adres en fazla 200 karakter olmalıdır.")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        //[Required]
        //[Display(Name = "Telefon Numarası")]
        //[MaxLength(11, ErrorMessage = "Telefon numarası en fazla 11 karakter olmalıdır.")]
        //[DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        ////[Required]
        //[Display(Name = "Şifre")]
        ////[RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:])([^\s]){8,16}$", ErrorMessage = "1 adet rakam (0-9), 1 adet büyük harf, 1 adet küçük harf, 1 adet özel karakter, 8 - 16 karakter arası, boşluk içeremez.")]
        ////[DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required]
        [Display(Name = "Kimlik Numarası")]
        //[MaxLength(11, ErrorMessage = " TC numarası en fazla 11 karakter olmalıdır.")]
        public string CitizenNo { get; set; }

        //[Required]
        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        //[Required]
        [Display(Name = "Kan Grubu")]
        public BloodType BloodType { get; set; }

        //[Required]
        //[MaxLength(100, ErrorMessage = "Unvan en fazla 11 karakter olmalıdır.")]
        [Display(Name = "Unvan")]
        public string JobTitle { get; set; }

        //[Required]
        //[MaxLength(100, ErrorMessage = "Meslek en fazla 11 karakter olmalıdır.")]
        [Display(Name = "Meslek")]
        public string Profession { get; set; }

        //[Required]
        [Display(Name = "Fotoğraf")]
        //[DataType(DataType.ImageUrl)]
        public string PhotoPath { get; set; }

        [NotMapped]
        [Display(Name = "Fotoğraf")]
        public IFormFile Photo { get; set; }

        // Nav Property
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string Role { get; set; } = "Manager";
    }
}
