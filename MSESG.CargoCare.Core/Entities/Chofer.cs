using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core
{
    public class Chofer : Updateable
    {
        #region Primitive Properties

        public string Nombre { get; set; }

        public string Cedula { get; set; }

        public string Licencia { get; set; }

        public string Telefono { get; set; }
        #endregion
    }
}
