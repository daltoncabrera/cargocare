using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core;
using System.Linq;
using MSESG.CargoCare.Core.Dto;
using MSESG.CargoCare.Core.EFServices.Dto;
using MSESG.CargoCare.Core.Entities;

namespace MSESG.CargoCare.Core.EFServices
{
  public class ActividadRepository : Repository<Actividad>
  {
    private ApplicationDbContext _ctx;
    public ActividadRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
      _ctx = dbContext;
    }

    public object GetActividades()
    {
      throw new NotImplementedException();
    }

    public object GetActividadesDiarioReport(int clienteId, DateTime? dateInit, int? dir)
    {
      var result = new ActividadDto();
      var tempDate = dateInit;
      if (!dateInit.HasValue)
      {
        dateInit = _ctx.Inspeciones.Where(s => s.ClienteId == clienteId).Max(s => s.FechaInicio) ?? DateTime.Now;
      }
      else if (dir.HasValue)
      {

        dateInit = dir.Value == 1
          ? _ctx.Inspeciones.Where(s => s.ClienteId == clienteId && s.FechaInicio != null
                                        && s.FechaInicio.Value.Date > dateInit.Value.Date).Min(s => s.FechaInicio)
          : _ctx.Inspeciones.Where(s => s.ClienteId == clienteId && s.FechaInicio != null
                                        && s.FechaInicio.Value.Date < dateInit.Value.Date).Max(s => s.FechaInicio);

      }

      var cliente = _ctx.Clientes.FirstOrDefault(s => s.Id == clienteId);

      result.DateFilterInit = dateInit;
      result.ClienteId = cliente.Id;
      result.Cliente = cliente.Nombre;
      result.Type = ActividadType.Diaria;

      ///sin fechas para buscar inspecciones
      if (!dateInit.HasValue)
      {
        var beforeOrAfterMsg = "para";
        if (dir.HasValue && dir.Value == -1)
          beforeOrAfterMsg = "anterior a";

        if (dir.HasValue && dir.Value == 1)
          beforeOrAfterMsg = "despues de";

        var datePartMsg = tempDate != null ? $"{tempDate.Value:dd/MM/yyyy}" : "";
        result.Message = $"No se encontraron registros  {beforeOrAfterMsg} la fecha {datePartMsg}";
        throw new Exception(result.Message);
        //return result;
      }

      var actividad = _ctx.Actividades.FirstOrDefault(s => s.Type == ActividadType.Diaria
                                                           && s.FechaInicio.Value.Date == dateInit.Value.Date);

      result.ActividadId = actividad?.Id;
      result.Disponible = actividad != null && actividad.Disponible;

      var inspect = from i in _ctx.Inspeciones.Where(s => s.ClienteId == clienteId).ToList()
                    join d in _ctx.InspeccionDetalles on i.Id equals d.IdInspeccion
                    join p in _ctx.Precargas on i.PrecargaId equals p.Id into pg
                    join c in _ctx.Clientes on i.ClienteId equals c.Id
                    join ch in _ctx.Choferes on i.ChoferId equals ch.Id
                    join ca in _ctx.Camiones on i.CamionId equals ca.Id
                    join ci in _ctx.Cisternas on i.CisternaId equals ci.Id
                    join u in _ctx.Users on i.InspectorId equals u.Id
                    join t in _ctx.Terminales on i.TerminalId equals t.Id
                    join t2 in _ctx.Terminales on i.TerminalDestinoId equals t2.Id into tg
                    join e in _ctx.Empresas on i.EmpresaId equals e.Id
                    join pr in _ctx.Productos on i.ProductoId equals pr.Id into prG
                    from o in pg.DefaultIfEmpty()
                    from dest in tg.DefaultIfEmpty()
                    from prod in prG.DefaultIfEmpty()
                    where i.ClienteId == clienteId
                          && i.FechaInicio != null
                          && i.FechaInicio.Value.Date == dateInit.Value.Date
                    select new { Orden = o, Inspeccion = i, Cliente = c, Chofer = ch, Camion = ca, Cisterna = ci, Inspector = u, Terminal = t, Destino = dest, Empresa = e, Producto = prod };


