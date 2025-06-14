using System;
using System.Collections.Generic;
using System.Linq;

namespace MSESG.CargoCare.Core
{
  public interface IMyContext : IDisposable
  {
    int SaveChanges();
  }
}