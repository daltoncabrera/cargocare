using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core
{
  public class Variable : Updateable
  {
    public string?  VarName { get; set; }

    public string?  VarValue { get; set; }
  }
}
