using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices;
using MSESG.CargoCare.Core.EFServices.Dto;
using MSESG.CargoCare.Core.Entities;
using MSESG.CargoCare.Web.Handler;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSESG.CargoCare.Web.Controllers
{

  [Route("api/[controller]")]
  public class PlanificacionController : BaseController
  {
    private readonly IViewRenderService _viewRenderService;
    public PlanificacionController(UnitOfWork uow, IViewRenderService viewRenderService) : base(uow)
    {
      _viewRenderService = viewRenderService;
    }

    [HttpGet]
    public IActionResult Get()
    {
      var result = _unitOfWork.PlanificacionRepository.GetByClienteDto(CurAppSessionData.ClienteId).OrderByDescending(s => s.FechaInicio);
      return Ok(result);        
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var planificacion = _unitOfWork.PlanificacionRepository.All.FirstOrDefault(s => s.Id == id);
      if (planificacion == null)
      {
        planificacion = new Planificacion();
        var monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
        planificacion.FechaInicio = monday;
        planificacion.FechaFin = monday.AddDays(6);
      }
      else
      {
        EnsureCliente(planificacion);
      }

      var detalle = _unitOfWork.PlanificacionRepository.GetDespachoDto(id);
      var terminales = _unitOfWork.TerminalRepository.GetByCliente(CurAppSessionData.ClienteId).Select(s => new KeyValue(s.Id, s.Nombre));
      var destinos = _unitOfWork.TerminalRepository.GetByClienteDestinos(CurAppSessionData.ClienteId).Select(s => new KeyValue(s.Id, s.Nombre));
      return Ok(new { model = new PlanificacionDto() { Planificacion = planificacion, PlanificacionDespacho = detalle }, terminales, destinos });
    }

    [HttpGet()]
    [Route("GetPrecargas/{id}")]
    public IActionResult GetPrecargas(int id)
    {
      var result = _unitOfWork.PrecargaRepository.GetByCliente(CurAppSessionData.ClienteId).Where(s => s.PlanificacionId == id).OrderByDescending(s => s.PrecargaId);
      return Ok(result);
    }

    [HttpPost]
    public IActionResult Post([FromBody]PlanificacionDto model)
    {
      if (ModelState.IsValid)
      {
        FillUpdateable(model.Planificacion);
        _unitOfWork.PlanificacionRepository.GuardarPlanificacion(model);
        _unitOfWork.Save();
        return Ok();
      }
      else
      {
        return BadRequest("invalid model");
      }
    }

    [HttpPut("{id}")]
    public IActionResult put(int id, [FromBody]PlanificacionDto model)
    {
      if (ModelState.IsValid)
      {
        EnsureCliente(model.Planificacion);
        FillUpdateable(model.Planificacion);
        _unitOfWork.PlanificacionRepository.GuardarPlanificacion(model);

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

      var result = _unitOfWork.PlanificacionRepository.GetById(id);
      if (result == null)
        return NotFound("entity not found");

      EnsureCliente(result);
      _unitOfWork.PlanificacionRepository.EliminarPlanificacion(result);
      _unitOfWork.Save();
      return Ok();
    }



    [HttpPost()]
    [Route("SendMail/{id}")]
    public async Task<IActionResult> SendMail(int id)
    {
   

      var empresaSettings = _unitOfWork.EmpresaRepository.GetSettings(CurAppSessionData.EmpresaId);
      if (empresaSettings == null || string.IsNullOrWhiteSpace(empresaSettings.EmailDomain) || empresaSettings.EmailPort <= 0 || string.IsNullOrWhiteSpace(empresaSettings.UserEmail) || string.IsNullOrWhiteSpace(empresaSettings.EmailPassword) || string.IsNullOrWhiteSpace(empresaSettings.FromEmail) || string.IsNullOrWhiteSpace(empresaSettings.CcEmail))
        return BadRequest("Favor configurar datos de envio de correo para la empresa llenando: Dominio SMTP, Puerto, Email, Password, Remitente y aquien enviar la copia CC Mail");

      var planificacion = _unitOfWork.PlanificacionRepository.GetById(id);
      var detalle = _unitOfWork.PlanificacionRepository.GetDespachoDto(id);
      var result = new PlanificacionDto { Planificacion = planificacion, PlanificacionDespacho = detalle };
      var destinos = _unitOfWork.TerminalRepository.GetByClienteDestinos(CurAppSessionData.ClienteId).Select(s => new KeyValue(s.Id, s.Nombre));
      var terminales = _unitOfWork.TerminalRepository.GetByCliente(CurAppSessionData.ClienteId).Select(s => new KeyValue(s.Id, s.Nombre));
      var cliente = _unitOfWork.ClienteRepository.GetById(planificacion.ClienteId ?? 0);
      var empresa = _unitOfWork.EmpresaRepository.GetById(cliente.EmpresaId.Value);
      result.Destinos = destinos;
      result.Terminales = terminales;
      result.Cliente = cliente.Nombre;
      result.Empresa = empresa.Nombre;
      var viewRendered = await _viewRenderService.RenderToStringAsync("testMail", result);
      var mailHandler = new MailHandler(empresaSettings);
      var resp = await mailHandler.SendPlanificacion(viewRendered);
      if (resp.Successful)
        return Ok();
      else
        return BadRequest(resp.ErrorMessages);
    }
  }

}
