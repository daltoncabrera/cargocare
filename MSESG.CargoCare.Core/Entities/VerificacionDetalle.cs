using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSESG.CargoCare.Core
{
  public class VerificacionDetalle 
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public VerificacionDetalle()
    {

    }

    public VerificacionDetalle(string name, string descripcion)
    {
      this.ItemName = name;
      this.ItemDescription = descripcion;
    }
    public int VerificacionId { get; set; }

    public string ItemName { get; set; }

    public string ItemDescription { get; internal set; }

    public bool? Bueno { get; set; }

    public bool? Regular { get; set; }

    public bool? Malo { get; set; }

    public bool? Si { get; set; }

    public bool? No { get; set; }

    public string Observacion { get; set; }
  }
}
