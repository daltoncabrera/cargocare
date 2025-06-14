using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core.Entities;

namespace MSESG.CargoCare.Core.EFServices.Dto
{
  public class PlanificacionDetalleDto              
  {
    public PlanificacionDetalle Detalle { get; set; }
    public IEnumerable<PlanificacionDestino> Destinos { get; set; }
  }
  
}