      var resumen = inspect.GroupBy(s =>
        new { Fecha = $"{s.Inspeccion.FechaInicio.Value:dd/MM/yyyy}.", Terminal = s.Terminal.Nombre, Destino = s.Destino.Nombre })
        .Select(s => new
        {
          Fecha = s.Key.Fecha,
          HoraInicio = $"{s.Min(t => t.Inspeccion.FechaInicio):h,mm tt}",
          HoraFin = $"{s.Max(t => t.Inspeccion.FechaFin):h,mm tt}",
          s.Key.Terminal,
          s.Key.Destino,
          Total = s.Count()
        });

      var detalle = inspect.Select(s => new { s.Inspeccion.Referencia, s.Camion.Ficha, s.Inspeccion.Temperatura, s.Inspeccion.CertificadoCalidad, s.Inspeccion.TemperaturaMuestra }).Distinct();

      var observaciones =
        (from o in _ctx.Observaciones
        join u in _ctx.Users on o.InspectorId equals u.Id
       where o.ClienteId == clienteId
             && o.Fecha != null && o.Fecha.Value.Date == dateInit.Value.Date
             && o.Type == ActividadType.Diaria
        select new ObservacionDto()
        {
          Id = o.Id,
          ClienteId = o.ClienteId,
          EmpresaId = o.EmpresaId,
          Type = o.Type,
          Nota = o.Nota,
          Fecha = o.Fecha,
          ActividadId = o.ActividadId,
          Inspector = u.FullName,
          InspectorId = o.InspectorId
        }).ToList();

      if (!observaciones.Any())
        observaciones.Add(new ObservacionDto {Inspector = "", Nota = ""});

      return new
      {
        Resumen = resumen,
        Detalle = detalle,
        Observaciones = observaciones
      };

      //var m = from  i in  inspect.group
      //  select new
      //{
      //  FechaInicio = i.Inspeccion.FechaInicio,
      //  FechaFin = i.Inspeccion.FechaFin,
      //  Terminal = i.Terminal.Nombre,
      //  TerminalDestino = i.Destino.Nombre,
      //};

      //if (inspect != null)
      //{
      //  var destinos = inspect.GroupBy(a => a.Destino).Select(s => new { Terminal = s.Key, Detalle = s })
      //    .OrderBy(s => s.Terminal.DestinoOrd);

      //  result.FechaInicio = inspect.Min(s => s.Inspeccion.FechaInicio);
      //  result.FechaFin = inspect.Max(s => s.Inspeccion.FechaFin);
      //  result.TotalCamiones = inspect.Count();
      //  TerminalDestino = r.Terminal.Nombre;
      //  TerminalDestionId = r.Terminal.Id;
      //  CamionesQty = r.Detalle.Count();
      //  result.Resumen = (from r in destinos
      //                    select new ResumenDto()
      //                    {
      //                      TerminalDestino = r.Terminal.Nombre,
      //                      TerminalDestionId = r.Terminal.Id,
      //                      CamionesQty = r.Detalle.Count()
      //                    }).ToList();


      //  var deta = inspect.Select(s => new ActiviadDetalleDto()
      //  {
      //    Id = s.Inspeccion.Id,
      //    Referencia = s.Inspeccion.Referencia,
      //    Ficha = s.Camion.Ficha,
      //    TemperaturaCarga = s.Inspeccion.Temperatura,
      //    TemperaturaTomada = s.Inspeccion.TemperaturaMuestra,
      //    Certificados = s.Inspeccion.CertificadoCalidad
      //  }).ToList();

      //  result.Detalle = deta;
      //  var observaciones =
      //    from o in _ctx.Observaciones
      //    join u in _ctx.Users on o.InspectorId equals u.Id
      //    where o.ClienteId == clienteId
      //          && o.Fecha != null && o.Fecha.Value.Date == dateInit.Value.Date
      //          && o.Type == ActividadType.Diaria
      //    select new ObservacionDto()
      //    {
      //      Id = o.Id,
      //      ClienteId = o.ClienteId,
      //      EmpresaId = o.EmpresaId,
      //      Type = o.Type,
      //      Nota = o.Nota,
      //      Fecha = o.Fecha,
      //      ActividadId = o.ActividadId,
      //      Inspector = u.FullName,
      //      InspectorId = o.InspectorId
      //    };


