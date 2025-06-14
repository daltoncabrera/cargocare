 using System;
using System.Collections.Generic;
using System.Text;
 using Microsoft.EntityFrameworkCore;
 using MSESG.CargoCare.Core;

namespace MSESG.CargoCare.Core.EFServices
{
    public class UserRoleRepository : Repository<ApplicationUserRole>
    {
        public UserRoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
