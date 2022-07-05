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
    public class Admin : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email /*{ get { return FirstName + "." + LastName + "@" + "admin" + "." + "com"; } }*/{ get; set; }

        public string Password { get; set; } 
        public string PhoneNumber { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public string ProfilePictureName { get; set; }
        [NotMapped]
        public IFormFile ProfilePictureFile { get; set; }
        
        
    }
}
