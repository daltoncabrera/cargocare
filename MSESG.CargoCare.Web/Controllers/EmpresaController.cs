using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Mvc;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices;
using MSESG.CargoCare.Web.Handler;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSESG.CargoCare.Web.Controllers
{
  [Route("api/[controller]")]
  public class EmpresaController : BaseController
  {

    public EmpresaController(UnitOfWork uow)
            : base(uow)
    {
    }

    [HttpGet]
    public IActionResult Get()
    {
      var result = _unitOfWork.EmpresaRepository.All.OrderByDescending(s => s.CreatedDate);
      return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var result = _unitOfWork.EmpresaRepository.All.FirstOrDefault(s => s.Id == id);
      var empresaSettings = _unitOfWork.EmpresaRepository.GetSettings(id) ?? new EmpresaSetting();
      return Ok(new { Empresa = result, Settings = empresaSettings });
    }

    [HttpPost]
    public IActionResult Post([FromBody]Empresa empresa)
    {
      if (ModelState.IsValid)
      {
        _unitOfWork.EmpresaRepository.Insert(empresa);
        empresa.Slug = CleanInput(empresa.Nombre);
        _unitOfWork.Save();
        return Ok(empresa);
      }
      else
      {
        return BadRequest("invalid model");
      }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody]Empresa empresa)
    {
      if (ModelState.IsValid)
      {
        empresa.Slug = CleanInput(empresa.Nombre);
        _unitOfWork.EmpresaRepository.Update(empresa);
        _unitOfWork.Save();
        return Ok(empresa);
      }
      else
      {
        return BadRequest("invalid model");
      }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var result = _unitOfWork.EmpresaRepository.All.FirstOrDefault(s => s.Id == id);
      if (result == null)
        return NotFound("entity not found");

      _unitOfWork.EmpresaRepository.Delete(result);
      _unitOfWork.Save();
      return Ok();
    }

    [HttpPost()]
    [Route("Upload")]
    public async Task<IActionResult> Upload(int empresaId)
    {
      var files = Request?.Form?.Files;
      var curFile = files?.FirstOrDefault();
      if (curFile != null)
      {
        var extension = "";
        if (!string.IsNullOrEmpty(curFile.FileName))
          extension = Path.GetExtension(curFile.FileName);
        var sysFileName = string.Format("{0}_{1}{2}", curFile.FileName, Guid.NewGuid(), extension);
        var filePath = Path.Combine(GetFilesDirectory(), sysFileName);


        if (System.IO.File.Exists(filePath))
          return BadRequest("archivo existe");

        var empresa = _unitOfWork.EmpresaRepository.GetById(empresaId);

        if (empresa == null)
          return BadRequest("Cliente no existe");

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
          await curFile.CopyToAsync(stream);
          if (!string.IsNullOrWhiteSpace(empresa.Logo))
          {
            var oldPath = Path.Combine(GetFilesDirectory(), empresa.Logo);
            if (System.IO.File.Exists(filePath))
              System.IO.File.Delete(empresa.Logo);
          }

          empresa.Logo = sysFileName;
          _unitOfWork.EmpresaRepository.Update(empresa);
          _unitOfWork.Save();
        }

        return Ok(sysFileName);

      }

      return BadRequest();
    }


    [HttpPost()]
    [Route("SaveSettings")]
    public IActionResult SaveSettings([FromBody]EmpresaSetting settings)
    {
      if (!_unitOfWork.EmpresaRepository.IsValidEmpresa(settings.EmpresaId.Value))
        return BadRequest("Empresa no existe");

      FillUpdateable(settings, false);

      _unitOfWork.EmpresaRepository.SaveSettings(settings);
      return Ok(settings);
    }

    [HttpPost()]
    [Route("Sendtest/{id}")]
    public async Task<IActionResult> Sendtest(int id)
    {
      if (!_unitOfWork.EmpresaRepository.IsValidEmpresa(id))
        return BadRequest("Empresa no existe");

      var empresaSettings = _unitOfWork.EmpresaRepository.GetSettings(id);
      if (empresaSettings == null || string.IsNullOrWhiteSpace(empresaSettings.EmailDomain) || empresaSettings.EmailPort <= 0 || string.IsNullOrWhiteSpace(empresaSettings.UserEmail) || string.IsNullOrWhiteSpace(empresaSettings.EmailPassword) || string.IsNullOrWhiteSpace(empresaSettings.FromEmail) || string.IsNullOrWhiteSpace(empresaSettings.CcEmail))
        return BadRequest("Favor configurar datos de envio de correo para la empresa llenando: Dominio SMTP, Puerto, Email, Password, Remitente y aquien enviar la copia CC Mail");

      var mailHandler = new MailHandler(empresaSettings);
      await mailHandler.Test();
      return Ok();
    }




  }
}
