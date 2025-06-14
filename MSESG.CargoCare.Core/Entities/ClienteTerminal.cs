using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core
{
  public class ClienteTerminal : Updateable
  {

    public int TerminalId { get; set; }
    public bool EsPlanificacionDestino { get; set; } = false;
    public int Ord { get; set; }
    public string Contacto { get; set; }
    public string ConduceCaption { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
  }
}
