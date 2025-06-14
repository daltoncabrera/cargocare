using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.EFServices.Dto
{
    public class SellosLoteDto
    {
        public int Lote { get; set; }
        public int Inicial { get; set; }
        public int Final { get; set; }
        public int Espacios { get; set; } = 6;
    }
}