      //result.  = observaciones.ToList();
      //}

      return inspect;
    }

    public ActividadDto GetDateInfo(int clienteId, DateTime? dateInit, int? dir)
    {
      var result = new ActividadDto();
      var tempDate = dateInit;
      if (!dateInit.HasValue)
      {
        dateInit = _ctx.Inspeciones.Where(s => s.ClienteId == clienteId).Max(s => s.FechaInicio) ?? DateTime.Now;
      }
      else if (dir.HasValue)
      {

        dateInit = dir.Value == 1
          ? _ctx.Inspeciones.Where(s => s.ClienteId == clienteId && s.FechaInicio != null
                                                                 && s.FechaInicio.Value.Date > dateInit.Value.Date).Min(s => s.FechaInicio)
          : _ctx.Inspeciones.Where(s => s.ClienteId == clienteId && s.FechaInicio != null
                                                                 && s.FechaInicio.Value.Date < dateInit.Value.Date).Max(s => s.FechaInicio);

      }

      ///sin fechas para buscar inspecciones
      if (!dateInit.HasValue)
      {
        var beforeOrAfterMsg = "para";
        if (dir.HasValue && dir.Value == -1)
          beforeOrAfterMsg = "anterior a";

        if (dir.HasValue && dir.Value == 1)
          beforeOrAfterMsg = "despues de";

        var datePartMsg = tempDate != null ? $"{tempDate.Value:dd/MM/yyyy}" : "";
        result.Message = $"No se encontraron registros  {beforeOrAfterMsg} la fecha {datePartMsg}";
        throw new Exception(result.Message);
        //return result;
      }

      var cliente = _ctx.Clientes.FirstOrDefault(s => s.Id == clienteId);

      result.DateFilterInit = dateInit;
      result.ClienteId = cliente.Id;
      result.Cliente = cliente.Nombre;
      result.Type = ActividadType.Diaria;

      return result;
    }

