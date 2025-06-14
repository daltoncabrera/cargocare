using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MSESG.CargoCare.Core;

namespace MSESG.CargoCare.Core.EFServices
{
  public class EmpresaRepository : Repository<Empresa>
  {
    private ApplicationDbContext _ctx;
    public EmpresaRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
      _ctx = dbContext;
    }

    public Empresa GetById(int id)
    {
      return All.FirstOrDefault(s => s.Id == id);
    }

    public EmpresaSetting GetSettings(int id)
    {
      return _ctx.EmpresaSettings.FirstOrDefault(s => s.EmpresaId == id);
    }

    public void SaveSettings(EmpresaSetting settings)
    {
      var entSetting = _ctx.EmpresaSettings.AsNoTracking().FirstOrDefault(s => s.EmpresaId == settings.EmpresaId);
      if (entSetting == null)
      {
        _ctx.EmpresaSettings.Add(settings);
      }
      else
      {
        _ctx.EmpresaSettings.Update(settings);
      }

      _ctx.SaveChanges();
    }

    public bool IsValidEmpresa(int id)
    {
      return id > 0 && _ctx.Empresas.Any(s => s.Id == id);
    }
  }
}
