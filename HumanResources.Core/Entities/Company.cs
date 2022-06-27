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
    public class Company : BaseEntity
    {
        public Company()
        {
            Users = new HashSet<User>();
        }

        //[Required]
        [Display(Name = "Şirket Adı")]
        public string Name { get; set; }

        //[Required]
        [Display(Name = "Şirket Tipi")]
        [EnumDataType(typeof(CompanyType))]
        public CompanyType CompanyType { get; set; }

        //[Required]
        [Display(Name = "Adres")]
        //[MaxLength(200, ErrorMessage = "Adres en fazla 200 karakter olmalıdır.")]
        //[DataType(DataType.MultilineText)]
        public string Address { get; set; }

        //[Required]
        [Display(Name = "Telefon Numarası")]
        //[MaxLength(11, ErrorMessage = "Adres en fazla 11 karakter olmalıdır.")]
        //[DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        
        [Display(Name = "Vergi Numarası")]
        public string TaxNumber { get; set; }

        
        [Display(Name = "Vergi Dairesi")]
        public string TaxAdministration { get; set; }

        
        [Display(Name = "Mersis Numarası")]
        public string MersisNo { get; set; }

        
        public string PhotoPath { get; set; }

        [NotMapped]
        [Display(Name = "Şirket Logosu")]
        public IFormFile Photo { get; set; }

        // Admin tarafından atanacak
        // Nav. property

        public int PackageId { get; set; }
        public Package Package { get; set; }

        // Nav. Property
        public ICollection<User> Users { get; set; }
    }
}
