using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MSESG.CargoCare.Core.EFServices
{
    public class ApplicationRole : IdentityRole<int>, IIdentificable
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string name, string description) : base(name)
        {
            this.Description = description;
        }
        public string Description { get; set; }
        public RoleType RoleType { get; set; }
    }
}
