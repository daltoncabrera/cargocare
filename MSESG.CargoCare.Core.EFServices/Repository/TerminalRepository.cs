using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices.Dto;

namespace MSESG.CargoCare.Core.EFServices
{
  public class TerminalRepository : Repository<Terminal>
  {
    private ApplicationDbContext _applicationDbContext;
    public TerminalRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
      _applicationDbContext = dbContext;
    }

    public IEnumerable<Terminal> GetByCliente(int clienteId)
    {
      var r = from ct in _applicationDbContext.Terminales
              where (!ct.ClienteId.HasValue || ct.ClienteId == clienteId)
               && !ct.EsDestino
              select ct;

      return r;
    }

    public IEnumerable<Terminal> GetByClienteDestinos(int clienteId)
    {
      var r = from ct in _applicationDbContext.Terminales
        where (!ct.ClienteId.HasValue || ct.ClienteId == clienteId)
              && ct.EsDestino
        select ct;

      return r;
    }

    public IEnumerable<Terminal> GetOwnedByCliente(int clienteId)
    {
      var r = _applicationDbContext.Terminales.Where(ct => ct.ClienteId == clienteId).OrderBy(s => s.DestinoOrd).ThenByDescending(s => s.Id);
      return r;
    }

    public IEnumerable<Terminal> GetTerminales()
    {
      return All.Where(s => !s.ClienteId.HasValue || s.ClienteId <= 0);
    }
  }
}
