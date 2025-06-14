using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSESG.CargoCare.Web.Controllers
{
  [Route("api/[controller]")]
  public class TerminalController : BaseController
  {
    public TerminalController(UnitOfWork uow) : base(uow)
    {
    }

    [HttpGet]
    public IActionResult Get()
    {
      var result = _unitOfWork.TerminalRepository.GetTerminales();
      return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var result = _unitOfWork.TerminalRepository.All.FirstOrDefault(s => s.Id == id);
      return Ok(result);
    }


    [HttpGet]
    [Route("GetByCliente/{id}")]
    public IActionResult GetByCliente(int id)
    {
      var result = _unitOfWork.TerminalRepository.GetOwnedByCliente(id);
      return Ok(result);
    }

    [HttpPost]
    public IActionResult Post([FromBody]Terminal terminal)
    {
      if (ModelState.IsValid)
      {
        _unitOfWork.TerminalRepository.Insert(terminal);
        _unitOfWork.Save();
        return Ok(terminal);
      }
      else
      {
        return BadRequest("invalid model");
      }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody]Terminal terminal)
    {
      if (ModelState.IsValid)
      {
        _unitOfWork.TerminalRepository.Update(terminal);
        _unitOfWork.Save();
        return Ok(terminal);
      }
      else
      {
        return BadRequest("invalid model");
      }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var result = _unitOfWork.TerminalRepository.All.FirstOrDefault(s => s.Id == id);
      if (result == null)
        return NotFound("entity not found");

      _unitOfWork.TerminalRepository.Delete(result);
      _unitOfWork.Save();
      return Ok();
    }

  }
}
