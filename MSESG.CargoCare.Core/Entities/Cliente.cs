using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSESG.CargoCare.Core
{
  public class Cliente : Updateable
  {

    public string Nombre { get; set; }

    public string Slug { get; set; }

    public string Telefono { get; set; }

    public string Fax { get; set; }

    public string Celular { get; set; }

    public string Email { get; set; }

    public string Direccion { get; set; }

    public string Logo { get; set; }

    public string Key { get; set; }
    public string NotaPieConduce { get; set; }
    public string NotaPieConduce2 { get; set; }
    public string NotaLateralConduce { get; set; }

    public string Contacto { get; set; }
    public DateTime? InicioLabor { get; set; }
    public DateTime? FinLabor { get; set; }
    public bool Sabados { get; set; }
    public bool Domingos { get; set; }
    public string Correos { get; set; }
    public string ConduceTpl { get; set; }
    public bool LlenaDetalle  { get; set; } = true;

  }
}
