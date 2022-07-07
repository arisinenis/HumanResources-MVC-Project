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
        [Required(ErrorMessage = "Lütfen Kart adı giriniz.")]
        [MinLength(3, ErrorMessage = "Kart adı en az 3 karakter olmalıdır.")]
        [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ]+$", ErrorMessage = "Lütfen sayı ve özel karakter kullanmayınız.")]
        [Display(Name = "Kart Adı")]

        public string CardName { get; set; }
        [Required(ErrorMessage = "Lütfen Kart üzerindeki ad soyad  giriniz.")]
        [MinLength(3, ErrorMessage = "Kart üzerindeki ad soyad en az 3 karakter olmalıdır.")]
        [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ]+$", ErrorMessage = "Lütfen sayı ve özel karakter kullanmayınız.")]
        [Display(Name = "Kart Sahibi Ad Soyad")]
        public string NameSurname { get; set; }
        
        [Required(ErrorMessage = "Lütfen Kart üzerindeki kart numarası  giriniz.")]
        [MinLength(16, ErrorMessage = "Kart üzerindeki nnumara en az 16 karakter olmalıdır.")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Kart numarası sadece rakamlardan oluşmalıdır.")]

        [Display(Name = "Kart Numarası")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Lütfen Kartınızın arka tarafındaki 3 haneli CVV kodunu giriniz")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Kart Güvenlik Numarası")]
        //[MaxLength(3,ErrorMessage = "CVC kodu 3 haneli olmalıdır."),MinLength(3,ErrorMessage ="CVC kodu 3 haneli olmalıdır.")]
        public int CVV { get; set; }

        [Required(ErrorMessage = "Lütfen Kart bakiyesi giriniz")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Kredi Kartı Bakiyesi")]
        public decimal Bakiye { get; set; }

        [Required(ErrorMessage = "Lütfen Kart son kullanma tarihi giriniz")]

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yy-MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "Kart Son Kullanma Tarihi")]
        public string ExpirationDate { get; set; } = "00/00";

        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }

    }
}
