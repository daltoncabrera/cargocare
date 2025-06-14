using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.Dto;
using MSESG.CargoCare.Core.EFServices.Dto;
using MSESG.CargoCare.Core.Entities;

namespace MSESG.CargoCare.Core.EFServices
{
  public class ProductoRepository : Repository<Producto>
  {
    private ApplicationDbContext _dbContext;
    public ProductoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
      _dbContext = dbContext;
    }

    public IEnumerable<ClienteProductoDto> GetByCliente(int clienteId)
    {
      var result = from pc in _dbContext.ProductosClientes
                   join p in _dbContext.Productos on pc.ProductoId equals p.Id
                   join c in _dbContext.Clientes on pc.ClienteId equals c.Id
                   where pc.ClienteId == clienteId
                   select new ClienteProductoDto()
                   {
                     Id = pc.Id,
                     ClienteId = clienteId,
                     ProductoId = p.Id,
                     Producto = p.Nombre,
                     CodigoProducto = pc.CodigoProducto
                   };

      return result;
    }

    public IEnumerable<Producto> GetProductos()
    {
      return All;
    }

    public void AddToCliente(ClienteProductoDto model)
    {
      var clienteProducto = _dbContext.ProductosClientes.FirstOrDefault(s =>
        s.ClienteId == model.ClienteId && s.ProductoId == model.ProductoId);
      if (clienteProducto == null)
      {
        clienteProducto = new ProductosCliente();
        clienteProducto.ProductoId = model.ProductoId;
        clienteProducto.ClienteId = model.ClienteId;
        clienteProducto.CodigoProducto = model.CodigoProducto;
        _dbContext.ProductosClientes.Add(clienteProducto);
      }
      else
      {
        clienteProducto.CodigoProducto = model.CodigoProducto;
        _dbContext.ProductosClientes.Update(clienteProducto);
      }

      _dbContext.SaveChanges();

    }

    public void RemoveFromCliente(ClienteProductoDto model)
    {
      var curClienteProducto = _dbContext.ProductosClientes.FirstOrDefault(s => s.ClienteId == model.ClienteId && s.ProductoId == model.ProductoId);
      if (curClienteProducto != null)
      {
        _dbContext.ProductosClientes.Remove(curClienteProducto);
        _dbContext.SaveChanges();
      }
    }

    public object GetReport(int? clienteId, DateTime? dateInit, DateTime? dateEnd, string title = "")
    {
      var prod = from d in _dbContext.InspeccionDetalles
                 join i in _dbContext.Inspeciones on d.IdInspeccion equals i.Id
                 join p in _dbContext.Productos on d.ProductoId equals p.Id
                 join t in _dbContext.Terminales on i.TerminalId equals t.Id
                 join des in _dbContext.Terminales on i.TerminalDestinoId equals des.Id
                 where i.FechaInicio.Value.Date >= dateInit.Value.Date
                       && i.FechaInicio.Value.Date <= dateEnd.Value.Date
                       && i.ClienteId == clienteId
                 select new { i, d, p, t, des };


      var result = prod.GroupBy(s => new { Fecha = s.i.FechaInicio.Value.Date, Producto = s.p.Nombre, Terminal = s.t.Nombre, Destino = s.des.Nombre })
        .OrderBy(s => s.Key.Fecha)
        .Select(s => new
        {
          Producto = s.Key.Producto,
          Fecha = s.Key.Fecha,
          Cantidad = s.Sum(d => d.d.Cantidad),
          Terminal = s.Key.Terminal,
          Destino = s.Key.Destino
        });

      var header = new
      {
        FechaInicio = dateInit.HasValue ? $"{dateInit.Value:dd/MM/yyyy}." : "",
        FechaFin = dateEnd.HasValue ? $"{dateEnd.Value:dd/MM/yyyy}." : "",
        Cliente = "Cliente xxx",
        Title = title
      };
      return new { header, detail = result };
    }

