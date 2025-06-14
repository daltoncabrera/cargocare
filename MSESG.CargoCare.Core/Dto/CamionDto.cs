using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core.Entities;

namespace MSESG.CargoCare.Core.EFServices.Dto
{
    public class CisternaDto
    {
        public Cisterna Cisterna { get; set; }
        public List<CisternaDetalle> Detalle { get; set; }
    }
}
