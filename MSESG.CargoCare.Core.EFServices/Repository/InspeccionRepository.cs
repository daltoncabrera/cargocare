using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices.Dto;
using MSESG.CargoCare.Core.Entities;

namespace MSESG.CargoCare.Core.EFServices
{
  public class InspeccionRepository : Repository<Inspeccion>
  {
    private ApplicationDbContext appContext;
    public InspeccionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
      appContext = dbContext;
    }

    public InspeccionEditDto GetByOrderId(int id)
    {
      var result = new InspeccionEditDto();

      var inspect = (from o in appContext.Precargas
                     join c in appContext.Cisternas on o.CisternaId equals c.Id
                     join cd in appContext.CisternaDetalles on c.Id equals cd.CisternaId into cdG
                     join i in appContext.Inspeciones on o.Id equals i.PrecargaId into iG
                     join d in appContext.PrecargaDetalles on o.Id equals d.PrecargaId into pdG
                     where o.Id == id
                     select new { Orden = o, Inspeccion = iG.FirstOrDefault(), Cisterna = c, Detalles = pdG, CisternaDetalle = cdG }).First();

      if (inspect.Inspeccion != null)
      {
        result.Inspeccion = inspect.Inspeccion;
        result.Detalle = appContext.InspeccionDetalles.Where(s => s.IdInspeccion == inspect.Inspeccion.Id);

      }
      else
      {
        result.Inspeccion = new Inspeccion()
        {
          ChoferId = inspect.Orden.ChoferId,
          CamionId = inspect.Orden.CamionId,
          CisternaId = inspect.Orden.CisternaId,
          FechaInicio = inspect.Orden.Fecha,
          ClienteId = inspect.Orden.ClienteId,
          ProductoId = inspect.Orden.ProductoId,
          PrecargaId = id,
          Precarga = inspect.Orden.Correlativo,
          EmpresaId = inspect.Orden.EmpresaId,
          Nota = inspect.Orden.Nota,
          Referencia = inspect.Orden.Referencia,
          NoCargaRefineria = inspect.Orden.NoCargaRefineria,
          PlanificacionId = inspect.Orden.PlanificacionId,
          Planificacion = inspect.Orden.Planificacion,
          TerminalId = inspect.Orden.TerminalId,
          TerminalDestinoId = inspect.Orden.TerminalDestinoId,
          ConduceTpl = inspect.Orden.ConduceTpl,
          LlenaDetalle = inspect.Orden.LlenaDetalle
        };

        var detalle = new List<InspeccionDetalle>();
        foreach (var cd in inspect.CisternaDetalle)
        {
          var deta = new InspeccionDetalle();
          deta.CompartimentoId = cd.Compartimento;
          fillProducto(deta, inspect.Detalles, (preDeta) =>
          {
            deta.Capacidad = cd?.Capacidad;
            deta.Cantidad = preDeta?.Cantidad;
            deta.CantidadDespachada = preDeta?.Cantidad;
            deta.ProductoId = preDeta?.ProductoId;
            deta.Producto = preDeta?.Producto;
            deta.EnUso = preDeta != null;

            deta.HasChapa = inspect.CisternaDetalle.Any(s => s.HasChapa && s.Compartimento == deta.CompartimentoId);
            deta.HasBocaCarga = inspect.CisternaDetalle.Any(s => s.HasBoca && s.Compartimento == deta.CompartimentoId);
            deta.HasBocaDescarga = inspect.CisternaDetalle.Any(s => s.HasDescarga && s.Compartimento == deta.CompartimentoId);
          });

          detalle.Add(deta);
        }

        result.HasChapa = inspect.CisternaDetalle.Any(s => s.HasChapa);
        result.HasBocaCarga = inspect.CisternaDetalle.Any(s => s.HasBoca);
        result.HasBocaDescarga = inspect.CisternaDetalle.Any(s => s.HasDescarga);

        result.Detalle = detalle;

      }


      return result;

    }