    public object GetReportMes(int? clienteId, DateTime? dateInit, DateTime? dateEnd, string title = "")
    {
      var prod = from d in _dbContext.InspeccionDetalles
                 join i in _dbContext.Inspeciones on d.IdInspeccion equals i.Id
                 join p in _dbContext.Productos on d.ProductoId equals p.Id
                 join t in _dbContext.Terminales on i.TerminalId equals t.Id
                 join des in _dbContext.Terminales on i.TerminalDestinoId equals des.Id
                 where i.FechaInicio.Value.Date >= dateInit.Value.Date
                       && i.FechaInicio.Value.Date <= dateEnd.Value.Date
                       && i.ClienteId == clienteId
                 select new { i, d, p, t, des };


      var result = prod.GroupBy(s => new { Fecha =s.i.FechaInicio.Value.ToString("MMMM", new CultureInfo("es-MX")), Producto = s.p.Nombre, Terminal = s.t.Nombre, Destino = s.des.Nombre })
        .Select(s => new
        {
          Producto = s.Key.Producto,
          Fecha = s.Key.Fecha,
          Cantidad = s.Sum(d => d.d.Cantidad),
          Terminal = s.Key.Terminal,
          Destino = s.Key.Destino
        });

      var header = new
      {
        FechaInicio = dateInit.HasValue ? $"{dateInit.Value:dd/MM/yyyy}." : "",
        FechaFin = dateEnd.HasValue ? $"{dateEnd.Value:dd/MM/yyyy}." : "",
        Cliente = "Cliente xxx",
        Title = title
      };
      return new { header, detail = result };
    }

    public object GetReportAnio(int? clienteId, DateTime? dateInit, DateTime? dateEnd, string title = "")
    {
      var prod = from d in _dbContext.InspeccionDetalles
                 join i in _dbContext.Inspeciones on d.IdInspeccion equals i.Id
                 join p in _dbContext.Productos on d.ProductoId equals p.Id
                 join t in _dbContext.Terminales on i.TerminalId equals t.Id
                 join des in _dbContext.Terminales on i.TerminalDestinoId equals des.Id
                 where i.FechaInicio.Value.Date >= dateInit.Value.Date
                       && i.FechaInicio.Value.Date <= dateEnd.Value.Date
                       && i.ClienteId == clienteId
                 select new { i, d, p, t, des };


      var result = prod.GroupBy(s => new { Fecha = $"{s.i.FechaInicio.Value:yyyy}.", Producto = s.p.Nombre, Terminal = s.t.Nombre, Destino = s.des.Nombre })
        .Select(s => new
        {
          Producto = s.Key.Producto,
          Fecha = s.Key.Fecha,
          Cantidad = s.Sum(d => d.d.Cantidad),
          Terminal = s.Key.Terminal,
          Destino = s.Key.Destino
        });

      var header = new
      {
        FechaInicio = dateInit.HasValue ? $"{dateInit.Value:dd/MM/yyyy}." : "",
        FechaFin = dateEnd.HasValue ? $"{dateEnd.Value:dd/MM/yyyy}." : "",
        Cliente = "Cliente xxx",
        Title = title
      };
      return new { header, detail = result };
    }

    public List<ChartReportDto> GetReportGrafico(int clienteId, DateTime? dateInit, DateTime? dateEnd)
    {
      var listDto = new List<ChartReportDto>();

      var prod = from d in _dbContext.InspeccionDetalles
                 join i in _dbContext.Inspeciones on d.IdInspeccion equals i.Id
                 join p in _dbContext.Productos on d.ProductoId equals p.Id
                 join t in _dbContext.Terminales on i.TerminalId equals t.Id
                 join des in _dbContext.Terminales on i.TerminalDestinoId equals des.Id
                 where i.FechaInicio.Value.Date >= dateInit.Value.Date
                       && i.FechaInicio.Value.Date <= dateEnd.Value.Date
                       && i.ClienteId == clienteId
                 select new { i, d, p, t, des };


      var result = prod.GroupBy(s => new { Fecha = s.i.FechaInicio.Value.Date, Producto = s.p.Nombre, })
        .Select(s => new
        {
          Producto = s.Key.Producto,
          Fecha = s.Key.Fecha,
          Cantidad = s.Sum(d => d.d.Cantidad),
        });

      var rresult = result.GroupBy(s => s.Producto);
      var fechas = result.Select(s => s.Fecha).Distinct();
      foreach (var r in rresult)
      {
        var curDto = new ChartReportDto();
        curDto.Label = r.Key;
        var detail = r.GroupBy(s => new { s.Fecha, s.Cantidad });
        foreach (var d in fechas)
        {
          var cantidad = detail.Where(s => s.Key.Fecha == d).Sum(s => s.Key.Cantidad);
          curDto.Arguments.Add(d.ToString("dd/MM/yyyy"));
          curDto.Values.Add(cantidad.ToString());
        }

        listDto.Add(curDto);
      }


      return listDto;
    }

