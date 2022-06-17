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
    public class Package : BaseEntity
    {
        public Package()
        {
            Companies = new HashSet<Company>();
        }

        [Required]
        [MinLength(3, ErrorMessage = "Ürün adı en az 3 karakter olmalıdır.")]
        [Display(Name = "Paket Adı")]
        public string Name { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "Ürün açıklaması en fazla 200 karakter olmalıdır.")]
        [Display(Name = "Açıklama")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Kampanya başlangıç tarihi")]
        public DateTime StartDate { get; set; } /*= DateTime.Now;*/
        [DataType(DataType.Date)]
        [Display(Name = "Kampanya bitiş tarihi")]
        public DateTime EndDate { get; set; } /*= DateTime.Now;*/
        [DataType(DataType.Date)]
        [Display(Name = "Satın alma tarihi")]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        [DataType(DataType.Currency)]
        [Display(Name = "Ücret")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal Cost { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Paket bitiş tarihi")]
        public DateTime Occupancy
        {
            get
            {
                return PurchaseDate.AddMonths(1);
            }

            set { PurchaseDate = value; }
        }
        [Display(Name = "Kullanıcı sayısı")]
        public int UsageAmount { get; set; }
        public string PhotoPath { get; set; }

        [NotMapped]
        [Display(Name = "Paket görseli")]
        public IFormFile Photo { get; set; }
        //[DataType(DataType.Currency)]
        //public decimal MinimumCost { get; set; }
        public bool PackageStatus { get; set; } = true;

        // Nav. Property
        public ICollection<Company> Companies { get; set; }
    }
}
