 using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core;

namespace MSESG.CargoCare.Core.EFServices
{
    public class InspeccionDetalleRepository : Repository<InspeccionDetalle>
    {
        public InspeccionDetalleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
