using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.Entities
{
    public class OrdenDetalle : Updateable
    {
        public int OrdenId { get; set; }
        public bool EnUso { get; set; } = true;
        public int? CisternaDetalleId { get; set; }
        public int? Compartimento { get; set; }

        public int? ProductoId { get; set; }
        public string Producto { get; set; }

        public int? MedidaId { get; set; }
        public int UnidadId { get; set; }

        public decimal? CantidadConduce { get; set; }

        public decimal? CantidadMedida { get; set; }

        public decimal? CantidadDespachada { get; set; }

        public int? SelloChapaManholeId { get; set; }
        public string SelloChapaManhole { get; set; }

        public int? SelloBocaCargaId { get; set; }
        public string SelloBocaCarga { get; set; }

        public int? SelloBocaDescargaId { get; set; }
        public string SelloBocaDescarga { get; set; }

    }
}
