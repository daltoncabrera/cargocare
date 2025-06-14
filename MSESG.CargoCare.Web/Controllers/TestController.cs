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
  public class TestController : Controller
  {
    //public IJsReportMVCService JsReportMVCService { get; }
    protected UnitOfWork _unitOfWork;
    protected AppSessionData CurAppSessionData;
    private readonly IHostingEnvironment _hostEnvironment;
    public TestController(UnitOfWork uow, IHostingEnvironment hostEnvironment) 
    {
      _hostEnvironment = hostEnvironment;
     /// JsReportMVCService = jsReportMVCService;
      _unitOfWork = uow;
    }

    [HttpGet]
    public IActionResult Get()
    {
      var dateInit = new DateTime(2018,7,5);
      var result = _unitOfWork.ActividadRepository.GetActividadesDiario(CurAppSessionData.ClienteId, dateInit, null);
      return Ok(result);
    }

    [HttpGet]
    [Route("Ordenes")]
    public IActionResult Ordenes()
    {
      var result = _unitOfWork.PrecargaRepository.GetPenddingByCliente(CurAppSessionData.ClienteId).OrderByDescending(s => s.PrecargaId);
      return Ok(result);
    }



    [HttpGet()]
    [Route("GetInspeccion/{id}")]
    public IActionResult GetInspeccion(int id)
    {
      var inspeccion = _unitOfWork.InspeccionRepository.GetConduceById(id);
    

      return Ok(inspeccion);
    }

    [HttpGet()]
    [Route("GetJson/{id}")]
    public IActionResult GetJson(int id)
    {
      var inspeccion = _unitOfWork.PrecargaRepository.GetReportById(id);


      return Ok(inspeccion);
    }


  }

}
