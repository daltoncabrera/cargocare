using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core.Dto
{
    public class ClienteProductoDto
    {
      public int Id { get; set; }
      public int ProductoId { get; set; }
      public int ClienteId { get; set; }
      public string Producto { get; set; }
    public string CodigoProducto { get; set; }
  }
}