    public InspeccionEditDto GetById(int id)
    {
      var result = new InspeccionEditDto();

      var inspect = (from i in appContext.Inspeciones
                     join d in appContext.InspeccionDetalles on i.Id equals d.IdInspeccion into pdG
                     join c in appContext.Cisternas on i.CisternaId equals c.Id
                     join cd in appContext.CisternaDetalles on c.Id equals cd.CisternaId into cdG
                     where i.Id == id
                     select new { Inspeccion = i, Cisterna = c, Detalles = pdG, CisternaDetalle = cdG }).First();

      if (inspect.Inspeccion != null)
      {
        result.Inspeccion = inspect.Inspeccion;
        result.Detalle = appContext.InspeccionDetalles.Where(s => s.IdInspeccion == inspect.Inspeccion.Id);

      }
      else
      {
        result.Inspeccion = new Inspeccion()
        {

        };

        var detalle = new List<InspeccionDetalle>();
        for (var i = 1; i <= inspect?.CisternaDetalle?.Count(); i++)
        {
          var deta = new InspeccionDetalle();
          deta.CompartimentoId = i;
          deta.HasChapa = inspect.CisternaDetalle.Any(s => s.HasChapa && s.Compartimento == deta.CompartimentoId);
          deta.HasBocaCarga = inspect.CisternaDetalle.Any(s => s.HasBoca && s.Compartimento == deta.CompartimentoId);
          deta.HasBocaDescarga = inspect.CisternaDetalle.Any(s => s.HasDescarga && s.Compartimento == deta.CompartimentoId);
          detalle.Add(deta);
        }

        if (inspect.CisternaDetalle != null)
        {
          result.HasChapa = inspect.CisternaDetalle.Any(s => s.HasChapa);
          result.HasBocaCarga = inspect.CisternaDetalle.Any(s => s.HasBoca);
          result.HasBocaDescarga = inspect.CisternaDetalle.Any(s => s.HasDescarga);
        }

        result.Detalle = detalle;
      }

      return result;
    }

    private void fillProducto(InspeccionDetalle deta, IEnumerable<PrecargaDetalle> detalles, Action<PrecargaDetalle> a)
    {
      switch (deta.CompartimentoId)
      {
        case 1:
          a(detalles.FirstOrDefault(s => s.Compartimento1 > 0));
          break;
        case 2:
          a(detalles.FirstOrDefault(s => s.Compartimento2 > 0));
          break;
        case 3:
          a(detalles.FirstOrDefault(s => s.Compartimento3 > 0));
          break;
        case 4:
          a(detalles.FirstOrDefault(s => s.Compartimento4 > 0));
          break;
        case 5:
          a(detalles.FirstOrDefault(s => s.Compartimento5 > 0));
          break;
        case 6:
          a(detalles.FirstOrDefault(s => s.Compartimento6 > 0));
          break;
        default:
          a(null);
          break;
      }
    }

