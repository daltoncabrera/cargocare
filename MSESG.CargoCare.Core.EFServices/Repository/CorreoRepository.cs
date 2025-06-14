using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.Entities;

namespace MSESG.CargoCare.Core.EFServices
{
  public class CorreoRepository : Repository<Correo>
  {
    public CorreoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public void SaveCorreo(Correo correo)
    {
      if (correo.Id > 0)
      {
        Update(correo);
      }
      else
      {
        Insert(correo);
      }

      _context.SaveChanges();
    }

    public IEnumerable<Correo> GetByCliente(int clienteId)
    {
      return All.Where(s => s.ClienteId == clienteId);
    }

    public Correo GetByEmail(string email)
    {
      return All.AsNoTracking().FirstOrDefault(s => s.Email == email);
    }
  }
}
