using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSESG.CargoCare.Core;

namespace MSESG.CargoCare.Core.EFServices
{
  public class ClienteRepository : Repository<Cliente>
  {
    private ApplicationDbContext _dbContext;
    public ClienteRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
      _dbContext = dbContext;
    }

    public Cliente GetById(int id)
    {
      return All.FirstOrDefault(s => s.Id == id);
    }

    public IEnumerable<ClienteDto> GetClientes()
    {
      var result = from c in All
                   join e in _dbContext.Empresas on c.EmpresaId equals e.Id
                   select new ClienteDto
                   {
                     Cliente = new Cliente()
                     {
                       Nombre = c.Nombre,
                       Id = c.Id,
                       Contacto = c.Contacto,
                       Correos = c.Correos,
                       Telefono = c.Telefono,
                       Slug = c.Slug,
                       Logo = string.IsNullOrWhiteSpace(c.Logo) ? "cliente.png" : c.Logo
                     },
                     Empresa = e
                   };

      return result;
    }


    public bool IsClienteTerminal(int clienteId, int terminalId)
    {
      return _dbContext.Terminales.Any(s => s.ClienteId == clienteId && s.Id == terminalId);
    }


  }
}
