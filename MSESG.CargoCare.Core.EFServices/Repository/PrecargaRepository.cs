using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.Dto;
using MSESG.CargoCare.Core.Entities;
using MSESG.CargoCare.Core.EFServices.Dto;

namespace MSESG.CargoCare.Core.EFServices
{
  public class PrecargaRepository : Repository<Precarga>
  {
    private ApplicationDbContext appContext;
    public PrecargaRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
      appContext = dbContext;
    }

    public Precarga Get(int id)
    {
      return All.FirstOrDefault(s => s.Id == id);
    }

    public IEnumerable<PrecargaDetalle> GetDetail(int id)
    {
      
      return id > 0 ? appContext.PrecargaDetalles.Where(s => s.PrecargaId == id) : null;
    }

    public IEnumerable<PrecargaDetalle> GetDetailMock(int clienteId)
    {
      var details = from pc in appContext.ProductosClientes
        join p in appContext.Productos on pc.ProductoId equals p.Id
        where pc.ClienteId == clienteId
        select new PrecargaDetalle
        {
          ProductoId = p.Id,
          Producto = p.Nombre,
          Cantidad = 0,
          Compartimento1 = 0,
          Compartimento2 = 0,
          Compartimento3 = 0,
          Compartimento4 = 0,
          Compartimento5 = 0,
          Compartimento6 = 0,
        };

      return details;
    }

    public void SavePrecarga(PrecargaEditDto model)
    {
      using (var dbTransction = appContext.Database.BeginTransaction())
      {
        try
        {
          if (model.Precarga.Id > 0)
          {
            Update(model.Precarga);
          }
          else
          {
            var oldCorrelativo = appContext.Precargas.Where(s => s.ClienteId == model.Precarga.ClienteId)
                .OrderByDescending(s => s.Correlativo).FirstOrDefault()
                ?.Correlativo;
            var newCorrelativo = oldCorrelativo + 1 ?? 1;
            model.Precarga.Correlativo = newCorrelativo;
            model.Precarga.Status = StatusType.Creado;

            Insert(model.Precarga);
          }

          _context.SaveChanges();

    
          foreach (var deta in model.Detalle)
          {
            if (deta.Id > 0)
            {
              deta.ModifiedDate = DateTime.Now;
              deta.ModifiedUser = model.Precarga.ModifiedUser;
              deta.PrecargaId = model.Precarga.Id;

              appContext.PrecargaDetalles.Update(deta);
            }
            else
            {
              deta.ClienteId = model.Precarga.ClienteId;
              deta.EmpresaId = model.Precarga.EmpresaId;
              deta.CreatedDate = DateTime.Now;
              deta.CreatedUser = model.Precarga.CreatedUser;
              deta.PrecargaId = model.Precarga.Id;
              appContext.PrecargaDetalles.Add(deta);
            }
          }

          appContext.SaveChanges();
          
          dbTransction.Commit();
        }
        catch (Exception e)
        {
          dbTransction.Rollback();
          throw;
        }
      }
    }

    public PrecargaReportDto GetReportById(int id)
    {
      var r = from o in All
        join ca in appContext.Camiones on o.CamionId equals ca.Id into caG
        join cho in appContext.Choferes on o.ChoferId equals cho.Id into choG
        join cis in appContext.Cisternas on o.CisternaId equals cis.Id into cisG
        join p in appContext.Planificaciones on o.PlanificacionId equals p.Id into pD
        join d in appContext.PrecargaDetalles on o.Id equals d.PrecargaId into gD
        join c in appContext.Clientes on o.ClienteId equals c.Id into cD
        join e in appContext.Empresas on o.EmpresaId equals e.Id into eD
        join i in appContext.Inspeciones on o.Id equals i.PrecargaId into iD
        join t1 in appContext.Terminales on o.TerminalId equals t1.Id into t1D
        join t2 in appContext.Terminales on o.TerminalDestinoId equals t2.Id into t2D
        join pro in appContext.Productos on o.ProductoId equals pro.Id into proD
        from camion in caG.DefaultIfEmpty()
        from chofer in choG.DefaultIfEmpty()
        from cisterna in cisG.DefaultIfEmpty()
        from planificacion in pD.DefaultIfEmpty()
        from cliente in cD.DefaultIfEmpty()
        from empresa in eD.DefaultIfEmpty()
        from terminal in t1D.DefaultIfEmpty()
        from destino in t2D.DefaultIfEmpty()
        from inspeccion in iD.DefaultIfEmpty()
        from producto in proD.DefaultIfEmpty()
        where o.Id == id
        select new PrecargaReportDto
        {
          Precarga = new PrecargaDto()
          {
            Correlativo = o.Correlativo,
            CodigoProducto = producto != null ?  producto.Nombre : "",
            Terminal = terminal != null ? terminal.Nombre : "",
            Destino = destino != null ? destino.Nombre : "",
            PlanificacionId = (planificacion != null) ? planificacion.Id : 0,
            Planificacion = (planificacion != null) ? planificacion.Correlativo : 0,
            PrecargaId = o.Id,
            ChoferNombre = (chofer != null) ? chofer.Nombre : "",
            FichaCamion = (camion != null) ? camion.Ficha : "",
            FichaCisterna = cisterna.Ficha,
            Fecha = o.Fecha,
            Referencia = o.Referencia,
            NoCargaRefineria = o.NoCargaRefineria,
            ClienteNombre = cliente != null ? cliente.Nombre : "",
            ClienteSlug = cliente != null ? cliente.Slug : "",
            EmpresaNombre = empresa != null ? empresa.Nombre : "",
            EmpresaSlug = empresa != null ? empresa.Slug : "",
          },
          Detalle = gD
        };

      return r.FirstOrDefault();
    }

