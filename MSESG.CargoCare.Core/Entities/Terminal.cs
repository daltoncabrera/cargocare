using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core
{
  public class Terminal : Updateable
  {
    public bool MultipleInstances { get; set; } = false;
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string NombreContacto { get; set; }
    public string ConduceCaption { get; set; }
    public string Correo { get; set; }
    public string Telefono { get; set; }
    public string Direccion { get; set; }
    public string MapLink { get; set; }
    public bool EsDestino { get; set; } = false;
    public int DestinoOrd { get; set; }
  }
}
