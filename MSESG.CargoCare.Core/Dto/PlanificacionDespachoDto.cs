using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core.Entities;

namespace MSESG.CargoCare.Core.EFServices.Dto
{
  public class PlanificacionDespachoDto              
  {
    public PlanificacionDespacho Despacho { get; set; }
    public IEnumerable<PlanificacionDetalleDto> PlanificacionDetalle { get; set; }
  }
  
}
