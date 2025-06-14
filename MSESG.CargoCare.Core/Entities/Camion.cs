using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core
{
    public class Camion : Updateable
    {

        public string? Marca { get; set; }

        public string? Modelo { get; set; }

        public string? Placa { get; set; }

        public string? Ficha { get; set; }

        public int ChoferId { get; set; }
        public int CisternaId { get; set; }

    }
}
