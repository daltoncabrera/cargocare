using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.Entities
{
  public enum ActividadType
  {
    Diaria = 1,
    Semanal = 2,
    Mensual = 3
  }
  public class Actividad : Updateable
  {
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public int? InspectorId { get; set; }
    public ActividadType Type { get; set; }
    public bool Disponible { get; set; } = false;
  }
}
