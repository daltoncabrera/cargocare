using System;
using System.Collections.Generic;
using System.Linq;

namespace MSESG.CargoCare.Core
{
  public interface IUnitOfWork : IDisposable
  {
    int Save();
    IMyContext MyContext { get; }
  }
}