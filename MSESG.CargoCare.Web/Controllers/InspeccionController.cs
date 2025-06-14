using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices;
using MSESG.CargoCare.Core.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSESG.CargoCare.Web.Controllers
{
  [Route("api/[controller]")]
  public class InspeccionController : BaseController
  {
    private readonly IHostingEnvironment _hostEnvironment;
    public InspeccionController(UnitOfWork uow, IHostingEnvironment hostEnvironment) : base(uow)
    {
      _hostEnvironment = hostEnvironment;
    }

    [HttpGet]
    public IActionResult Get()
    {
      var result = _unitOfWork.InspeccionRepository.GetByCliente(CurAppSessionData.ClienteId);
      return Ok(result);
    }

    [HttpGet]
    [Route("Ordenes")]
    public IActionResult Ordenes()
    {
      var result = _unitOfWork.PrecargaRepository.GetPenddingByClienteWithDetails(CurAppSessionData.ClienteId).OrderBy(s => s.PrecargaId);
      return Ok(result);
    }



    [HttpGet("{id}/{precargaId}")]
    public IActionResult Get(int id, int? precargaId)
    {
      if (precargaId.HasValue && precargaId.Value > 0)
      {
        var precarga = _unitOfWork.PrecargaRepository.GetById(precargaId.Value);
        EnsureCliente(precarga);
      }

      var cliente = _unitOfWork.ClienteRepository.GetById(CurAppSessionData.ClienteId);
      

      var inspeccion = precargaId > 0
        ? _unitOfWork.InspeccionRepository.GetByOrderId(precargaId.Value)
        : id > 0 ? _unitOfWork.InspeccionRepository.GetById(id)
        : new InspeccionEditDto() { Inspeccion = new Inspeccion() { FechaInicio = DateTime.Now, ConduceTpl = cliente.ConduceTpl, LlenaDetalle =  cliente.LlenaDetalle},
                                      Detalle = new List<InspeccionDetalle>() };

      if (inspeccion.Inspeccion.Id > 0)
        EnsureCliente(inspeccion.Inspeccion);

      // var detalle = orden?.Id > 0 ? _unitOfWork.InspeccionRepository.GetDetail(orden.Id) : new List<OrdenDetalle>();
      var productos = _unitOfWork.ProductoRepository.GetProductos().Select(s => new KeyValue(s.Id, s.Nombre));
      var camiones = _unitOfWork.CamionRepository.GetByCliente(CurAppSessionData.ClienteId).Select(s => new { Key = s.Id, Value = s.Ficha, CisternaId = s.CisternaId, ChoferId = s.ChoferId });
      var cisternas = _unitOfWork.CisternaRepository.GetByCliente(CurAppSessionData.ClienteId).Select(s => new { Key = s.Id, Value = s.Ficha, Copartimentos = s.Compartimentos });
      var cisternasDetalles = _unitOfWork.CisternaRepository.GetAllDetalle(cisternas.Select(s => s.Key));
      var choferes = _unitOfWork.ChoferRepository.GetByCliente(CurAppSessionData.ClienteId).Select(s => new KeyValue(s.Id, s.Nombre)); ;
      var sellos = _unitOfWork.SelloRepository.GetByClienteSinUsar(CurAppSessionData.ClienteId).Select(s => new KeyValue(s.Id, s.CodSello));
      var terminales = _unitOfWork.TerminalRepository.GetTerminales().Select(s => new KeyValue(s.Id, s.Nombre)); ;
      var destinos = _unitOfWork.TerminalRepository.GetByClienteDestinos(CurAppSessionData.ClienteId).Select(s => new KeyValue(s.Id, s.Nombre)); ;
      var result = new
      {
        Model = inspeccion,
        Productos = productos,
        Camiones = camiones,
        Cisternas = cisternas,
        Choferes = choferes,
        Sellos = sellos,
        CisternaDetalles = cisternasDetalles,
        Terminales = terminales,
        Destinos = destinos
      };

      return Ok(result);
    }

    [HttpPost]
    public IActionResult Post([FromBody]InspeccionEditDto model)
    {
      if (ModelState.IsValid)
      {
        model.Inspeccion.FechaFin = DateTime.Now.ToUniversalTime();
        if (model.Inspeccion.FechaInicio > model.Inspeccion.FechaFin)
          throw new Exception("Inicio / Fin Invalidos");

        FillUpdateable(model.Inspeccion);
        model.Inspeccion.InspectorId = CurAppSessionData.UserId;
        _unitOfWork.InspeccionRepository.SaveInspeccion(model);
        return Ok(new { Id = model.Inspeccion.Id });
      }
      else
      {
        return BadRequest("invalid model");
      }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody]InspeccionEditDto model)
    {
      if (ModelState.IsValid)
      {
        if (!HasClaims("sadmin"))
          return BadRequest("Solo los super administradores pueden editar inspecciones");

        EnsureCliente(model.Inspeccion);
        FillUpdateable(model.Inspeccion);
        _unitOfWork.InspeccionRepository.SaveInspeccion(model);
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
      var result = _unitOfWork.InspeccionRepository.All.FirstOrDefault(s => s.Id == id);
      if (result == null)
        return NotFound("entity not found");

      _unitOfWork.InspeccionRepository.Delete(result);
      _unitOfWork.Save();
      return Ok();
    }



  }

}
