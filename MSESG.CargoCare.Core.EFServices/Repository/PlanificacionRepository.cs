using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices.Dto;
using MSESG.CargoCare.Core.Entities;

namespace MSESG.CargoCare.Core.EFServices
{
  public class PlanificacionRepository : Repository<Planificacion>
  {
    private ApplicationDbContext _ctx;
    public PlanificacionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
      _ctx = dbContext;
    }

    public IEnumerable<PlanificacionDetalle> GetDetalle(int id)
    {
      return _ctx.PlanificacionDetalles.Where(s => s.PlanificacionId == id).OrderBy(s => s.FechaInicio).ThenBy(s => s.FechaFin);
    }

    public IEnumerable<Planificacion> GetByCliente(int clienteId)
    {
      return All.Where(s => s.ClienteId == clienteId);
    }

    public IEnumerable<PlanificacionListDto> GetByClienteDto(int clienteId)
    {
      var r = from p in _ctx.Planificaciones.Where(s => s.ClienteId == clienteId).ToList()
              join d in _ctx.PlanificacionDetalles on p.Id equals d.PlanificacionId into d1
              join o in _ctx.Precargas on p.Id equals o.PlanificacionId into og
              where p.ClienteId == clienteId
              select new PlanificacionListDto
              {
                Id = p.Id,
                FechaInicio = p.FechaInicio,
                FechaFin = p.FechaFin,
                Nota = p.Nota,
                CamionesQty = d1.Sum(s => s.CamionesQty),
                OrdenesQty = og.Count()
              };

      return r;
    }

    public Planificacion GetById(int id)
    {
      return All.FirstOrDefault(s => s.Id == id);
    }