    public List<ChartReportDto> GetReportGraficoMes(int clienteId, DateTime? dateInit, DateTime? dateEnd)
    {
      var listDto = new List<ChartReportDto>();

      var prod = from d in _dbContext.InspeccionDetalles
                 join i in _dbContext.Inspeciones on d.IdInspeccion equals i.Id
                 join p in _dbContext.Productos on d.ProductoId equals p.Id
                 join t in _dbContext.Terminales on i.TerminalId equals t.Id
                 join des in _dbContext.Terminales on i.TerminalDestinoId equals des.Id
                 where i.FechaInicio.Value.Date >= dateInit.Value.Date
                       && i.FechaInicio.Value.Date <= dateEnd.Value.Date
                       && i.ClienteId == clienteId
                 select new { i, d, p, t, des };


      var result = prod.GroupBy(s => new { Fecha = s.i.FechaInicio.Value.Date, Producto = s.p.Nombre })
        .Select(s => new
        {
          Producto = s.Key.Producto,
          Fecha = s.Key.Fecha.Date,
          Cantidad = s.Sum(d => d.d.Cantidad),
        });

      var rresult = result.GroupBy(s => s.Producto);
      var fechas = result.Select(s => s.Fecha.Date).Select(s => s.ToString("MMMM", new CultureInfo("es-MX"))).Distinct();
      foreach (var r in rresult)
      {
        var curDto = new ChartReportDto();
        curDto.Label = r.Key;
        var detail = r.GroupBy(s => new { s.Fecha.Date, s.Cantidad });
        foreach (var d in fechas)
        {
          var cantidad = detail.Where(s => s.Key.Date.ToString("MMMM", new CultureInfo("es-MX")) == d).Sum(s => s.Key.Cantidad);
          curDto.Arguments.Add(d);
          curDto.Values.Add(cantidad.ToString());
        }

        listDto.Add(curDto);
      }


      return listDto;
    }

    public List<ChartReportDto> GetReportGraficoAnio(int clienteId, DateTime? dateInit, DateTime? dateEnd)
    {
      var listDto = new List<ChartReportDto>();

      var prod = from d in _dbContext.InspeccionDetalles
                 join i in _dbContext.Inspeciones on d.IdInspeccion equals i.Id
                 join p in _dbContext.Productos on d.ProductoId equals p.Id
                 join t in _dbContext.Terminales on i.TerminalId equals t.Id
                 join des in _dbContext.Terminales on i.TerminalDestinoId equals des.Id
                 where i.FechaInicio.Value.Date >= dateInit.Value.Date
                       && i.FechaInicio.Value.Date <= dateEnd.Value.Date
                       && i.ClienteId == clienteId
                 select new { i, d, p, t, des };


      var result = prod.GroupBy(s => new { Fecha = $"{s.i.FechaInicio.Value:yyyy}", Producto = s.p.Nombre, })
        .Select(s => new
        {
          Producto = s.Key.Producto,
          Fecha = s.Key.Fecha,
          Cantidad = s.Sum(d => d.d.Cantidad),
        });

      var rresult = result.GroupBy(s => s.Producto);
      var fechas = result.Select(s => s.Fecha).Distinct();
      foreach (var r in rresult)
      {
        var curDto = new ChartReportDto();
        curDto.Label = r.Key;
        var detail = r.GroupBy(s => new { s.Fecha, s.Cantidad });
        foreach (var d in fechas)
        {
          var cantidad = detail.Where(s => s.Key.Fecha == d).Sum(s => s.Key.Cantidad);
          curDto.Arguments.Add(d);
          curDto.Values.Add(cantidad.ToString());
        }

        listDto.Add(curDto);
      }

      return listDto;
    }

  }
}
