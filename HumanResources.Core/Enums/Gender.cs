using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Core.Enums
{
    public enum Gender
    {
        [Display(Name="Kadın")]
        Female,
        [Display(Name = "Erkek")]
        Male
    }
}