    public Precarga GetById(int id)
    {
      return All.FirstOrDefault(s => s.Id == id);
    }

    public void DeletePrecarga(Precarga Precarga)
    {
      var detalles = GetDetail(Precarga.Id);
      appContext.PrecargaDetalles.RemoveRange(detalles);
      Delete(Precarga);

      _context.SaveChanges();
    }

    public IEnumerable<PrecargaDto> GetByCliente(int? clienteId = null)
    {
      var r = from o in All.Where(s => clienteId <= 0 || s.ClienteId == clienteId).ToList()
              join ca in appContext.Camiones on o.CamionId equals ca.Id into caG
              join cho in appContext.Choferes on o.ChoferId equals cho.Id into choG
              join cis in appContext.Cisternas on o.CisternaId equals cis.Id into cisG
              join ter in appContext.Terminales on o.TerminalId equals ter.Id into terG
              join terd in appContext.Terminales on o.TerminalDestinoId equals terd.Id into terdG
              join p in appContext.Planificaciones on o.PlanificacionId equals p.Id into pD
              join c in appContext.Clientes on o.ClienteId equals c.Id into cD
              join e in appContext.Empresas on o.EmpresaId equals e.Id into eD
              join i in appContext.Inspeciones on o.Id equals i.PrecargaId into iD
              //join d in appContext.PrecargaDetalles on o.Id equals d.PrecargaId into gD
              //from deta in gD.DefaultIfEmpty()
              from camion in caG.DefaultIfEmpty()
              from chofer in choG.DefaultIfEmpty()
              from cisterna in cisG.DefaultIfEmpty()
              from planificacion in pD.DefaultIfEmpty()
              from terminal in terG.DefaultIfEmpty()
              from destino in terdG.DefaultIfEmpty()
              from cliente in cD.DefaultIfEmpty()
              from empresa in eD.DefaultIfEmpty()
              from Inspeccion in iD.DefaultIfEmpty()
              select new PrecargaDto()
              {
                PrecargaId = o.Id,
                Correlativo = o.Correlativo,
                PlanificacionId = (planificacion != null) ? planificacion.Id : 0,
                Planificacion = (planificacion != null) ? planificacion.Correlativo : 0,
                Terminal = (terminal != null) ? terminal.Nombre : "",
                Destino = (terminal != null) ? destino.Nombre : "",
                ChoferNombre = (chofer != null) ? chofer.Nombre : "",
                FichaCamion = (camion != null) ? camion.Ficha : "",
                FichaCisterna = cisterna.Ficha,
                Fecha = o.Fecha,
                Referencia = o.Referencia,
                NoCargaRefineria = o.NoCargaRefineria,
                ClienteNombre = cliente != null ? cliente.Nombre : "",
                ClienteSlug = cliente != null ? cliente.Slug : "",
                EmpresaNombre = empresa != null ? empresa.Nombre : "",
                EmpresaSlug = empresa != null ? empresa.Slug : "",
                InspeccionId = Inspeccion != null ? Inspeccion.Id : 0,
                Inspeccion = Inspeccion != null ? Inspeccion.Correlativo : 0,
                FechaInicio = Inspeccion != null ? Inspeccion.FechaInicio : null,
                FechaFin = Inspeccion != null ? Inspeccion.FechaFin : null,
              };

      return r.OrderByDescending(s => s.PrecargaId);
    }