    public ConduceDTO GetConduceById(int id)
    {

      var result = new ConduceDTO();

      var inspect = (from i in appContext.Inspeciones
                     join d in appContext.InspeccionDetalles on i.Id equals d.IdInspeccion into dg
                     join p in appContext.Precargas on i.PrecargaId equals p.Id into pg
                     join c in appContext.Clientes on i.ClienteId equals c.Id
                     join ch in appContext.Choferes on i.ChoferId equals ch.Id
                     join ca in appContext.Camiones on i.CamionId equals ca.Id
                     join ci in appContext.Cisternas on i.CisternaId equals ci.Id
                     join u in appContext.Users on i.InspectorId equals u.Id
                     join t in appContext.Terminales on i.TerminalId equals t.Id
                     join t2 in appContext.Terminales on i.TerminalDestinoId equals t2.Id into tg
                     join e in appContext.Empresas on i.EmpresaId equals e.Id
                     join pr in appContext.Productos on i.ProductoId equals pr.Id into prG
                     from o in pg.DefaultIfEmpty()
                     from dest in tg.DefaultIfEmpty()
                     from prod in prG.DefaultIfEmpty()
                     where i.Id == id
                     select new { Orden = o, Inspeccion = i, Cliente = c, Chofer = ch, Camion = ca, Cisterna = ci, Inspector = u, Terminal = t, Destino = dest, Empresa = e, Detalle = dg, Producto = prod }).FirstOrDefault();
      
      if (inspect != null)
      {
        result.Inspeccion = new InspeccionDto()
        {
          Id = inspect.Inspeccion.Id,
          NoConduce = inspect.Inspeccion.Referencia,
          TipoConsuce =  inspect.Cliente.ConduceTpl,
          Correlativo = inspect.Inspeccion.Correlativo,
          OrdenId = inspect.Orden?.Id,
          OrdenCorrelativo = inspect.Orden?.Correlativo,
          EmpresaId = inspect.Inspeccion.EmpresaId,
          EmpresaNombre = inspect.Empresa.Nombre,
          Compartientos = inspect.Inspeccion.Compartimentos,
          ChoferId = inspect.Inspeccion.ChoferId,
          Terminal = inspect.Terminal.Nombre,
          Destino = inspect.Destino?.Nombre,
          ChoferNombre = inspect.Chofer.Nombre,
          TipoProducto = inspect.Producto != null ?  inspect.Producto.Nombre : "",
          Temperatura = inspect.Inspeccion.Temperatura,
          CertificadoCalidad = inspect.Inspeccion.CertificadoCalidad,
          ChoferCedula = inspect.Chofer.Cedula,
          CamionId = inspect.Inspeccion.CamionId,
          FichaCamion = inspect.Camion.Ficha,
          PlacaCamion = inspect.Camion.Placa,
          CisternaId = inspect.Inspeccion.CisternaId,
          FichaCisterna = inspect.Cisterna.Ficha,
          PlacaCisterna = inspect.Cisterna.Placa,
          FechaInicio = inspect.Inspeccion.FechaInicio,
          FechaFin = inspect.Inspeccion.FechaFin,
          ClienteId = inspect.Inspeccion.ClienteId,
          ClienteNombre = inspect.Cliente.Nombre,
          InspectorNombre = inspect.Inspector.FullName,
          InspectorId = inspect.Inspector.Id,
          Capacidad = inspect.Detalle.Sum(s  => s.Capacidad),
          Cantidad = inspect.Inspeccion.LlenaDetalle?  inspect.Detalle.Sum(s  => s.Cantidad) : inspect.Inspeccion.Cantidad,
          Nota = inspect.Inspeccion.Nota,
          Referencia = inspect.Inspeccion.Referencia,
          NoCargaRefineria = inspect.Inspeccion.NoCargaRefineria,
          PlanificacionId = inspect.Inspeccion.PlanificacionId,
          Planificacion = inspect.Inspeccion.Planificacion,
          ConduceCaption = inspect.Destino.ConduceCaption,
        };

        result.InspeccionDetalle = (from d in inspect.Detalle
                                    join p in appContext.Productos on d.ProductoId equals p.Id into pg
                                    from prod in pg.DefaultIfEmpty()

                                    select new InspeccionDetalleDto()
                                    {
                                      CompartimentoId = d.CompartimentoId,
                                      ProductoId = d.ProductoId,
                                      Producto = prod != null ?  prod.Nombre : "",
                                      SelloChapaManholeId = d.SelloChapaManholeId,
                                      SelloChapaManhole = "'" + d.SelloChapaManhole ,
                                      SelloBocaCargaId =  d.SelloBocaCargaId,
                                      SelloBocaCarga = "'" + d.SelloBocaCarga,
                                      SelloBocaDescargaId = d.SelloBocaDescargaId,
                                      SelloBocaDescarga = "'" + d.SelloBocaDescarga,
                                      Capacidad = d.Capacidad,
                                      Cantidad = d.Cantidad,
                                      CantidadDespachada =  d.CantidadDespachada,
                                      EnUso = d.EnUso
                                    }).ToList();
      }

      var detalleProducto = new List<DetallePorCompartimento>();
      var productoIds = result.InspeccionDetalle.Where(s => s.EnUso).Select(s => new { s.ProductoId, s.Producto }).Distinct();
      foreach (var p in productoIds)
      {
        detalleProducto.Add(new DetallePorCompartimento()
        {
          Producto = p.Producto,
          Compartiento1 = result.InspeccionDetalle.Where(s => s.ProductoId == p.ProductoId && s.CompartimentoId == 1).Select(s => s.CantidadDespachada).FirstOrDefault(),
          Compartiento2 = result.InspeccionDetalle.Where(s => s.ProductoId == p.ProductoId && s.CompartimentoId == 2).Select(s => s.CantidadDespachada).FirstOrDefault(),
          Compartiento3 = result.InspeccionDetalle.Where(s => s.ProductoId == p.ProductoId && s.CompartimentoId == 3).Select(s => s.CantidadDespachada).FirstOrDefault(),
          Compartiento4 = result.InspeccionDetalle.Where(s => s.ProductoId == p.ProductoId && s.CompartimentoId == 4).Select(s => s.CantidadDespachada).FirstOrDefault(),
          SubTotal = result.InspeccionDetalle.Where(s => s.ProductoId == p.ProductoId).Sum(s => s.CantidadDespachada),
        });
      }

      result.DetalleProducto = detalleProducto;

      return result;

    }

