using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.Dto;
using MSESG.CargoCare.Core.EFServices;
using MSESG.CargoCare.Core.Entities;
using Stimulsoft.System.Web;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSESG.CargoCare.Web.Controllers
{
  [Route("api/[controller]")]
  public class ClienteController : BaseController
  {
    public ClienteController(UnitOfWork uow) : base(uow)
    {

    }

    [HttpGet]
    public IActionResult Get()
    {
      var result = _unitOfWork.ClienteRepository.All.OrderByDescending(s => s.CreatedDate);
      return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var result = id > 0
          ? _unitOfWork.ClienteRepository.All.FirstOrDefault(s => s.Id == id)
          : new Cliente();
      var empresas = _unitOfWork.EmpresaRepository.All.Select(s => new KeyValue(s.Id.ToString(), s.Nombre));

      var terminales = _unitOfWork.TerminalRepository.GetOwnedByCliente(id);
      var correos = _unitOfWork.CorreoRepository.GetByCliente(id);
      var productosCliente = _unitOfWork.ProductoRepository.GetByCliente(id);
      var productos = _unitOfWork.ProductoRepository.GetProductos().Select(s => new KeyValue(s.Id, s.Nombre));
     
      var conducestpl = GetConducesTpls()?.Select(s => new KeyValue(s, s));
      return Ok(new { Cliente = result, Empresas = empresas, Correos = correos, Productos = productos,  ProductosCliente = productosCliente, ConducesTpl = conducestpl});
    }



    [HttpPost]
    public IActionResult Post([FromBody]Cliente cliente)
    {
      if (ModelState.IsValid)
      {
        FillUpdateable(cliente, false);
        cliente.Slug = CleanInput(cliente.Nombre);
        _unitOfWork.ClienteRepository.Insert(cliente);
        _unitOfWork.Save();
        return Ok(cliente);
      }
      else
      {
        return BadRequest("invalid model");
      }
    }

    [HttpPost]
    [Route("AddToClient")]
    public IActionResult AddToClient([FromBody]ClienteProductoDto model)
    {
      if (ModelState.IsValid)
      {
        _unitOfWork.ProductoRepository.AddToCliente(model);
        var productos = _unitOfWork.ProductoRepository.GetByCliente(model.ClienteId);
        return Ok(productos);
      }
      else
      {
        return BadRequest("invalid model");
      }
    }

    [HttpPost]
    [Route("RemoveFromClient")]
    public IActionResult RemoveFromClient([FromBody]ClienteProductoDto model)
    {
      if (ModelState.IsValid)
      {
        _unitOfWork.ProductoRepository.RemoveFromCliente(model);
        var productos = _unitOfWork.ProductoRepository.GetByCliente(model.ClienteId);
        return Ok(productos);
      }
      else
      {
        return BadRequest("invalid model");
      }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody]Cliente cliente)
    {
      if (ModelState.IsValid)
      {
        FillUpdateable(cliente, false);
        cliente.Slug = CleanInput(cliente.Nombre);
        _unitOfWork.ClienteRepository.Update(cliente);
        _unitOfWork.Save();
        return Ok(cliente);
      }
      else
      {
        return BadRequest("invalid model");
      }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var result = _unitOfWork.ClienteRepository.All.FirstOrDefault(s => s.Id == id);
      if (result == null)
        return NotFound("entity not found");

      _unitOfWork.ClienteRepository.Delete(result);
      _unitOfWork.Save();
      return Ok();
    }

    [HttpPost]
    [Route("Upload")]
    public async Task<IActionResult> Upload(int clienteId)
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

        var cliente = _unitOfWork.ClienteRepository.GetById(clienteId);

        if (cliente == null)
          return BadRequest("Cliente no existe");

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
          await curFile.CopyToAsync(stream);
          if (!string.IsNullOrWhiteSpace(cliente.Logo))
          {
            var oldPath = Path.Combine(GetFilesDirectory(), cliente.Logo);
            if (System.IO.File.Exists(filePath))
              System.IO.File.Delete(cliente.Logo);
          }

          cliente.Logo = sysFileName;
          _unitOfWork.ClienteRepository.Update(cliente);
          _unitOfWork.Save();
        }

        return Ok(sysFileName);

      }

      return BadRequest();

    }

    [HttpPost]
    [HttpPost]
    [Route("DeleteMail")]
    public async Task<IActionResult> DeleteMail([FromBody]Correo correo)
    {
      var cliente = correo.ClienteId.Value;
      _unitOfWork.CorreoRepository.Delete(correo);
      _unitOfWork.Save();
      var correos = _unitOfWork.CorreoRepository.GetByCliente(cliente);
      return Ok(correos);
    }

    [HttpPost]
    [Route("UpdateMail")]
    public async Task<IActionResult> UpdateMail([FromBody]Correo correo)
    {
      var oldCorreo = _unitOfWork.CorreoRepository.GetByEmail(correo.Email);
      if (oldCorreo != null && oldCorreo.ClienteId == correo.ClienteId)
      {
        if (correo.Id <= 0)
          return BadRequest("Correro ya existe!");
        
      }

      FillUpdateable(correo, false);
      _unitOfWork.CorreoRepository.SaveCorreo(correo);
      var correos = _unitOfWork.CorreoRepository.GetByCliente(correo.ClienteId.Value);
      return Ok(correos);
    }

  }

}