    public void GuardarPlanificacion(PlanificacionDto model)
    {
      var planificacion = model.Planificacion;
      _ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
      
      using (var dbTransction = _ctx.Database.BeginTransaction(IsolationLevel.ReadCommitted))
      {
        try
        {
          if (planificacion.Id > 0)
          {
            this.Update(planificacion);
          }
          else
          {
            var oldCorrelativo = _ctx.Planificaciones.Where(s => s.ClienteId == planificacion.ClienteId)
                .OrderByDescending(s => s.Correlativo).FirstOrDefault()
                ?.Correlativo;
            var newCorrelativo = oldCorrelativo + 1 ?? 1;

            planificacion.Correlativo = newCorrelativo;

            this.Insert(planificacion);
          }

          _ctx.SaveChanges();


          //Detalles de inspeccion terminar si insertar/actualizar/eliminar
          var oldDespa = _ctx.PlanificacionDespachos.AsNoTracking().Where(s => s.PlanificacionId == planificacion.Id).ToList();
          var despachos = model.PlanificacionDespacho.Select(s => s.Despacho).ToList();
          for (int i = 0; i < despachos.Count; i++)
          {
            despachos[i].PlanificacionId = planificacion.Id;
          }
          var toDeleteDes = oldDespa.Except(despachos, new KeyEqualityComparer<PlanificacionDespacho>(s => s.Id)); 
          var toUpdateDes = despachos.Intersect(oldDespa, new KeyEqualityComparer<PlanificacionDespacho>(s => s.Id));
          var toInsertDes = despachos.Except(oldDespa, new KeyEqualityComparer<PlanificacionDespacho>(s => s.Id));
          _ctx.PlanificacionDespachos.RemoveRange(toDeleteDes);
          _ctx.PlanificacionDespachos.UpdateRange(toUpdateDes);
          _ctx.PlanificacionDespachos.AddRange(toInsertDes);
          _ctx.SaveChanges();


          foreach (var deta in model.PlanificacionDespacho)
          {
            foreach (var s in deta.PlanificacionDetalle)
            {
              s.Detalle.PlanificacionId = deta.Despacho.PlanificacionId;
              s.Detalle.PlanificacionDespachoId = deta.Despacho.Id;
            }

          }
          
          var oldDeta = _ctx.PlanificacionDetalles.AsNoTracking().Where(s => s.PlanificacionId == planificacion.Id).ToList();
          var detallesDto = model.PlanificacionDespacho.SelectMany(s => s.PlanificacionDetalle).ToList();
          var detalles = detallesDto.Select(s => s.Detalle).ToList();
          var toDeleteDeta = oldDeta.Except(detalles, new KeyEqualityComparer<PlanificacionDetalle>(s => s.Id));;
          var toUpdateDeta = detalles.Intersect(oldDeta, new KeyEqualityComparer<PlanificacionDetalle>(s => s.Id));
          var toInsertDeta = detalles.Except( oldDeta, new KeyEqualityComparer<PlanificacionDetalle>(s => s.Id));
          _ctx.PlanificacionDetalles.RemoveRange(toDeleteDeta);
          _ctx.PlanificacionDetalles.UpdateRange(toUpdateDeta);
          _ctx.PlanificacionDetalles.AddRange(toInsertDeta);
          _ctx.SaveChanges();

          

          foreach (var deta in detallesDto)
          {
            foreach (var s in deta.Destinos)
            {
              s.PlanificacionId = deta.Detalle.PlanificacionId;
              s.PlanificacionDespachoId = deta.Detalle.PlanificacionDespachoId;
              s.PlanificacionDetalleId = deta.Detalle.Id;
            }
        
          }

          var destinos = detallesDto.SelectMany(s => s.Destinos).ToList();
          var oldDestinos = _ctx.PlanificacionDestinos.AsNoTracking().Where(s => s.PlanificacionId == planificacion.Id).ToList();

          var toDeleteDest = oldDestinos.Except(destinos, new KeyEqualityComparer<PlanificacionDestino>(s => s.Id)); ;
          var toUpdateDest = destinos.Intersect(oldDestinos, new KeyEqualityComparer<PlanificacionDestino>(s => s.Id));
          var toInsertDest = destinos.Except(oldDestinos, new KeyEqualityComparer<PlanificacionDestino>(s => s.Id));

          _ctx.PlanificacionDestinos.RemoveRange(toDeleteDest);
          _ctx.PlanificacionDestinos.UpdateRange(toUpdateDest);
          _ctx.PlanificacionDestinos.AddRange(toInsertDest);
          _ctx.SaveChanges();

          dbTransction.Commit();
        }
        catch (Exception e)
        {
          dbTransction.Rollback();
          throw;
        }
      }
    }

    public void EliminarPlanificacion(Planificacion planificacion)
    {
      var despachos=  _ctx.PlanificacionDespachos.Where(s => s.PlanificacionId == planificacion.Id);
      var detalle = _ctx.PlanificacionDetalles.Where(s => s.PlanificacionId == planificacion.Id);
      var destinos = _ctx.PlanificacionDestinos.Where(s => s.PlanificacionId == planificacion.Id);
      _ctx.PlanificacionDespachos.RemoveRange(despachos);
      _ctx.PlanificacionDestinos.RemoveRange(destinos);
      _ctx.PlanificacionDetalles.RemoveRange(detalle);
      _ctx.Planificaciones.Remove(planificacion);
      _ctx.SaveChanges();
    }

    public IEnumerable<PlanificacionDespachoDto> GetDespachoDto(int id)
    {
      var r = from depa in _ctx.PlanificacionDespachos.AsNoTracking()
        join deta in _ctx.PlanificacionDetalles.AsNoTracking() on depa.Id equals deta.PlanificacionDespachoId into detaG
        where depa.PlanificacionId == id
        select new PlanificacionDespachoDto()
        {
           Despacho =  depa,
           PlanificacionDetalle = detaG.Select(d => new PlanificacionDetalleDto() 
           { 
             Detalle = d, 
             Destinos = _ctx.PlanificacionDestinos.Where(dest => dest.PlanificacionDetalleId == d.Id).ToList()
           }).ToList()
        };

      return r;
    }
  }
}
