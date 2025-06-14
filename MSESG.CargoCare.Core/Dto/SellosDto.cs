using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.EFServices.Dto
{
    public class SellosDto
    {
        public int Id { get; set; }
        public string CodSello { get; set; }
        public int Lote { get; set; }
        public int InspeccionId { get; set; }
        public int CompartimentoId { get; set; }
        public string Ubicacion { get; set; }
        public bool Usado { get; set; } = false;
        public DateTime? FechaUsado { get; set; }
        public string SelloStatusId { get; set; }
        public string SelloStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
