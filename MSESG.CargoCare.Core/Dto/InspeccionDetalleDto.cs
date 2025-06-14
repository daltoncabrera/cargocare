using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core.Entities;

namespace MSESG.CargoCare.Core.EFServices.Dto
{
    public class InspeccionDetalleDto
    {
    public int IdInspeccion { get; set; }

      public int? CompartimentoId { get; set; }

      public int? ProductoId { get; set; }

      public string Producto { get; set; }

      public int UnidadId { get; set; }

      public string Unidad { get; set; }

      public decimal? Capacidad { get; set; }

      public decimal? Cantidad { get; set; }

      public decimal? CantidadDespachada { get; set; }


      public int? SelloChapaManholeId { get; set; }
      public string SelloChapaManhole { get; set; }

      public int? SelloBocaCargaId { get; set; }
      public string SelloBocaCarga { get; set; }

      public int? SelloBocaDescargaId { get; set; }
      public string SelloBocaDescarga { get; set; }
      public bool EnUso { get; set; }

  }

  public class DetallePorCompartimento
  {
    public string Producto { get; set; }
    public decimal? Compartiento1 { get; set; }
    public decimal? Compartiento2 { get; set; }
    public decimal? Compartiento3 { get; set; }
    public decimal? Compartiento4 { get; set; }
    public decimal? SubTotal { get; set; }
  }
}
