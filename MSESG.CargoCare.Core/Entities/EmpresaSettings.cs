using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core
{
  public class EmpresaSetting : Updateable
  {
 
    public String EmailDomain { get; set; }

    public int EmailPort { get; set; }
    
    public String UserEmail { get; set; }

    public String EmailPassword { get; set; }

    public String FromEmail { get; set; }
    public String CcEmail { get; set; }
    public String CcoEmail { get; set; }
  }
}
