using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MSESG.CargoCare.Core.EFServices
{
    public class ApplicationUserClaim : IdentityUserClaim<int>
    {
        public ApplicationUserClaim() : base()
        {

        }
    }
}
