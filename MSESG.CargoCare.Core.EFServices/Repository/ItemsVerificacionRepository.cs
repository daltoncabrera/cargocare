 using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core;

namespace MSESG.CargoCare.Core.EFServices
{
    public class ItemsVerificacionRepository : Repository<ItemsVerificacion>
    {
        public ItemsVerificacionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
