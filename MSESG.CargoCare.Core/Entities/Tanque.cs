using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.Entities
{
    public class TanqueSisterna
    {
        public string?Ficha { get; set; }

        public int? Compartimentos { get; set; }

        public decimal? Capacidad { get; set; }

        public int? Entradas { get; set; }

        public int? Salidas { get; set; }

    }
}
