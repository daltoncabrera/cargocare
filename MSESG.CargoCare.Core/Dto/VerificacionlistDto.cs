using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.Dto
{
    public class VerificacionlistDto
    {
      public int? InspectorId { get; set; }
      public string? Inspector { get; set; }
      public DateTime? Fecha { get; set; }
      public int? CamionId { get; set; }
      public string? Camion { get; set; }
      public int? ChoferId { get; set; }
      public string? Chofer { get; set; }
      public int? CisternaId { get; set; }
      public string? Cisterna { get; set; }
    public int Id { get; set; }
  }
}
