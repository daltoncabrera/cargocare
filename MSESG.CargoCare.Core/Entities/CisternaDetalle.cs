using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.Entities
{
    public class CisternaDetalle : Updateable
    {
        public int CisternaId { get; set; }
        public int Compartimento { get; set; }
        public string MedidaReferencia { get; set; }
        public string Medida { get; set; }
        public string Unidad { get; set; }
        public decimal Capacidad { get; set; }
        public bool HasChapa { get; set; } = true;
        public bool HasBoca { get; set; } = true;
        public bool HasDescarga { get; set; } = true;

    }
}
