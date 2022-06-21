using HumanResources.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Core.Entities
{
    public class Permission : BaseEntity
    {
        public Permission()
        {
            //Employees = new HashSet<Employee>();
        }
        [Display(Name = "İzin Türü")]
        public string PermissionType { get; set; }
        [Display(Name = "Toplam İzin Süresi")]
        public string TotalDayOfPermissionType { get; set; }
        [Display(Name = "İzin Talep Tarihi")]
        public DateTime RequestDate { get; set; }
        [Display(Name = "İzin Başlangıç Tarihi")]
        public DateTime StartDate { get; set; }
        [Display(Name = "İzin Bitiş Tarihi")]
        public DateTime EndDate { get; set; }
        [Display(Name = "İzin Durumu")]
        public PermissionStatus PermissionStatus { get; set; } = PermissionStatus.Bekliyor;

        // Nav. Property

        //public IEnumerable<Employee> Employees { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        
    }
}
