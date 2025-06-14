using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core.Entities;

namespace MSESG.CargoCare.Core.EFServices.Dto
{
  public class OrdenDto
  {
    public int OrdenId { get; set; }
    public int? Correlativo { get; set; }
    public DateTime Fecha { get; set; }
    public string Referncia { get; set; }
    public int? PlanificacionId { get; set; }
    public int ChoferId { get; set; }
    public string ChoferNombre { get; set; }
    public string FichaCamion { get; set; }
    public string FichaCisterna { get; set; }
    public string SecuenciaSellos { get; set; }
    public string NoCargaRefineria { get; set; }
    public string Referencia { get; set; }
    public IEnumerable<OrdenDetalle> Detalle { get; set; }
    public int? Planificacion { get; set; }
    public bool ShowDetail { get; set; } = false;
    public string Terminal { get; set; }
    public string ClienteNombre { get;  set; }
    public string EmpresaNombre { get;  set; }
    public string ClienteSlug { get;  set; }
    public string EmpresaSlug { get;  set; }
    public int InspeccionId { get; set; }
    public DateTime? FechaInicio { get;  set; }
    public DateTime? FechaFin { get;  set; }
  }
}
