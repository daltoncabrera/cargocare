using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.Entities
{
    public class PrecargaDetalle : Updateable
    {
        public int PrecargaId { get; set; }
        public int? CisternaDetalleId { get; set; }
        public int? Compartimento { get; set; }

        public int? ProductoId { get; set; }
        public string Producto { get; set; }


        public decimal? Cantidad { get; set; }
        public decimal? Compartimento1 { get; set; }
        public decimal? Compartimento2 { get; set; }
        public decimal? Compartimento3 { get; set; }
        public decimal? Compartimento4 { get; set; }
        public decimal? Compartimento5 { get; set; }
        public decimal? Compartimento6 { get; set; }
    }
}