    public IEnumerable<PrecargaDto> GetPenddingByClienteWithDetails(int? clienteId = null)
    {
      var r = from o in All.Where(s => clienteId <= 0 || s.ClienteId == clienteId).ToList()
              join ca in appContext.Camiones on o.CamionId equals ca.Id into caG
              join cho in appContext.Choferes on o.ChoferId equals cho.Id into choG
              join cis in appContext.Cisternas on o.CisternaId equals cis.Id into cisG
              join p in appContext.Planificaciones on o.PlanificacionId equals p.Id into pD
              join d in appContext.PrecargaDetalles on o.Id equals d.PrecargaId into gD
              join c in appContext.Clientes on o.ClienteId equals c.Id into cD
              join e in appContext.Empresas on o.EmpresaId equals e.Id into eD
              join i in appContext.Inspeciones on o.Id equals i.PrecargaId into iD
              from camion in caG.DefaultIfEmpty()
              from chofer in choG.DefaultIfEmpty()
              from cisterna in cisG.DefaultIfEmpty()
              from planificacion in pD.DefaultIfEmpty()
              from cliente in cD.DefaultIfEmpty()
              from empresa in eD.DefaultIfEmpty()
              from inspeccion in iD.DefaultIfEmpty()
              where inspeccion == null
              select new PrecargaDto()
              {
                Correlativo = o.Correlativo,
                PlanificacionId = (planificacion != null) ? planificacion.Id : 0,
                Planificacion = (planificacion != null) ? planificacion.Correlativo : 0,
                PrecargaId = o.Id,
                ChoferNombre = (chofer != null) ? chofer.Nombre : "",
                FichaCamion = (camion != null) ? camion.Ficha : "",
                FichaCisterna = cisterna.Ficha,
                Fecha = o.Fecha,
                Referencia = o.Referencia,
                NoCargaRefineria = o.NoCargaRefineria,
                ClienteNombre = cliente != null ? cliente.Nombre : "",
                ClienteSlug = cliente != null ? cliente.Slug : "",
                EmpresaNombre = empresa != null ? empresa.Nombre : "",
                EmpresaSlug = empresa != null ? empresa.Slug : "",
            
              };

      return r.OrderByDescending(s => s.PrecargaId);
    }

    public IEnumerable<PrecargaDto> GetPenddingByCliente(int? clienteId = null)
    {
      var r = from o in All.Where(s => clienteId <= 0 || s.ClienteId == clienteId).ToList()
              join ca in appContext.Camiones on o.CamionId equals ca.Id into caG
              join cho in appContext.Choferes on o.ChoferId equals cho.Id into choG
              join cis in appContext.Cisternas on o.CisternaId equals cis.Id into cisG
              join p in appContext.Planificaciones on o.PlanificacionId equals p.Id into pD
              join c in appContext.Clientes on o.ClienteId equals c.Id into cD
              join e in appContext.Empresas on o.EmpresaId equals e.Id into eD
              join i in appContext.Inspeciones on o.Id equals i.PrecargaId into iD
              from camion in caG.DefaultIfEmpty()
              from chofer in choG.DefaultIfEmpty()
              from cisterna in cisG.DefaultIfEmpty()
              from planificacion in pD.DefaultIfEmpty()
              from cliente in cD.DefaultIfEmpty()
              from empresa in eD.DefaultIfEmpty()
              from inspeccion in iD.DefaultIfEmpty()
              where inspeccion == null
              select new PrecargaDto()
              {
                Correlativo = o.Correlativo,
                PlanificacionId = (planificacion != null) ? planificacion.Id : 0,
                Planificacion = (planificacion != null) ? planificacion.Correlativo : 0,
                PrecargaId = o.Id,
                ChoferNombre = (chofer != null) ? chofer.Nombre : "",
                FichaCamion = (camion != null) ? camion.Ficha : "",
                FichaCisterna = cisterna.Ficha,
                Fecha = o.Fecha,
                Referencia = o.Referencia,
                NoCargaRefineria = o.NoCargaRefineria,
                ClienteNombre = cliente != null ? cliente.Nombre : "",
                ClienteSlug = cliente != null ? cliente.Slug : "",
                EmpresaNombre = empresa != null ? empresa.Nombre : "",
                EmpresaSlug = empresa != null ? empresa.Slug : ""
              };

      return r.OrderByDescending(s => s.PrecargaId);
    }
  }
}
