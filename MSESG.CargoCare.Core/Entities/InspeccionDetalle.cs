using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core
{
  public class InspeccionDetalle : Updateable
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
    public bool EnUso { get; set; } = true;
    public bool? HasBocaCarga { get; set; } = false;
    public bool? HasChapa { get; set; } = false;
    public bool? HasBocaDescarga { get; set; } = false;
  }
}
