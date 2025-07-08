using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core.Entities;

namespace MSESG.CargoCare.Core.EFServices.Dto
{
  public class PlanificacionDto
  {
    public PlanificacionDto()
    {
      PlanificacionDespacho = new List<PlanificacionDespachoDto>();
    }

    public Planificacion Planificacion { get; set; }
    public IEnumerable<PlanificacionDespachoDto> PlanificacionDespacho { get; set; }
    public string?Empresa { get; set; }
    public string?Cliente { get; set; }
    public IEnumerable<KeyValue> Terminales { get; set; }
    public IEnumerable<KeyValue> Destinos { get; set; }
     
  }
}