    public IEnumerable<InspeccionDetalle> GetDetail(int id)
    {
      return appContext.InspeccionDetalles.Where(s => s.IdInspeccion == id);
    }

    public void SaveInspeccion(InspeccionEditDto model)
    {
      using (var dbTransction = appContext.Database.BeginTransaction())
      {
        try
        {
          model.Inspeccion.Compartimentos = model.Detalle.Count();
          if (model.Inspeccion.Id > 0)
          {
            Update(model.Inspeccion);
          }
          else
          {
            var oldCorrelativo = appContext.Inspeciones.Where(s => s.ClienteId == model.Inspeccion.ClienteId)
                .OrderByDescending(s => s.Correlativo).FirstOrDefault()
                ?.Correlativo;
            var newCorrelativo = oldCorrelativo + 1 ?? 1;
            model.Inspeccion.Correlativo = newCorrelativo;

            var orden = appContext.Ordenes.FirstOrDefault(s => s.Id == model.Inspeccion.PrecargaId);

            if (orden != null)
            {
              orden.Status = StatusType.Inspeccionando;
            }

            Insert(model.Inspeccion);
          }

          _context.SaveChanges();

          var olDetalles = GetDetail(model.Inspeccion.Id);
          var inspeccionDetalles = olDetalles.ToList();
          var detacmps = inspeccionDetalles.Select(s => s.CompartimentoId).ToList();
          var toInsert = model.Detalle.Where(s => !detacmps.Contains(s.CompartimentoId));

          var newSellos = new List<int?>();
          newSellos.AddRange(model.Detalle.Select(s => s.SelloChapaManholeId).ToList());
          newSellos.AddRange(model.Detalle.Select(s => s.SelloBocaCargaId).ToList());
          newSellos.AddRange(model.Detalle.Select(s => s.SelloBocaDescargaId).ToList());

          var oldSellos = new List<int?>();
          oldSellos.AddRange(inspeccionDetalles.Select(s => s.SelloChapaManholeId).ToList());
          oldSellos.AddRange(inspeccionDetalles.Select(s => s.SelloBocaCargaId).ToList());
          oldSellos.AddRange(inspeccionDetalles.Select(s => s.SelloBocaDescargaId).ToList());

          var liberados = oldSellos.Where(s => !newSellos.Contains(s));


          foreach (var deta in inspeccionDetalles)
          {
            var curDeta = model.Detalle.FirstOrDefault(s => s.CompartimentoId == deta.CompartimentoId);
            if (curDeta == null)
            {
              appContext.InspeccionDetalles.Remove(deta);
            }
            else
            {
              deta.Capacidad = curDeta.Capacidad;
              deta.CantidadDespachada = curDeta.CantidadDespachada;
              deta.Cantidad = curDeta.Cantidad;
              deta.UnidadId = curDeta.UnidadId;
              deta.ProductoId = curDeta.ProductoId;
              deta.SelloChapaManholeId = curDeta.SelloChapaManholeId;
              deta.SelloChapaManhole = curDeta.SelloChapaManhole;
              deta.SelloBocaCargaId = curDeta.SelloBocaCargaId;
              deta.SelloBocaCarga = curDeta.SelloBocaCarga;
              deta.SelloBocaDescargaId = curDeta.SelloBocaDescargaId;
              deta.SelloBocaDescarga = curDeta.SelloBocaDescarga;
              deta.EnUso = curDeta.EnUso;
              deta.EmpresaId = model.Inspeccion.EmpresaId;
              deta.ClienteId = model.Inspeccion.ClienteId;
              deta.IdInspeccion = model.Inspeccion.Id;
              deta.Producto = curDeta.Producto;
            }

          }

          foreach (var deta in toInsert)
          {
            deta.EmpresaId = model.Inspeccion.EmpresaId;
            deta.ClienteId = model.Inspeccion.ClienteId;
            deta.IdInspeccion = model.Inspeccion.Id;
            appContext.InspeccionDetalles.Add(deta);
          }

          var sellosParaLiberar = appContext.Sellos.Where(s => liberados.Contains(s.Id));
          var sellosParaReservar = appContext.Sellos.Where(s => newSellos.Contains(s.Id));

          foreach (var sello in sellosParaReservar)
          {
            //if (sello.SelloStatus != SelloStatus.Disponible)
            //    throw new Exception(string.Format("Este sello No se puede usar: {0} / {1} ", sello.Lote,
            //        sello.CodSello));

            sello.SelloStatus = SelloStatus.Usado;

          }

          foreach (var sello in sellosParaLiberar)
          {
            sello.SelloStatus = SelloStatus.Disponible;
          }

          appContext.SaveChanges();
          dbTransction.Commit();
        }
        catch (Exception e)
        {
          dbTransction.Rollback();
          throw e;
        }
      }
    }


