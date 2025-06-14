 using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core;

namespace MSESG.CargoCare.Core.EFServices
{
    public class UnidadMedidaRepository : Repository<UnidadMedida>
    {
        public UnidadMedidaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
