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
    public class AdvancePayment:BaseEntity
    {
        [Display(Name = "Toplam Ödeme")]
        public decimal TotalPaymentRequest { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Talep Durumu")]
        public PermissionStatus Status { get; set; } = PermissionStatus.Bekliyor;
        [Display(Name = "Talep Tarihi")]
        [DataType(DataType.Date)]
        public DateTime CreateTime { get; set; }=DateTime.Now;
        public string FileName { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

        //Nav Properties
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        
    }
}