    public IEnumerable<InspeccionDto> GetByCliente(int clienteId)
    {
      var r = from i in All.Where(s => s.ClienteId == clienteId)
              join ca in appContext.Camiones on i.CamionId equals ca.Id into caG
              join cho in appContext.Choferes on i.ChoferId equals cho.Id into choG
              join cis in appContext.Cisternas on i.CisternaId equals cis.Id into cisG
              join p in appContext.Planificaciones on i.PlanificacionId equals p.Id into pD
              join o in appContext.Precargas on i.PrecargaId equals o.Id into oD
              join t in appContext.Terminales on i.TerminalId equals t.Id into tD
              //from deta in gD.DefaultIfEmpty()
              from camion in caG.DefaultIfEmpty()
              from chofer in choG.DefaultIfEmpty()
              from cisterna in cisG.DefaultIfEmpty()
              from planificacion in pD.DefaultIfEmpty()
              from orden in oD.DefaultIfEmpty()
              from terminal in tD.DefaultIfEmpty()
              select new InspeccionDto()
              {
                Id = i.Id,
                Correlativo = i.Correlativo,
                NoOrden = (orden != null) ? orden.Correlativo : 0,
                PlanificacionId = (planificacion != null) ? planificacion.Id : 0,
                Planificacion = (planificacion != null) ? planificacion.Correlativo : 0,
                Terminal = (terminal != null) ? terminal.Nombre : "",
                OrdenId = i.Id,
                ChoferNombre = (chofer != null) ? chofer.Nombre : "",
                FichaCamion = (camion != null) ? camion.Ficha : "",
                FichaCisterna = cisterna.Ficha,
                FechaInicio = i.FechaInicio,
                FechaFin = i.FechaFin,
                Referencia = i.Referencia,
                NoCargaRefineria = i.NoCargaRefineria,
                Horas = i.FechaInicio.HasValue && i.FechaFin.HasValue ? (i.FechaFin - i.FechaInicio).Value.Hours : 0,
                Minutos = i.FechaInicio.HasValue && i.FechaFin.HasValue ? (i.FechaFin - i.FechaInicio).Value.Minutes : 0
              };

      return r.OrderByDescending(s => s.Id);
    }

  }
}
