using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core.Entities;

namespace MSESG.CargoCare.Core.EFServices.Dto
{
  public class ConduceDTO
  {
    public InspeccionDto Inspeccion{get; set;}
    public IEnumerable<DetallePorCompartimento> DetalleProducto { get; set; }
    public IEnumerable<InspeccionDetalleDto> InspeccionDetalle { get; set; }
  }
  public class InspeccionDto
  {
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public int? Correlativo { get; set; }
    public int? OrdenId { get; set; }
    public int? OrdenCorrelativo { get; set; }
    public string?Referncia { get; set; }
    public int? PlanificacionId { get; set; }
    public int? Planificacion { get; set; }
    public int Compartientos { get; set; }
    public int? EmpresaId { get; set; }
    public string?EmpresaNombre { get; set; }
    public int? ClienteId { get; set; }
    public string?ClienteNombre { get; set; }
    public int InspectorId { get; set; }
    public string?InspectorNombre { get; set; }
    public int TerminalId { get; set; }
    public string?Terminal { get; set; }
    public int? ChoferId { get; set; }
    public string?ChoferNombre { get; set; }
    public string?ChoferCedula { get; set; }
    public int? CamionId { get; set; }
    public string?PlacaCamion { get; set; }
    public string?FichaCamion { get; set; }
    public int? CisternaId { get; set; }
    public string?PlacaCisterna { get; set; }
    public string?FichaCisterna { get; set; }
    public string?SecuenciaSellos { get; set; }
    public string?NoCargaRefineria { get; set; }
    public string?Referencia { get; set; }
    public bool ShowDetail { get; set; } = false;
    public int Horas { get; set; }
    public int Minutos { get; set; }

    public IEnumerable<OrdenDetalle> Detalle { get; set; }
    public string?Nota { get; set; }
    public int? NoOrden { get; set; }
    public string?Destino { get; set; }
    public string?TipoProducto { get; set; }
    public decimal? Temperatura { get; set; }
    public string?CertificadoCalidad { get; set; }
    public string?TipoConsuce { get; set; }
    public decimal? Capacidad { get; set; }
    public decimal? Cantidad { get; set; }
    public string?ConduceCaption { get; set; }
    public string?NoConduce { get; set; }
  }
}
