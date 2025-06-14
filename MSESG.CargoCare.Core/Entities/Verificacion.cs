using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core
{
  public class Verificacion : Updateable
  {
   
    public int? InspectorId { get; set; }
    public DateTime? Fecha { get; set; }
    public int? CamionId { get; set; }
    public int? ChoferId { get; set; }
    public int? CisternaId { get; set; }
    
  }
}
