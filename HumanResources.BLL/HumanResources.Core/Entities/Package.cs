








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

        [Required(ErrorMessage = "Lütfen Paket ismi giriniz.")]
        [MinLength(3, ErrorMessage = "Ürün adı en az 3 karakter olmalıdır.")]
        [Display(Name = "Paket Adı")]
        [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ]+$", ErrorMessage = "Lütfen sayı ve özel karakter kullanmayınız.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen Paket için açıklama giriniz.")]
        [MaxLength(200, ErrorMessage = "Ürün açıklaması en fazla 200 karakter olmalıdır.")]
        [Display(Name = "Açıklama")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Kampanya başlangıç tarihi")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; } /*= DateTime.Now;*/
        [DataType(DataType.Date)]
        [Display(Name = "Kampanya bitiş tarihi")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; } /*= DateTime.Now;*/
        [DataType(DataType.Date)]
        [Display(Name = "Satın alma tarihi")]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Lütfen Paket ücreti giriniz.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Ücret")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
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
        [Required(ErrorMessage = "Lütfen Kullanıcı sayısı giriniz.")]
        public int UsageAmount { get; set; }
        public string PhotoPath { get; set; }

        [NotMapped]
        [Display(Name = "Paket görseli")]
        public IFormFile Photo { get; set; }
        //[DataType(DataType.Currency)]
        //public decimal MinimumCost { get; set; }
        private bool _PackageStatus;
        public bool PackageStatus
        {
            get; set;
            
        }

        // Nav. Property
        public ICollection<Company> Companies { get; set; }
        //private bool _IsActive;
        //public bool IsActive
        //{
        //    get
        //    {
        //        if (StartDate < DateTime.Now)
        //        {
        //            return false;
        //        }
        //        else { return _IsActive; }
        //    }
        //    set
        //    {
        //        _IsActive = value;
        //    }

        //}
    }
}
