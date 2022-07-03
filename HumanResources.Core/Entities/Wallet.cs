using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Core.Entities
{
    public class Wallet : BaseEntity
    {
        [Display(Name = "Toplam Bakiye")]
        //[DataType(DataType.Currency)]
        public decimal Balance { get; set; } = 0;

        [Display(Name = "Son Yükleme Zamanı")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime TopUpDate { get; set; } = DateTime.Now.Date;

        // Nav. Property
        public int CompanyID { get; set; }
        public Company Company { get; set; }
    }
}
