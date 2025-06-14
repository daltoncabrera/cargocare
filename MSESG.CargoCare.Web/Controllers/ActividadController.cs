using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.Dto;
using MSESG.CargoCare.Core.EFServices;
using MSESG.CargoCare.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSESG.CargoCare.Web.Controllers
{
  [Route("api/[controller]")]
  public class ActividadController : BaseController
  {
    public ActividadController(UnitOfWork uow) : base(uow)
    {
    }

    [HttpGet]
    [Route("GetDay")]
    public IActionResult GetDay(DateTime? dateInit, int? dir = null)
    {
      var result = _unitOfWork.ActividadRepository.GetActividadesDiario(CurAppSessionData.ClienteId, dateInit, dir);
   
      return Ok(result);
    }



    [HttpGet]
    [Route("GetDateInfo")]
    public IActionResult GetDateInfo(DateTime? dateInit, int? dir = null)
    {
      var result = _unitOfWork.ActividadRepository.GetDateInfo(CurAppSessionData.ClienteId, dateInit, dir);

      return Ok(result);
    }

    [HttpGet]
    [Route("GetWeek")]
    public IActionResult GetWeek(DateTime? dateInit)
    {
      var result = _unitOfWork.ActividadRepository.GetActividadesSemana(dateInit);
      return Ok(result);
    }

    [HttpGet]
    [Route("GetMonth")]
    public IActionResult GetMonth(DateTime? dateInit, int? dir = null)
    {
      var result = _unitOfWork.ActividadRepository.GetActividadesMes(CurAppSessionData.ClienteId, dateInit, dir);
      return Ok(result);
    }

    [HttpGet]
    [Route("GetRango")]
    public IActionResult GetRango(DateTime? dateInit, DateTime? dateEnd)
    {
      var result = _unitOfWork.ActividadRepository.GetActividadesRango(dateInit, dateEnd);
      return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var result = _unitOfWork.ActividadRepository.All.FirstOrDefault(s => s.Id == id);
      return Ok(result);
    }

    [HttpPost]
    [Route("AddObservation")]
    public IActionResult AddObservation([FromBody]ObservacionDto observacion)
    {
      if (ModelState.IsValid)
      {
        var o = new Observacion()
        {
          EmpresaId = observacion.EmpresaId,
          ClienteId = observacion.ClienteId,
          ActividadId = observacion.ActividadId,
          InspectorId = CurAppSessionData.UserId,
          Nota = observacion.Nota,
          Fecha = observacion.Fecha,
          Type = observacion.Type,
        };
        FillUpdateable(o, true);
        _unitOfWork.ActividadRepository.AddObservation(o);
        return Ok();
      }
      else
      {
        return BadRequest("invalid model");
      }
    }

    [HttpPost]
    [Route("DeleteObservation")]
    public IActionResult DeleteObservation([FromBody]ObservacionDto objservacion)
    {
      if (ModelState.IsValid)
      {
        var o = _unitOfWork.ActividadRepository.GetObservationById(objservacion.Id);
        EnsureCliente(o);
        _unitOfWork.ActividadRepository.DeleteObservation(o);
        _unitOfWork.Save();
        return Ok();
      }
      else
      {
        return BadRequest("invalid model");
      }
    }

    [HttpPost]
    [Route("CheckActivity")]
    public IActionResult CheckActivity(bool flagVisible, [FromBody]ActividadDto model)
    {
      if (ModelState.IsValid)
      {

        _unitOfWork.ActividadRepository.CheckActivity(flagVisible, model, CurAppSessionData);
        return Ok();
      }
      else
      {
        return BadRequest("invalid model");
      }
    }



    [HttpPost]
    public IActionResult Post([FromBody]Actividad Actividad)
    {
      if (ModelState.IsValid)
      {
        _unitOfWork.ActividadRepository.Insert(Actividad);
        _unitOfWork.Save();
        return Ok(Actividad);
      }
      else
      {
        return BadRequest("invalid model");
      }
    }




    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody]Actividad Actividad)
    {
      if (ModelState.IsValid)
      {
        _unitOfWork.ActividadRepository.Update(Actividad);
        _unitOfWork.Save();
        return Ok(Actividad);
      }
      else
      {
        return BadRequest("invalid model");
      }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var result = _unitOfWork.ActividadRepository.All.FirstOrDefault(s => s.Id == id);
      if (result == null)
        return NotFound("entity not found");

      _unitOfWork.ActividadRepository.Delete(result);
      _unitOfWork.Save();
      return Ok();
    }

  }
}
