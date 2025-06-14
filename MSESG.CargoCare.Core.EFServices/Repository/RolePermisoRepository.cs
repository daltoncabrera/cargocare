using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.EFServices
{
    public class RolePermisoRepository : Repository<RolePermiso>
    {
        public RolePermisoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
