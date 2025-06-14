using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSESG.CargoCare.Core
{
    public class Empresa : Updateable
    {
        public string Nombre { get; set; }
        public string Slug { get; set; }

        public string Logo { get; set; }
        public string RNC { get; set; }
        public string Mail { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
  
    }
}
