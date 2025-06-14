using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.Entities;
using MSESG.CargoCare.Core.EFServices.Dto;

namespace MSESG.CargoCare.Core.EFServices
{
  public class OrdenRepository : Repository<Orden>
  {
    private ApplicationDbContext appContext;
    public OrdenRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
      appContext = dbContext;
    }

    public Orden Get(int id)
    {
      return All.FirstOrDefault(s => s.Id == id);
    }

    public IEnumerable<OrdenDetalle> GetDetail(int ordenId)
    {
      return appContext.OrdenDetalles.Where(s => s.OrdenId == ordenId);
    }

    public void SaveOrden(OrdenEditDto model)
    {
      using (var dbTransction = appContext.Database.BeginTransaction())
      {
        try
        {
          if (model.Orden.Id > 0)
          {
            Update(model.Orden);
          }
          else
          {
            var oldCorrelativo = appContext.Ordenes.Where(s => s.ClienteId == model.Orden.ClienteId)
                .OrderByDescending(s => s.Correlativo).FirstOrDefault()
                ?.Correlativo;
            var newCorrelativo = oldCorrelativo + 1 ?? 1;
            model.Orden.Correlativo = newCorrelativo;
            model.Orden.Status = StatusType.Creado;

            Insert(model.Orden);
          }

          _context.SaveChanges();

          var olDetalles = GetDetail(model.Orden.Id);
          var ordenDetalles = olDetalles.ToList();
          var detacmps = ordenDetalles.Select(s => s.Compartimento).ToList();
          var toInsert = model.Detalle.Where(s => !detacmps.Contains(s.Compartimento));

          var newSellos = new List<int?>();
          newSellos.AddRange(model.Detalle.Select(s => s.SelloChapaManholeId).ToList());
          newSellos.AddRange(model.Detalle.Select(s => s.SelloBocaCargaId).ToList());
          newSellos.AddRange(model.Detalle.Select(s => s.SelloBocaDescargaId).ToList());

          var oldSellos = new List<int?>();
          oldSellos.AddRange(ordenDetalles.Select(s => s.SelloChapaManholeId).ToList());
          oldSellos.AddRange(ordenDetalles.Select(s => s.SelloBocaCargaId).ToList());
          oldSellos.AddRange(ordenDetalles.Select(s => s.SelloBocaDescargaId).ToList());

          var liberados = oldSellos.Where(s => !newSellos.Contains(s));


          foreach (var deta in ordenDetalles)
          {
            var curDeta = model.Detalle.FirstOrDefault(s => s.Compartimento == deta.Compartimento);
            if (curDeta == null)
            {
              appContext.OrdenDetalles.Remove(deta);
            }
            else
            {
              deta.CantidadConduce = curDeta.CantidadConduce;
              deta.CantidadDespachada = curDeta.CantidadDespachada;
              deta.CisternaDetalleId = curDeta.CisternaDetalleId;
              deta.CantidadMedida = curDeta.CantidadMedida;
              deta.OrdenId = model.Orden.Id;
              deta.MedidaId = curDeta.MedidaId;
              deta.UnidadId = curDeta.UnidadId;
              deta.ProductoId = curDeta.ProductoId;
              deta.SelloChapaManholeId = curDeta.SelloChapaManholeId;
              deta.SelloChapaManhole = curDeta.SelloChapaManhole;
              deta.SelloBocaCargaId = curDeta.SelloBocaCargaId;
              deta.SelloBocaCarga = curDeta.SelloBocaCarga;
              deta.SelloBocaDescargaId = curDeta.SelloBocaDescargaId;
              deta.SelloBocaDescarga = curDeta.SelloBocaDescarga;
              deta.EnUso = curDeta.EnUso;
              deta.EmpresaId = model.Orden.EmpresaId;
              deta.ClienteId = model.Orden.ClienteId;
              deta.Producto = curDeta.Producto;
            }

          }

          foreach (var deta in toInsert)
          {
            deta.EmpresaId = model.Orden.EmpresaId;
            deta.ClienteId = model.Orden.ClienteId;
            deta.OrdenId = model.Orden.Id;
            appContext.OrdenDetalles.Add(deta);
          }

          var sellosParaLiberar = appContext.Sellos.Where(s => liberados.Contains(s.Id));
          var sellosParaReservar = appContext.Sellos.Where(s => newSellos.Contains(s.Id) && !oldSellos.Contains(s.Id));

          foreach (var sello in sellosParaReservar)
          {
            if (sello.SelloStatus != SelloStatus.Disponible)
              throw new Exception(string.Format("Este sello No se puede usar: {0} / {1} ", sello.Lote,
                  sello.CodSello));

            sello.SelloStatus = SelloStatus.Reservado;

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
          throw;
        }
      }
    }

    public Orden GetById(int id)
    {
      return All.FirstOrDefault(s => s.Id == id);
    }

    public void DeleteOrden(Orden orden)
    {
      var detalles = GetDetail(orden.Id);
      appContext.OrdenDetalles.RemoveRange(detalles);
      Delete(orden);

      _context.SaveChanges();
    }

  }
}
