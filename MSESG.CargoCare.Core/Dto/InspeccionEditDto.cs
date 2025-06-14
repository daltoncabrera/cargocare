using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.EFServices
{
  public class InspeccionEditDto
  {
    public Inspeccion Inspeccion { get; set; }
    public IEnumerable<InspeccionDetalle> Detalle { get; set; }
    public bool HasBocaCarga { get; set; } = true;
    public bool HasChapa { get; set; } = true;
    public bool HasBocaDescarga { get; set; } = true;
  }
}
