using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core.EFServices.Dto;
using MSESG.CargoCare.Core.Entities;

namespace MSESG.CargoCare.Core.Dto
{
    public class PrecargaReportDto
    {
      public PrecargaDto Precarga { get; set; }
      public IEnumerable<PrecargaDetalle> Detalle { get; set; }
    }
}
