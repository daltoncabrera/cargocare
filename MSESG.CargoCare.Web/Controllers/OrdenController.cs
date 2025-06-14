using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices;
using MSESG.CargoCare.Core.EFServices.Dto;
using MSESG.CargoCare.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSESG.CargoCare.Web.Controllers
{
  [Route("api/[controller]")]
  public class OrdenController : BaseController
  {

    public OrdenController(UnitOfWork uow) : base(uow)
    {
    }

    [HttpGet]
    public IActionResult Get()
    {
      var result = _unitOfWork.PrecargaRepository.GetByCliente(CurAppSessionData.ClienteId).OrderByDescending(s => s.PrecargaId);
      return Ok(result);
    }

    [HttpGet("{id}/{planificacionId}")]
    public IActionResult Get(int id, int planificacionId)
    {
      Orden orden = null;
      if (id > 0)
      {
        orden = _unitOfWork.OrdenRepository.Get(id) ?? new Orden() { Fecha = DateTime.Now };
      }
      else if (planificacionId > 0)
      {
        var pla = (from p in _unitOfWork.AppDbContext.Planificaciones
                   join o in _unitOfWork.AppDbContext.Ordenes on p.Id equals o.PlanificacionId into oG
                   from order in oG.DefaultIfEmpty()
                   select new
                   {
                     Fecha = order != null ? order.Fecha : DateTime.Now,
                     Order = order,
                     Planificacion = p,
                   }).FirstOrDefault();

        EnsureCliente(pla.Planificacion);

        orden = pla?.Order ?? new Orden()
        {
          PlanificacionId = pla?.Planificacion?.Id,
          Planificacion = pla?.Planificacion?.Correlativo,
          Fecha = DateTime.Now
        };
      }
      else
      {
        orden = new Orden() { Fecha = DateTime.Now };
      }

      if (orden?.Id > 0)
        EnsureCliente(orden);

      var detalle = orden?.Id > 0 ? _unitOfWork.OrdenRepository.GetDetail(orden.Id) : new List<OrdenDetalle>();
      var productos = _unitOfWork.ProductoRepository.GetProductos().Select(s => new KeyValue(s.Id, s.Nombre));
      var camiones = _unitOfWork.CamionRepository.GetByCliente(CurAppSessionData.ClienteId).Select(s => new { Key = s.Id, Value = s.Ficha, CisternaId = s.CisternaId, ChoferId = s.ChoferId });
      var cisternas = _unitOfWork.CisternaRepository.GetByCliente(CurAppSessionData.ClienteId).Select(s => new { Key = s.Id, Value = s.Ficha, Copartimentos = s.Compartimentos });
      var cisternasDetalles = _unitOfWork.CisternaRepository.GetAllDetalle(cisternas.Select(s => s.Key));
      var choferes = _unitOfWork.ChoferRepository.GetByCliente(CurAppSessionData.ClienteId).Select(s => new KeyValue(s.Id, s.Nombre)); ;
      var sellos = _unitOfWork.SelloRepository.GetByClienteSinUsar(CurAppSessionData.ClienteId).Select(s => new KeyValue(s.Id, s.CodSello)); 
      var terminales = _unitOfWork.TerminalRepository.GetByCliente(CurAppSessionData.ClienteId).Select(s => new KeyValue(s.Id, s.Nombre)); ;
      var result = new
      {
        Model = new { Orden = orden, Detalle = detalle },
        Productos = productos,
        Camiones = camiones,
        Cisternas = cisternas,
        Choferes = choferes,
        Sellos = sellos,
        CisternaDetalles = cisternasDetalles,
        Terminales = terminales,
      };

      return Ok(result);
    }

    [HttpPost]
    public IActionResult Post([FromBody]OrdenEditDto model)
    {
      if (ModelState.IsValid)
      {
        EnsureCliente(model.Orden);
        FillUpdateable(model.Orden);
        _unitOfWork.OrdenRepository.SaveOrden(model);
        return Ok();
      }
      else
      {
        return BadRequest("invalid model");
      }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody]OrdenEditDto model)
    {
      if (ModelState.IsValid)
      {
        var oldOrden = _unitOfWork.OrdenRepository.GetById(id);
        if (oldOrden.Id != model.Orden.Id || oldOrden.ClienteId != model.Orden.ClienteId)
          throw new Exception("Datos no validos");

        _unitOfWork.AppDbContext.Entry(oldOrden).State = EntityState.Detached;

        EnsureCliente(model.Orden);
        FillUpdateable(model.Orden);
        _unitOfWork.OrdenRepository.SaveOrden(model);
        return Ok();
      }
      else
      {
        return BadRequest("invalid model");
      }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var result = _unitOfWork.OrdenRepository.All.FirstOrDefault(s => s.Id == id);
      if (result == null)
        return NotFound("entity not found");
      EnsureCliente(result);
      _unitOfWork.OrdenRepository.DeleteOrden(result);
      _unitOfWork.Save();
      return Ok();
    }

  }

}
