using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core.Entities;

namespace MSESG.CargoCare.Core.EFServices
{
    public class OrdenEditDto
    {
        public Orden Orden { get; set; }
        public IEnumerable<OrdenDetalle> Detalle { get; set; }
    }
}
