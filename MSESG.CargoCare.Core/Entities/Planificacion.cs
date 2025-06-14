using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.Entities
{
  public class Planificacion : Updateable
  {
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public string Referencia { get; set; }
  }


  public class PlanificacionDespacho : Updateable
  {
    public int PlanificacionId { get; set; }
    public int TerminalId { get; set; }

  }

  public class PlanificacionDetalle : Updateable
  {
    public int PlanificacionId { get; set; }
    public int PlanificacionDespachoId { get; set; }
    public bool Activo { get; set; }
    public DateTime Fecha { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int CamionesQty { get; set; }

  }
  public class PlanificacionDestino : Updateable
  {
    public int PlanificacionId { get; set; }
    public int PlanificacionDespachoId { get; set; }
    public int PlanificacionDetalleId { get; set; }
    public int TerminalId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int CamionesQty { get; set; }


  }
}
