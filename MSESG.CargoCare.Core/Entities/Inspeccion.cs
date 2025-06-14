using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core
{
  public class Inspeccion : Updateable
  {

    public string Codigo { get; set; }

    public int InspectorId { get; set; }
    public int? PlanificacionId { get; set; }
    public int? Planificacion { get; set; }

    public int? ChoferId { get; set; }

    public int? CamionId { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public string Referencia { get; set; }

    public int? TipoInspeccionId { get; set; }

    public string UnidadId { get; set; }
    public int? CisternaId { get; set; }
    public string NoCargaRefineria { get; set; }
    public int Compartimentos { get; set; }
    public int? TerminalId { get; set; }
    public int? PrecargaId { get; set; }
    public int? TerminalDestinoId { get; set; }
    public int? Precarga { get; set; }
    public int? ProductoId { get; set; }
    public decimal? Cantidad { get; set; }
    public string CertificadoCalidad { get; set; }
    public decimal? Temperatura { get; set; }
    public decimal? TemperaturaMuestra { get; set; }
    public string ConduceTpl { get; set; }
    public bool LlenaDetalle { get; set; }
    public decimal? TotalCargado { get; set; }
  }
}
