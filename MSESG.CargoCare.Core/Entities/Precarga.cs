using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.Entities
{
  public class Precarga : Updateable
  {
    public int? PlanificacionId { get; set; }
    public int? Planificacion { get; set; }
    public int? TerminalId { get; set; }
    public int? TerminalDestinoId { get; set; }
    public DateTime Fecha { get; set; }
    public DateTime? HoraEntrada { get; set; }
    public DateTime? HoraSalida { get; set; }
    public int? CamionId { get; set; }
    public int? ChoferId { get; set; }
    public int? CisternaId { get; set; }
    public string?Referencia { get; set; }
    public int? ProductoId { get; set; }
    public decimal? Cantidad { get; set; }
    public string?NoCargaRefineria { get; set; }
    public string?NumeroPrecarga { get; set; }
    public string?ConduceTpl { get; set; }
    public bool LlenaDetalle { get; set; }
  }
}
