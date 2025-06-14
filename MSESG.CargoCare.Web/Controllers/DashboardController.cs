using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices;
using MSESG.CargoCare.Core.EFServices.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSESG.CargoCare.Web.Controllers
{
  [Route("api/[controller]")]
  public class DashboardController : BaseController
  {

    public DashboardController(UnitOfWork uow) : base(uow)
    {

    }

    [HttpGet]
    public IActionResult Get()
    {
      return Ok();
    }

    [HttpGet]
    [Route("GetClientes")]
    public IActionResult GetClientes()
    {
      var clientes = _unitOfWork.ClienteRepository.GetClientes();
      return Ok(clientes);
    }

    [HttpGet]
    [Route("GetOrdenes")]
    public IActionResult GetOrdenes()
    {
      var curCliente = CurAppSessionData?.ClienteId;
      if (curCliente > 0 && HasClaims("sadmin", "supervisor", "inspector"))
      {
        var ordenes = _unitOfWork.PrecargaRepository.GetByCliente(curCliente);
        return Ok(ordenes);
      }

      return Ok();
    }

    [HttpGet]
    [Route("GetPenddingOrders")]
    public IActionResult GetPenddingOrders()
    {
      var curCliente = CurAppSessionData?.ClienteId;
      if (curCliente > 0 && HasClaims("sadmin", "supervisor", "inspector"))
      {
        var ordenes = _unitOfWork.PrecargaRepository.GetPenddingByCliente(curCliente);
        return Ok(ordenes);
      }

      return Ok();
    }

    [HttpGet]
    [Route("GetTerminales")]
    public IActionResult GetTerminales()
    {
      if (HasClaims("sadmin", "supervisor", "inspector"))
      {
        var terminales = _unitOfWork.TerminalRepository.GetTerminales();
        return Ok(terminales);
      }

      return Ok();
    }

    [HttpGet]
    [Route("GetProductos")]
    public IActionResult GetProductos()
    {
      if (HasClaims("sadmin", "supervisor", "inspector"))
      {
        var productos = _unitOfWork.ProductoRepository.GetProductos();
        return Ok(productos);
      }

      return Ok();
    }

    [HttpGet]
    [Route("GetUsuarios")]
    public IActionResult GetUsuarios()
    {
      if (HasClaims("sadmin", "supervisor", "inspector"))
      {
        var usuarios = _unitOfWork.UsuarioService.GetUsuarios();
        return Ok(usuarios);
      }

      return Ok();
    }


    [HttpGet]
    [Route("GetCamiones")]
    public IActionResult GetCamiones()
    {
      var curCliente = CurAppSessionData?.ClienteId;
      if (curCliente > 0)
      {
        var camiones = _unitOfWork.CamionRepository.GetByCliente(curCliente.Value);
        return Ok(camiones);
      }
      else
      {
        return Ok();
      }

    }

    [HttpGet]
    [Route("GetCisternas")]
    public IActionResult GetCisternas()
    {
     
      var curCliente = CurAppSessionData?.ClienteId;
       if (curCliente > 0)
      {
        var cisternas = _unitOfWork.CisternaRepository.GetByCliente(curCliente.Value);
        return Ok(cisternas);
      }
      else
      {
        return Ok();
      }
    }

    [HttpGet]
    [Route("GetChoferes")]
    public IActionResult GetChoferes()
    {
   
      var curCliente = CurAppSessionData?.ClienteId;
       if (curCliente > 0)
      {
        var choferes = _unitOfWork.ChoferRepository.GetByCliente(curCliente.Value);
        return Ok(choferes);
      }
      else
      {
        return Ok();
      }
    }

    [HttpGet]
    [Route("GetSellos")]
    public IActionResult GetSellos(int page = 1, int pageSize = 10, int totalItems = 0, int column = 0, string filter = "", int sortColumn = 0, SortDir dir = SortDir.Ascending, SelloStatus estatus = SelloStatus.None)
    {

      var curCliente = CurAppSessionData?.ClienteId;
       if (curCliente > 0)
      {
        var objFilter = new FilterModel(page, pageSize, totalItems, column, filter, sortColumn, dir, estatus);

        var result = _unitOfWork.SelloRepository
          .GetSellos(CurAppSessionData.ClienteId, objFilter)
          .Select(s => new SellosDto
          {
            Id = s.Id,
            CodSello = s.CodSello,
            Lote = s.Lote,
            SelloStatusId = ((int) s.SelloStatus).ToString(),
            SelloStatus = CoreUtils.GetEnumDescription<SelloStatus>(s.SelloStatus),
            CreatedDate = s.CreatedDate,

          });


        return Ok(new {Sellos = result, Filter = objFilter});
      }

      return Ok();
    }



    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      return Ok();
    }

  }

}
