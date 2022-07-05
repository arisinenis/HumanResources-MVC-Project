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
    public class Expense : BaseEntity
    {
        public object Package;

        [Display(Name = "Toplam Miktar")]
        public decimal Total { get; set; }
        //[Display(Name = "Minimum Talep Edilebilir Miktar")]
        //public decimal MinAmount { get; set; } = 1000;
        //[Display(Name = "Maksimum Talep Edilebilir Miktar")]
        //public decimal MaxAmount { get; set; } // Maaşa göre hesaplanacak. (Personel maaşının yüzde 30u kadar olacak.). Sadece get edilebilir olmalı. Otomatik oluşabilir.
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Harcama Türü")]
        public ExpenseType ExpenseType { get; set; }
        [Display(Name = "Talep Durumu")]
        public PermissionStatus Status { get; set; } = PermissionStatus.Bekliyor;

        [Display(Name = "Talep Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime RequestDate { get; set; }
        [Display(Name = "Cevaplanma Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ResponseDate { get; set; }
        [Display(Name = "Doğrulama Belgesi")]
        public string FileName { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

        // Nav. Properties
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
