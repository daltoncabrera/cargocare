using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSESG.CargoCare.Core;

namespace MSESG.CargoCare.Core.EFServices
{
  public class SelloRepository : Repository<Sello>
  {
    private ApplicationDbContext _dbContext;
    public SelloRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
      _dbContext = dbContext;
    }

    public void GenerateSellos(int empresaId, int clienteId, int selloLote, int selloInicial, int selloFinal, int selloEspacios, int user, int cliente, int empresa)
    {
      var ctx = _context as ApplicationDbContext;

      if (selloInicial <= 0 || selloFinal <= 0)
        throw new Exception("Sellos Inicial y Final deben ser mayor a 0");

      if (selloFinal < selloInicial)
        throw new Exception("Sello incial debe ser menor");

      var exiten = All.Any(s => s.ClienteId == clienteId
                                && s.Lote == selloLote
                                && s.IntSello >= selloInicial);
      if (exiten)
        throw new Exception("Colision de sellos, intenta ingresar sellos que existen");


      for (int i = selloInicial; i <= selloFinal; i++)
      {
        var sello = new Sello();
        sello.ClienteId = clienteId;
        sello.EmpresaId = empresaId;
        sello.Lote = selloLote;
        sello.IntSello = i;
        sello.SelloStatus = SelloStatus.Disponible;
        sello.CodSello = string.Format("{0:000000}", i);
        sello.CreatedDate = DateTime.Now;
        sello.ModifiedDate = DateTime.Now;
        sello.CreatedUser = user;
        sello.ClienteId = cliente;
        sello.EmpresaId = empresa;

        Insert(sello);
      }

    }

    public IEnumerable<KeyValue> GetLotes(int clienteId)
    {
      var result = _dbContext.Sellos.Where(s => s.ClienteId == clienteId)
          .GroupBy(s => new { s.Lote, s.CreatedDate.Value.Date })
          .Select(s => s.Key);

      return result.Select(s => new KeyValue(s.Lote, string.Format("{0} [{1:dd/MM/yyyy}]", s.Lote, s.Date)));
    }

    public IEnumerable<Sello> GetSellos(int clienteId, FilterModel filter)
    {
      var result = All.Where(s => s.ClienteId == clienteId);

      if (filter != null)
      {
        switch (filter.Column)
        {
          case 1:
            if (!string.IsNullOrWhiteSpace(filter.Filter))
            {
              var lote = Convert.ToInt32(filter.Filter);
              result = result.Where(s => s.Lote == lote);
            }
            break;

        }

        if (filter.Estatus != SelloStatus.None)
        {
          result = result.Where(s => s.SelloStatus == filter.Estatus);
        }

        result = result.OrderBy(s => s.Lote).ThenBy(s => s.IntSello);
        if (filter.Page == 1)
        {
          filter.TotalItems = result.Count();
        }

        result = result.Skip(filter.Skip).Take(filter.PageSize);

      }

      return result;
    }

    public IEnumerable<Sello> GetByClienteSinUsar(int clienteId, int ordenId, string filter = "")
    {

      var sellosEnUso = (from s in _dbContext.Sellos
                         from d in _dbContext.OrdenDetalles
                         where s.ClienteId == clienteId
                               && d.OrdenId == ordenId
                               && s.SelloStatus != SelloStatus.Disponible
                               && (d.SelloChapaManholeId == s.Id
                                   || d.SelloBocaCargaId == s.Id
                                   || d.SelloBocaDescargaId == s.Id)
                         select s.Id).ToList();


      var result = !string.IsNullOrWhiteSpace(filter)
      ? _dbContext.Sellos
          .Where(s => s.ClienteId == clienteId
                      && (sellosEnUso.Contains(s.Id) || s.SelloStatus == SelloStatus.Disponible)
                      && (s.CodSello.Contains(filter)))

      : _dbContext.Sellos
          .Where(s => s.ClienteId == clienteId && (sellosEnUso.Contains(s.Id) || s.SelloStatus == SelloStatus.Disponible));


      return result;
    }

    public IEnumerable<Sello> GetByClienteSinUsar(int clienteId)
    {
      return All.Where(s => s.ClienteId == clienteId && s.SelloStatus == SelloStatus.Disponible);
    }

    public void DeleteLote(int id, int cliente)
    {
      var loteSellos = _dbContext.Sellos.Where(s => s.ClienteId == cliente && s.Lote == id);


      if (loteSellos.Any(s => s.SelloStatus != SelloStatus.Disponible))
        throw new Exception("Lote tiene sellos en uso");


      _dbContext.Sellos.RemoveRange(loteSellos);

      _dbContext.SaveChanges();

    }

    public Sello GetSelloById(int id)
    {
      return All.FirstOrDefault(s => s.Id == id);
    }
  }
}
