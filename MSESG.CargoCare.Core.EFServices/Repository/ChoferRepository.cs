 using System;
using System.Collections.Generic;
 using System.Linq;
 using System.Text;
using MSESG.CargoCare.Core;

namespace MSESG.CargoCare.Core.EFServices
{
    public class ChoferRepository : Repository<Chofer>
    {
        public ChoferRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Chofer> GetByCliente(int clienteId)
        {
            return All.Where(s => s.ClienteId == clienteId);
        }
    }
}
