 using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core;

namespace MSESG.CargoCare.Core.EFServices
{
    public class PermisoRepository : Repository<Permiso>
    {
        public PermisoRepository(IUnitOfWork uow) : base(uow)
        {
        }
    }
}
