using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSESG.CargoCare.Core
{
  public class KeyValue
  {
    public KeyValue()
    {
      
    }

     public KeyValue(object key, string?val)
     {
       Key = key;
       Value = val;
     }

    public object Key { get; set; }
    public string?Value { get; set; }
  }
}
