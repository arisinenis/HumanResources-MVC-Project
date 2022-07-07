using HumanResources.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HumanResources.Core.Entities
{
    public class Company : BaseEntity
    {
        public Company()
        {
            Users = new HashSet<User>();
            Packages = new HashSet<Package>();
            
        }

        [Required(ErrorMessage = "Lütfen şirket ismi giriniz.")]
        [Display(Name = "Şirket Adı")]
        [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ]+$", ErrorMessage = "Lütfen sayı ve özel karakter kullanmayınız.")]
        public string Name { get; set; }

        //[Required]
        [Display(Name = "Şirket Tipi")]
        [EnumDataType(typeof(CompanyType))]
        public CompanyType CompanyType { get; set; }

        //[Required(ErrorMessage = "Lütfen şirket ismi giriniz.")]
        [Display(Name = "Adres")]
        //[MaxLength(200, ErrorMessage = "Adres en fazla 200 karakter olmalıdır.")]
        //[DataType(DataType.MultilineText)]
        public string Address { get; set; }

        //[Required]
        [Display(Name = "Telefon Numarası")]
        //[MaxLength(11, ErrorMessage = "Adres en fazla 11 karakter olmalıdır.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Vergi numarası 10 haneli olmalıdır.")]
        [Display(Name = "Vergi Numarası")]
        public string TaxNumber { get; set; }

        [Display(Name = "Personel Sayısı")]
        public int PersonelSayisi { get; set; }
        [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ]+$", ErrorMessage = "Lütfen sayı ve özel karakter kullanmayınız.")]
        [Display(Name = "Vergi Dairesi")]
        public string TaxAdministration { get; set; }

        [RegularExpression(@"^(\d{16})$", ErrorMessage = "Mersis numarası 16 haneli olmalıdır.")]
        [Display(Name = "Mersis Numarası")]
        public string MersisNo { get; set; }

        
        public string PhotoPath { get; set; }

        [NotMapped]
        [Display(Name = "Şirket Logosu")]
        public IFormFile Photo { get; set; }

        // Admin tarafından atanacak
        // Nav. property

        //public int PackageId { get; set; }
        //public Package Package { get; set; }

        // Nav. Property
        public ICollection<User> Users { get; set; }
        public ICollection<Package> Packages { get; set; }

        //public int WalletID { get; set; }
        [JsonIgnore]
        public Wallet Wallet { get; set; }
        
    }
}
