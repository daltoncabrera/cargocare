using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.Entities
{
  public class Observacion : Updateable
  {
    public int? ActividadId { get; set; }
    public DateTime? Fecha { get; set; }
    public int? InspectorId { get; set; }
    public ActividadType Type { get; set; }
  }
}