    public ActividadDto GetActividadesDiario(int clienteId, DateTime? dateInit, int? dir)
    {
      var result = new ActividadDto();
      var tempDate = dateInit;
      if (!dateInit.HasValue)
      {
        dateInit = _ctx.Inspeciones.Where(s => s.ClienteId == clienteId).Max(s => s.FechaInicio) ?? DateTime.Now;
      }
      else if (dir.HasValue)
      {

        dateInit = dir.Value == 1
          ? _ctx.Inspeciones.Where(s => s.ClienteId == clienteId && s.FechaInicio != null
                                        && s.FechaInicio.Value.Date > dateInit.Value.Date).Min(s => s.FechaInicio)
          : _ctx.Inspeciones.Where(s => s.ClienteId == clienteId && s.FechaInicio != null
                                        && s.FechaInicio.Value.Date < dateInit.Value.Date).Max(s => s.FechaInicio);

      }

      var cliente = _ctx.Clientes.FirstOrDefault(s => s.Id == clienteId);

      result.DateFilterInit = dateInit;
      result.ClienteId = cliente.Id;
      result.Cliente = cliente.Nombre;
      result.Type = ActividadType.Diaria;

      ///sin fechas para buscar inspecciones
      if (!dateInit.HasValue)
      {
        var beforeOrAfterMsg = "para";
        if (dir.HasValue && dir.Value == -1)
          beforeOrAfterMsg = "anterior a";

        if (dir.HasValue && dir.Value == 1)
          beforeOrAfterMsg = "despues de";

        var datePartMsg = tempDate != null ? $"{tempDate.Value:dd/MM/yyyy}" : "";
        result.Message = $"No se encontraron registros  {beforeOrAfterMsg} la fecha {datePartMsg}";
        throw new Exception(result.Message);
        //return result;
      }

      var actividad = _ctx.Actividades.FirstOrDefault(s => s.Type == ActividadType.Diaria
                                                           && s.FechaInicio.Value.Date == dateInit.Value.Date);

      result.ActividadId = actividad?.Id;
      result.Disponible = actividad != null && actividad.Disponible;

      var inspect = (from i in _ctx.Inspeciones.Where(s => s.ClienteId == clienteId).ToList()
                     join d in _ctx.InspeccionDetalles on i.Id equals d.IdInspeccion into dg
                     join p in _ctx.Precargas on i.PrecargaId equals p.Id into pg
                     join c in _ctx.Clientes on i.ClienteId equals c.Id
                     join ch in _ctx.Choferes on i.ChoferId equals ch.Id
                     join ca in _ctx.Camiones on i.CamionId equals ca.Id
                     join ci in _ctx.Cisternas on i.CisternaId equals ci.Id
                     join u in _ctx.Users on i.InspectorId equals u.Id
                     join t in _ctx.Terminales on i.TerminalId equals t.Id
                     join t2 in _ctx.Terminales on i.TerminalDestinoId equals t2.Id into tg
                     join e in _ctx.Empresas on i.EmpresaId equals e.Id
                     join pr in _ctx.Productos on i.ProductoId equals pr.Id into prG
                     from o in pg.DefaultIfEmpty()
                     from dest in tg.DefaultIfEmpty()
                     from prod in prG.DefaultIfEmpty()
                     where i.ClienteId == clienteId
                           && i.FechaInicio != null
                           && i.FechaInicio.Value.Date == dateInit.Value.Date
                     select new { Orden = o, Inspeccion = i, Cliente = c, Chofer = ch, Camion = ca, Cisterna = ci, Inspector = u, Terminal = t, Destino = dest, Empresa = e, Detalle = dg, Producto = prod });

      if (inspect != null)
      {
        var destinos = inspect.GroupBy(a => a.Destino).Select(s => new { Terminal = s.Key, Detalle = s })
          .OrderBy(s => s.Terminal.DestinoOrd);

        result.FechaInicio = inspect.Min(s => s.Inspeccion.FechaInicio);
        result.FechaFin = inspect.Max(s => s.Inspeccion.FechaFin);
        result.TotalCamiones = inspect.Count();
        result.Resumen = (from r in destinos
                          select new ResumenDto()
                          {
                            TerminalDestino = r.Terminal.Nombre,
                            TerminalDestionId = r.Terminal.Id,
                            CamionesQty = r.Detalle.Count()
                          }).ToList();


        var deta = inspect.Select(s => new ActiviadDetalleDto()
        {
          Id = s.Inspeccion.Id,
          Referencia = s.Inspeccion.Referencia,
          Ficha = s.Camion.Ficha,
          TemperaturaCarga = s.Inspeccion.Temperatura,
          TemperaturaTomada = s.Inspeccion.TemperaturaMuestra,
          Certificados = s.Inspeccion.CertificadoCalidad
        }).ToList();

        result.Detalle = deta;
        var observaciones =
          from o in _ctx.Observaciones
          join u in _ctx.Users on o.InspectorId equals u.Id
          where o.ClienteId == clienteId
                && o.Fecha != null && o.Fecha.Value.Date == dateInit.Value.Date
                && o.Type == ActividadType.Diaria
          select new ObservacionDto()
          {
            Id = o.Id,
            ClienteId = o.ClienteId,
            EmpresaId = o.EmpresaId,
            Type = o.Type,
            Nota = o.Nota,
            Fecha = o.Fecha,
            ActividadId = o.ActividadId,
            Inspector = u.FullName,
            InspectorId = o.InspectorId
          };


        result.Observaciones = observaciones.ToList();
      }

      return result;
    }

