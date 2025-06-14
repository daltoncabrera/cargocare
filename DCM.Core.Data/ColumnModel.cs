using System;

namespace DCM.Core.Data
{
  public class ColumnModel
  {
    public string Nombre { get; set; }

    public Type Tipo { get; set; }

    public int Capacidad { get; set; }

    public bool IsPrimary { get; set; }

    public bool AllowNull { get; set; }
  }
}
