using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Core.Enums
{
    public enum BloodType
    {
        [Display(Name = "A Rh +")]
        ArhP,
        [Display(Name = "A Rh -")]
        ArhN,
        [Display(Name = "B Rh +")]
        BrhP,
        [Display(Name = "B Rh -")]
        BrhN,
        [Display(Name = "0 Rh +")]
        SrhP,
        [Display(Name = "0 Rh -")]
        SrhN,
        [Display(Name = "AB Rh +")]
        ABrhP,
        [Display(Name = "AB Rh -")]
        ABrhN
    }
}
