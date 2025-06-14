using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.EFServices.Dto
{
  public class ClienteTerminalDto
  {
    public int? TerminalOrd { get; set; }
    public int? TerminalId { get; set; }
    public int? ClienteId { get; set; }
    public string Nombre { get; set; }
    public int? Total { get; set; }
  }
}