    public void CheckActivity(bool flagVisible, ActividadDto model, AppSessionData sessionData)
    {

      Actividad curActivity;
      if (model.ActividadId > 0)
      {
        curActivity = _ctx.Actividades.FirstOrDefault(s => s.Id == model.ActividadId);
        curActivity.ModifiedUser = sessionData.UserId;
        curActivity.ModifiedDate = DateTime.Now;
        curActivity.ClienteId = sessionData.ClienteId;
        curActivity.EmpresaId = sessionData.EmpresaId;
        curActivity.FechaInicio = model.DateFilterInit;
        curActivity.FechaFin = model.DateFilterInit;
        curActivity.Disponible = flagVisible;
        _ctx.Actividades.Update(curActivity);
        _ctx.SaveChanges();

      }
      else
      {
        curActivity = new Actividad();
        curActivity.InspectorId = sessionData.UserId;
        curActivity.CreatedUser = sessionData.UserId;
        curActivity.CreatedDate = DateTime.Now;
        curActivity.ClienteId = sessionData.ClienteId;
        curActivity.EmpresaId = sessionData.EmpresaId;
        curActivity.FechaInicio = model.DateFilterInit;
        curActivity.FechaFin = model.DateFilterInit;
        curActivity.Disponible = flagVisible;
        curActivity.Type = model.Type;
        _ctx.Actividades.Add(curActivity);
        _ctx.SaveChanges();

        var observacionesIds = model.Observaciones.Select(s => s.Id);
        var objservacionesToEdit = _ctx.Observaciones.Where(s => s.ClienteId == sessionData.ClienteId && observacionesIds.Contains(s.Id));
        foreach (var obs in objservacionesToEdit)
        {
          obs.ActividadId = curActivity.Id;
          _ctx.Observaciones.Update(obs);
        }

        _ctx.SaveChanges();

      }

    }

    public Observacion GetObservationById(int id)
    {
      return _ctx.Observaciones.FirstOrDefault(s => s.Id == id);
    }

    public object GetActividadesSemana(DateTime? dateInit)
    {
      throw new NotImplementedException();
    }

