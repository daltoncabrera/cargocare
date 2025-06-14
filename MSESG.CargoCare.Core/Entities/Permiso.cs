using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core
{
  public class Permiso : Updateable
  {
    public int UsuarioId { get; set; }
    public int PermisoType { get; set; }     
  }
}
