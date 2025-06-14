using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core.Entities;

namespace MSESG.CargoCare.Core.EFServices
{
    public class PrecargaEditDto
    {
        public Precarga Precarga { get; set; }
        public IEnumerable<PrecargaDetalle> Detalle { get; set; }
    }
}
