using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core.EFServices.Dto;
using MSESG.CargoCare.Core.Entities;

namespace MSESG.CargoCare.Core.Dto
{
  public class ActividadDto
  {
    public ActividadDto()
    {
      Observaciones = new List<ObservacionDto>();
    }

    public int ClienteId { get; set; }
    public string Cliente { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public Decimal? TotalCamiones { get; set; }
    public List<ActiviadDetalleDto> Detalle { get; set; }
    public List<ResumenDto> Resumen { get; set; }
    public List<ObservacionDto> Observaciones { get; set; }
    public DateTime? DateFilterInit { get; set; }
    public int? ActividadId { get; set; }
    public ActividadType Type { get; set; }
    public string Message { get; set; }
    public bool Disponible { get; set; }
  }


  public class DatesDto
  {
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public int? TotalCamiones { get; set; }
    public Decimal? Acumulado { get; set; }
    public Dictionary<int, ResumenDto> Detalles { get; set; }
    public List<ClienteProductoDto> Terminales { get; set; }
  }

  public class ActividadMonthDto
  {
    public ActividadMonthDto()
    {
      Observaciones = new List<ObservacionDto>();
    }

    public int ClienteId { get; set; }
    public string Cliente { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public DateTime? DateFilterInit { get; set; }
    public int? ActividadId { get; set; }
    public ActividadType Type { get; set; }
    public string Message { get; set; }
    public bool Disponible { get; set; }
    public List<ObservacionDto> Observaciones { get; set; }
    public List<DatesDto> Detalle { get; set; }
    public List<ClienteTerminalDto> Terminales { get; set; }
    public List<ActiviadDetalleDto> Conduces { get; set; }
    public int? TotalCamiones { get; set; }
  }


  public class ResumenDto
  {
    public string TerminalDestino { get; set; }
    public int? CamionesQty { get; set; }
    public int? TerminalDestionId { get; set; }
  }

  public class ObservacionDto
  {
    public int? ActividadId { get; set; }
    public DateTime? Fecha { get; set; }
    public string Nota { get; set; }
    public int? InspectorId { get; set; }
    public string Inspector { get; set; }
    public int? ClienteId { get; set; }
    public int Id { get; set; }
    public int? EmpresaId { get; set; }
    public ActividadType Type { get; set; }
  }

  public class ActiviadDetalleDto
  {
    private DateTime? Fecha { get; set; }
    public int? Id { get; set; }
    public string Referencia { get; set; }
    public int? InspectorId { get; set; }
    public string Inspector { get; set; }
    public int? CamionId { get; set; }
    public string Ficha { get; set; }
    public string PlacaCamion { get; set; }
    public int? CisternaId { get; set; }
    public string PlacaCisterna { get; set; }
    public int? ChoferId { get; set; }
    public string Chofer { get; set; }
    public int? ProductoId { get; set; }
    public string Producto { get; set; }
    public string Certificados { get; set; }

    public decimal? TemperaturaCarga { get; set; }
    public decimal? TemperaturaTomada { get; set; }
    public decimal? TotalCargado { get; set; }

    public string SelloChapa1 { get; set; }
    public string SelloChapa2 { get; set; }
    public string SelloChapa3 { get; set; }
    public string SelloChapa4 { get; set; }
    public string SelloChapa5 { get; set; }

    public string SelloBocaDescarga1 { get; set; }
    public string SelloBocaDescarga2 { get; set; }
    public string SelloBocaDescarga3 { get; set; }
    public string SelloBocaDescarga4 { get; set; }
    public string SelloBocaDescarga5 { get; set; }

    public string SelloBocaCarga1 { get; set; }
    public string SelloBocaCarga2 { get; set; }
    public string SelloBocaCarga3 { get; set; }
    public string SelloBocaCarga4 { get; set; }
    public string SelloBocaCarga5 { get; set; }
    public DateTime? FechaFin { get; set; }
    public DateTime? FechaInicio { get; set; }
    public decimal? TotalCarga { get; set; }
    public int? DestinoId { get; set; }
    public string Destino { get; set; }
  }
}
