using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MSESG.CargoCare.Core.EFServices
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser()
        {

        }

        public string FullName { get; set; }
        public  bool Activo { get; set; }
        public bool HasToChangePassword { get; set; }
    }
}
