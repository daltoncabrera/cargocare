using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.Dto;
using MSESG.CargoCare.Core.EFServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSESG.CargoCare.Web.Controllers
{
  [Route("api/[controller]")]
  public class VerificacionController : BaseController
  {

    public VerificacionController(UnitOfWork uow) : base(uow)
    {
    }



    [HttpGet]
    public IActionResult Get()
    {
      var result = _unitOfWork.VerificacionRepository.GetByCliente(CurAppSessionData.ClienteId);
      return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var model = _unitOfWork.VerificacionRepository.GetById(id) ?? new VerificacionDto() { Verificacion = new Verificacion(), Detalle = new ItemsVerificacion() };
      var camiones = _unitOfWork.CamionRepository.GetByCliente(CurAppSessionData.ClienteId).Select(s => new { Key = s.Id, Value = s.Ficha, CisternaId = s.CisternaId, ChoferId = s.ChoferId });
      var cisternas = _unitOfWork.CisternaRepository.GetByCliente(CurAppSessionData.ClienteId).Select(s => new { Key = s.Id, Value = s.Ficha, Copartimentos = s.Compartimentos });
      var choferes = _unitOfWork.ChoferRepository.GetByCliente(CurAppSessionData.ClienteId).Select(s => new KeyValue(s.Id, s.Nombre)); ;

      return Ok(new { model, choferes, camiones, cisternas });
    }

    [HttpPost]
    public IActionResult Post([FromBody]VerificacionDto model)
    {
      if (ModelState.IsValid)
      {
        model.Verificacion.InspectorId = CurAppSessionData.UserId;
        FillUpdateable(model.Verificacion);
        _unitOfWork.VerificacionRepository.Save(model);
        _unitOfWork.Save();
        return Ok();
      }
      else
      {
        return BadRequest("invalid model");
      }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody]VerificacionDto model)
    {
      if (ModelState.IsValid)
      {
        EnsureCliente(model.Verificacion);
        FillUpdateable(model.Verificacion);

        _unitOfWork.VerificacionRepository.Save(model);
        _unitOfWork.Save();
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
      var result = _unitOfWork.VerificacionRepository.All.FirstOrDefault(s => s.Id == id);
      if (result == null)
        return NotFound("entity not found");

      EnsureCliente(result);

      _unitOfWork.VerificacionRepository.Delete(result);
      _unitOfWork.Save();
      return Ok();
    }

  }

}
