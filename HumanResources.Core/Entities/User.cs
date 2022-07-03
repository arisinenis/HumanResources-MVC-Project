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
        
        [MaxLength(50, ErrorMessage = "İsim en fazla 50 karakter olmalıdır.")]
        [Required(ErrorMessage = "Lütfen isminizi giriniz.")]
        [Display(Name = "İsim")]
        [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ]+$", ErrorMessage = "Lütfen sayı ve özel karakter kullanmayınız.")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "İkinci İsim en fazla 50 karakter olmalıdır.")]
        [Display(Name = "İkinci İsim (İsteğe Bağlı)")]
        [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ]+$", ErrorMessage = "Lütfen sayı ve özel karakter kullanmayınız.")]
        public string? SecondName { get; set; }

        
        [MaxLength(50, ErrorMessage = "Soyisim en fazla 50 karakter olmalıdır.")]
        [Required(ErrorMessage = "Lütfen isminizi giriniz.")]
        [Display(Name = "Soyisim")]
        [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ]+$", ErrorMessage = "Lütfen sayı ve özel karakter kullanmayınız.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Lütfen mail adresinizi giriniz.")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Lütfen mail adresinizi kontrol ediniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen adresinizi giriniz.")]
        [Display(Name = "Adres")]
        [MaxLength(200, ErrorMessage = "Adres en fazla 200 karakter olmalıdır.")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Lütfen telefon numaranızı giriniz.")]
        [Display(Name = "Telefon Numarası")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        [Display(Name = "Şifre")]
        ////[RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:])([^\s]){8,16}$", ErrorMessage = "1 adet rakam (0-9), 1 adet büyük harf, 1 adet küçük harf, 1 adet özel karakter, 8 - 16 karakter arası, boşluk içeremez.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen TC Kimlik numaranızı giriniz.")]
        [Display(Name = "Kimlik Numarası")]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "TC Kimlik numarası 11 haneli olmalıdır.")]
        public string CitizenNo { get; set; }

        
        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; } = DateTime.Now;

        //[Required]
        [Display(Name = "Kan Grubu")]
        public BloodType BloodType { get; set; }

        //[Required]
        //[MaxLength(100, ErrorMessage = "Unvan en fazla 11 karakter olmalıdır.")]
        [Display(Name = "Ünvan")]
        [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ]+$", ErrorMessage = "Lütfen sayı ve özel karakter kullanmayınız.")]
        public string JobTitle { get; set; }

        //[Required]
        //[MaxLength(100, ErrorMessage = "Meslek en fazla 11 karakter olmalıdır.")]
        [Display(Name = "Meslek")]
        [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ]+$", ErrorMessage = "Lütfen sayı ve özel karakter kullanmayınız.")]
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
