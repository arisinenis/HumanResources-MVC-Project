using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Core.Entities
{
    public class CreditCard:BaseEntity
    {
        [Display(Name = "Kart Sahibi Ad Soyad")]
        public string NameSurname { get; set; }

        [Display(Name = "Kart Numarası")]
        public string CardNumber { get; set; }

        [Display(Name = "Kart Güvenlik Numarası")]
        public int CCV { get; set; }

        [Display(Name = "Kart Son Kullanma Tarihi")]
        public DateTime ExpirationDate { get; set; } = DateTime.Now;

        public int CompanyID { get; set; }
        public Company Company { get; set; }
        public Wallet Wallet { get; set; }

    }
}
