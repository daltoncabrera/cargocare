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
  public class PrecargaController : BaseController
  {

    public PrecargaController(UnitOfWork uow) : base(uow)
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
      Precarga Precarga = null;
      if (id > 0)
      {
        Precarga = _unitOfWork.PrecargaRepository.Get(id) ?? new Precarga() { Fecha = DateTime.Now };
      }
      else
      {
        Precarga = new Precarga() { Fecha = DateTime.Now };
        var cliente = _unitOfWork.ClienteRepository.GetById(CurAppSessionData.ClienteId);
        Precarga.LlenaDetalle = cliente.LlenaDetalle;
        Precarga.ConduceTpl = cliente.ConduceTpl;
        if (planificacionId > 0)
        {
          var planificacion = _unitOfWork.PlanificacionRepository.GetById(planificacionId);
          Precarga.PlanificacionId = planificacion?.Id;
          Precarga.Planificacion = planificacion?.Correlativo;
        }

      }

      if (Precarga?.Id > 0)
        EnsureCliente(Precarga);

      var detalle = _unitOfWork.PrecargaRepository.GetDetail(Precarga.Id);


      if (detalle == null || !detalle.Any())
        detalle = _unitOfWork.PrecargaRepository.GetDetailMock(CurAppSessionData.ClienteId);

      var camiones = _unitOfWork.CamionRepository.GetByCliente(CurAppSessionData.ClienteId).Select(s => new { Key = s.Id, Value = s.Ficha, CisternaId = s.CisternaId, ChoferId = s.ChoferId });
      var cisternas = _unitOfWork.CisternaRepository.GetByCliente(CurAppSessionData.ClienteId).Select(s => new { Key = s.Id, Value = s.Ficha, Copartimentos = s.Compartimentos });
      var cisternasDetalles = _unitOfWork.CisternaRepository.GetAllDetalle(cisternas.Select(s => s.Key));
      var choferes = _unitOfWork.ChoferRepository.GetByCliente(CurAppSessionData.ClienteId).Select(s => new KeyValue(s.Id, s.Nombre)); ;
      var terminales = _unitOfWork.TerminalRepository.GetByCliente(CurAppSessionData.ClienteId).Select(s => new KeyValue(s.Id, s.Nombre)); ;
      var destinos = _unitOfWork.TerminalRepository.GetByClienteDestinos(CurAppSessionData.ClienteId).Select(s => new KeyValue(s.Id, s.Nombre)); ;
      var result = new
      {
        Model = new { Precarga = Precarga, Detalle = detalle },
        Camiones = camiones,
        Cisternas = cisternas,
        Choferes = choferes,
        CisternaDetalles = cisternasDetalles,
        Terminales = terminales,
        Destinos = destinos
      };

      return Ok(result);
    }

    [HttpPost]
    public IActionResult Post([FromBody]PrecargaEditDto model)
    {
      if (ModelState.IsValid)
      {
        EnsureCliente(model.Precarga);
        FillUpdateable(model.Precarga);
        _unitOfWork.PrecargaRepository.SavePrecarga(model);
        return Ok(model);
      }
      else
      {
        return BadRequest("invalid model");
      }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody]PrecargaEditDto model)
    {
      if (ModelState.IsValid)
      {
        var oldPrecarga = _unitOfWork.PrecargaRepository.GetById(id);
        if (oldPrecarga.Id != model.Precarga.Id || oldPrecarga.ClienteId != model.Precarga.ClienteId)
          throw new Exception("Datos no validos");

        _unitOfWork.AppDbContext.Entry(oldPrecarga).State = EntityState.Detached;

        EnsureCliente(model.Precarga);
        FillUpdateable(model.Precarga);
        _unitOfWork.PrecargaRepository.SavePrecarga(model);
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
      var result = _unitOfWork.PrecargaRepository.All.FirstOrDefault(s => s.Id == id);
      if (result == null)
        return NotFound("entity not found");
      EnsureCliente(result);
      _unitOfWork.PrecargaRepository.DeletePrecarga(result);
      _unitOfWork.Save();
      return Ok();
    }

  }

}
