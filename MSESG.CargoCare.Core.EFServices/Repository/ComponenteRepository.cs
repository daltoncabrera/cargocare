 using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core;

namespace MSESG.CargoCare.Core.EFServices
{
    public class ComponenteRepository : Repository<Componente>
    {
        public ComponenteRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
