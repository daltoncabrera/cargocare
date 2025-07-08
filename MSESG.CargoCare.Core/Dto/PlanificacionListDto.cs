using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.EFServices.Dto
{
    public class PlanificacionListDto
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string? Referencia { get; set; }
        public string? Nota { get; set; }
        public int CamionesQty { get; set; }
        public int OrdenesQty { get; set; }
        public int DestionQty { get; set; }
        public int DespachoQty { get; set; }
    }
}
