 using System;
using System.Collections.Generic;
 using System.Linq;
 using System.Text;
using MSESG.CargoCare.Core;

namespace MSESG.CargoCare.Core.EFServices
{
    public class CamionRepository : Repository<Camion>
    {
        public CamionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Camion> GetByCliente(int clienteId)
        {
            return All.Where(s => s.ClienteId == clienteId);
        }
    }
}
