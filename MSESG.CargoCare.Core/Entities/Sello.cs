using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core
{
    public class Sello : Updateable
    {
        public string CodSello { get; set; }
        public int Lote { get; set; }
        public int IntSello { get; set; }
        public int? InspeccionId { get; set; }

        public int? CompartimentoId { get; set; }

        public string? Ubicacion { get; set; }

        public SelloStatus SelloStatus { get; set; } = SelloStatus.Disponible;
        public DateTime? FechaUsado { get; set; }

    }
}
