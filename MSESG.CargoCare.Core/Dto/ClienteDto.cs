using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.EFServices
{
    public class ClienteDto
    {
        public Cliente Cliente { get; set; }
        public Empresa Empresa { get; set; }
        public int Hora { get; set; }
        public int Minutos { get; set; }  
    }
}