    public ActividadMonthDto GetActividadesMes(int clienteId, DateTime? dateInit, int? dir = null)
    {
      var result = new ActividadMonthDto();
      var tempDate = dateInit;
      if (!dateInit.HasValue)
      {
        dateInit = _ctx.Inspeciones.Where(s => s.ClienteId == clienteId).Max(s => s.FechaInicio) ?? DateTime.Now;
      }
      else if (dir.HasValue)
      {
        var lastDay = dateInit.Value.LastDayOfMonth();
        var fistDay = dateInit.Value.FirstDayOfMonth();
        dateInit = dir.Value == 1
          ? _ctx.Inspeciones.Where(s => s.ClienteId == clienteId && s.FechaInicio != null
                                        && s.FechaInicio.Value.Date > lastDay).Min(s => s.FechaInicio)
          : _ctx.Inspeciones.Where(s => s.ClienteId == clienteId && s.FechaInicio != null
                                        && s.FechaInicio.Value.Date < fistDay).Max(s => s.FechaInicio);

      }

      var cliente = _ctx.Clientes.FirstOrDefault(s => s.Id == clienteId);

      result.DateFilterInit = dateInit;
      result.ClienteId = cliente.Id;
      result.Cliente = cliente.Nombre;
      result.Type = ActividadType.Mensual;

      ///sin fechas para buscar inspecciones
      if (!dateInit.HasValue)
      {
        var beforeOrAfterMsg = "para";
        if (dir.HasValue && dir.Value == -1)
          beforeOrAfterMsg = "anterior a";

        if (dir.HasValue && dir.Value == 1)
          beforeOrAfterMsg = "despues de";

        var datePartMsg = tempDate != null ? $"{tempDate.Value:MMM yyyy}" : "";
        result.Message = $"No se encontraron registros  {beforeOrAfterMsg}  {datePartMsg}";
        throw new Exception(result.Message);
        //return result;
      }

      var actividad = _ctx.Actividades.FirstOrDefault(s => s.Type == ActividadType.Mensual
                                                           && s.FechaInicio.Value.Date == dateInit.Value.Date);

      result.ActividadId = actividad?.Id;
      result.Disponible = actividad != null && actividad.Disponible;

      var inspect = (from i in _ctx.Inspeciones.Where(s => s.ClienteId == clienteId).ToList()
                     join d in _ctx.InspeccionDetalles on i.Id equals d.IdInspeccion into dg
                     join p in _ctx.Precargas on i.PrecargaId equals p.Id into pg
                     join c in _ctx.Clientes on i.ClienteId equals c.Id
                     join ch in _ctx.Choferes on i.ChoferId equals ch.Id
                     join ca in _ctx.Camiones on i.CamionId equals ca.Id
                     join ci in _ctx.Cisternas on i.CisternaId equals ci.Id
                     join u in _ctx.Users on i.InspectorId equals u.Id
                     join t in _ctx.Terminales on i.TerminalId equals t.Id
                     join t2 in _ctx.Terminales on i.TerminalDestinoId equals t2.Id into tg
                     join e in _ctx.Empresas on i.EmpresaId equals e.Id
                     join pr in _ctx.Productos on i.ProductoId equals pr.Id into prG
                     from o in pg.DefaultIfEmpty()
                     from dest in tg.DefaultIfEmpty()
                     from prod in prG.DefaultIfEmpty()
                     where i.ClienteId == clienteId
                         && i.FechaInicio != null
                         && (i.FechaInicio.Value.Year == dateInit.Value.Year && i.FechaInicio.Value.Month == dateInit.Value.Month)
                     select new { Orden = o, Inspeccion = i, Cliente = c, Chofer = ch, Camion = ca, Cisterna = ci, Inspector = u, Terminal = t, Destino = dest, Empresa = e, Detalle = dg, Producto = prod });

      if (inspect != null)
      {
        var dias = inspect.GroupBy(s => s.Inspeccion.FechaInicio.Value.Date).Select(s => new { Dia = s.Key, Detalle = s });
        result.Detalle = new List<DatesDto>();
        result.Terminales = inspect.GroupBy(s => new { s.Destino.Id, s.Destino.Nombre, s.Destino.DestinoOrd }).Select(s => new ClienteTerminalDto
        {
          TerminalId = s.Key.Id,
          Nombre = s.Key.Nombre,
          TerminalOrd = s.Key.DestinoOrd,

        }).ToList();


        int? sum = 0;
        foreach (var dia in dias)
        {
          var curD = new DatesDto();
          curD.FechaInicio = dia.Detalle.Min(s => s.Inspeccion.FechaInicio);
          curD.FechaFin = dia.Detalle.Max(s => s.Inspeccion.FechaInicio);
          curD.TotalCamiones = dia.Detalle.Count();
          sum += curD.TotalCamiones;
          curD.Acumulado = sum;
          curD.Detalles = new Dictionary<int, ResumenDto>();
          var destinos = dia.Detalle.GroupBy(a => a.Destino).Select(s => new { Key = s.Key, G = s })
            .OrderBy(s => s.Key.DestinoOrd);

          foreach (var d in destinos)
          {
            curD.Detalles.Add(d.Key.Id, new ResumenDto()
            {
              CamionesQty = d.G.Select(s => s.Inspeccion).Count(),
              TerminalDestino = d.Key.Nombre,
              TerminalDestionId = d.Key.Id,
            });
          }

          result.Detalle.Add(curD);
        }

        result.TotalCamiones = sum;
        result.FechaInicio = inspect.Min(s => s.Inspeccion.FechaInicio);
        result.FechaFin = inspect.Max(s => s.Inspeccion.FechaFin);

        foreach (var t in result.Terminales)
        {
          t.Total = result.Detalle.SelectMany(s => s.Detalles).Where(s => s.Value.TerminalDestionId == t.TerminalId).Sum(s => s.Value.CamionesQty);
        }

        //result.TotalCamiones = inspect.Count();

        var deta = inspect.Select(s => new ActiviadDetalleDto()
        {
          Id = s.Inspeccion.Id,
          FechaInicio = s.Inspeccion.FechaInicio,
          FechaFin = s.Inspeccion.FechaFin,
          Referencia = s.Inspeccion.Referencia,
          InspectorId = s.Inspeccion.InspectorId,
          Inspector = s.Inspector != null ? s.Inspector.FullName : "",

          ChoferId = s.Inspeccion.ChoferId,
          Chofer = s.Chofer != null ? s.Chofer.Nombre : "",

          Ficha = s.Camion != null ? s.Camion.Ficha : "",
          CamionId = s.Inspeccion.CamionId,
          PlacaCamion = s.Camion != null ? s.Camion.Placa : "",
          CisternaId = s.Inspeccion.CisternaId,
          PlacaCisterna = s.Cisterna != null ? s.Cisterna.Placa : "",

          TemperaturaCarga = s.Inspeccion.Temperatura,
          TemperaturaTomada = s.Inspeccion.TemperaturaMuestra,
          Certificados = s.Inspeccion.CertificadoCalidad,
          TotalCarga = s.Inspeccion.Cantidad,
          TotalCargado = s.Inspeccion.TotalCargado,

          ProductoId = s.Inspeccion.ProductoId,
          Producto = s.Producto != null ? s.Producto.Nombre : "",
          DestinoId = s.Inspeccion.TerminalDestinoId,
          Destino = s.Destino != null ? s.Destino.Nombre : "",

          SelloBocaCarga1 = s.Detalle.Where(d => d.CompartimentoId == 1).Select(d => d.SelloBocaCarga).FirstOrDefault(),
          SelloBocaCarga2 = s.Detalle.Where(d => d.CompartimentoId == 2).Select(d => d.SelloBocaCarga).FirstOrDefault(),
          SelloBocaCarga3 = s.Detalle.Where(d => d.CompartimentoId == 3).Select(d => d.SelloBocaCarga).FirstOrDefault(),
          SelloBocaCarga4 = s.Detalle.Where(d => d.CompartimentoId == 4).Select(d => d.SelloBocaCarga).FirstOrDefault(),
          SelloBocaCarga5 = s.Detalle.Where(d => d.CompartimentoId == 5).Select(d => d.SelloBocaCarga).FirstOrDefault(),

          SelloBocaDescarga1 = s.Detalle.Where(d => d.CompartimentoId == 1).Select(d => d.SelloBocaDescarga).FirstOrDefault(),
          SelloBocaDescarga2 = s.Detalle.Where(d => d.CompartimentoId == 2).Select(d => d.SelloBocaDescarga).FirstOrDefault(),
          SelloBocaDescarga3 = s.Detalle.Where(d => d.CompartimentoId == 3).Select(d => d.SelloBocaDescarga).FirstOrDefault(),
          SelloBocaDescarga4 = s.Detalle.Where(d => d.CompartimentoId == 4).Select(d => d.SelloBocaDescarga).FirstOrDefault(),
          SelloBocaDescarga5 = s.Detalle.Where(d => d.CompartimentoId == 5).Select(d => d.SelloBocaDescarga).FirstOrDefault(),

          SelloChapa1 = s.Detalle.Where(d => d.CompartimentoId == 1).Select(d => d.SelloChapaManhole).FirstOrDefault(),
          SelloChapa2 = s.Detalle.Where(d => d.CompartimentoId == 2).Select(d => d.SelloChapaManhole).FirstOrDefault(),
          SelloChapa3 = s.Detalle.Where(d => d.CompartimentoId == 3).Select(d => d.SelloChapaManhole).FirstOrDefault(),
          SelloChapa4 = s.Detalle.Where(d => d.CompartimentoId == 4).Select(d => d.SelloChapaManhole).FirstOrDefault(),
          SelloChapa5 = s.Detalle.Where(d => d.CompartimentoId == 5).Select(d => d.SelloChapaManhole).FirstOrDefault(),

        }).ToList();

        result.Conduces = deta;

        var observaciones =
          from o in _ctx.Observaciones
          join u in _ctx.Users on o.InspectorId equals u.Id
          where o.ClienteId == clienteId
                && o.Fecha != null
                && (o.Fecha.Value.Year == dateInit.Value.Year && o.Fecha.Value.Month == dateInit.Value.Month)
                && o.Type == ActividadType.Mensual
          select new ObservacionDto()
          {
            Id = o.Id,
            ClienteId = o.ClienteId,
            EmpresaId = o.EmpresaId,
            Type = o.Type,
            Nota = o.Nota,
            Fecha = o.Fecha,
            ActividadId = o.ActividadId,
            Inspector = u.FullName,
            InspectorId = o.InspectorId,
          };


        result.Observaciones = observaciones.ToList();
      }

      return result;
    }

    public object GetActividadesRango(DateTime? dateInit, DateTime? dateEnd)
    {
      throw new NotImplementedException();
    }

    public void AddObservation(Observacion observacion)
    {
      _ctx.Observaciones.Add(observacion);
      _ctx.SaveChanges();
    }

    public void DeleteObservation(Observacion observacion)
    {
      _ctx.Observaciones.Remove(observacion);
      _ctx.SaveChanges();


    }
  }
}
