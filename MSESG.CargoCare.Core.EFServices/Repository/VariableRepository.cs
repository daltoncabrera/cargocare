using System;
 using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core;

namespace MSESG.CargoCare.Core.EFServices
{
    public class VariableRepository : Repository<Variable>
    {
        public VariableRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
