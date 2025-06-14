using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.Entities
{
  public class Correo: Updateable
  {
    public string Email { get; set; }
    public string Nombre { get; set; }
    public bool NotificaPlanificacion { get; set; } = false;
    public bool NotificaOrden { get; set; } = false;
    public bool NotificaInspeccion { get; set; } = false;
  }
}
