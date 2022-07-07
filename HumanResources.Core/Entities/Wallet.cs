using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HumanResources.Core.Entities
{
    
    public class Wallet : BaseEntity
    {
        public Wallet()
        {
            CreditCards = new HashSet<CreditCard>();
        }
        [Display(Name = "Toplam Bakiye")]
        
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Bakiye negatif değer olamaz.")]
        //[DataType(DataType.Currency)]
        public decimal Balance { get; set; } = 0;

        [Display(Name = "Son Yükleme Zamanı")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime TopUpDate { get; set; } = DateTime.Now.Date;

        // Nav. Property
        public int CompanyID { get; set; }
        public Company Company { get; set; }
        [JsonIgnore]
        public IEnumerable<CreditCard> CreditCards { get; set; }
    }
}
