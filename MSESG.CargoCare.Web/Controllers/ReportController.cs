using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Reflection;
using MSESG.CargoCare.Core;
using MSESG.CargoCare.Core.EFServices;
using MSESG.CargoCare.Core.EFServices.Dto;
using Newtonsoft.Json;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report.Web;
using Stimulsoft.Report.Export;

namespace MSESG.CargoCare.Web.Controllers
{
  [AllowAnonymous]
  public class ReportController : BaseController
  {

    private readonly IHostingEnvironment _hostEnvironment;
    public ReportController(UnitOfWork uow, IHostingEnvironment hostEnvironment) : base(uow)
    {
      _hostEnvironment = hostEnvironment;

      //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
      //Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
      //Stimulsoft.Base.StiLicense.LoadFromStream(stream);
    }

    public IActionResult Index()
    {
      return View();
    }



    public IActionResult Test()
    {
      var dateInit = new DateTime(2018, 7, 5);
      var result = _unitOfWork.ActividadRepository.GetActividadesDiario(2, dateInit, null);
      return Ok(result);
    }

    public IActionResult ActividadesReport(DateTime? dateInit = null)
    {

      //var result = _unitOfWork.ActividadRepository.GetActividadesDiarioReport(CurAppSessionData.ClienteId, dateInit, null);
      //StiReport report = new StiReport();
  
      //var path = StiNetCoreHelper.MapPath(this, "Reportes\\actividadDia.mrt");
      //report.Load(path);
      //report.Dictionary.Databases.Clear();
      //var dsroot = Stimulsoft.Base.StiJsonToDataSetConverter.GetDataSet(JsonConvert.SerializeObject(result));
      //report.RegData("root", dsroot);
      return View();
    }


    public IActionResult GetActividadesReport(DateTime? dateInit = null, int? dir = null)
    {
      var result = _unitOfWork.ActividadRepository.GetActividadesDiarioReport(CurAppSessionData.ClienteId, dateInit, dir);
      StiReport report = new StiReport();

      var path = StiNetCoreHelper.MapPath(this, "Reportes//actividadDia.mrt");
      report.Load(path);
      report.Dictionary.Databases.Clear();
      var dsroot = Stimulsoft.Base.StiJsonToDataSetConverter.GetDataSet(JsonConvert.SerializeObject(result));
      report.RegData("root", dsroot);

      return StiNetCoreViewer.GetReportResult(this, report);
    }



    public IActionResult GetInspeccionReport(int id)
    {

      StiReport report = new StiReport();
      var result = _unitOfWork.InspeccionRepository.GetConduceById(id);
      var path = StiNetCoreHelper.MapPath(this, "Reportes\\conduce2.mrt");
      report.Load(path);
      report.Dictionary.Databases.Clear();
      var dsroot = Stimulsoft.Base.StiJsonToDataSetConverter.GetDataSet(JsonConvert.SerializeObject(result));
      report.RegData("root", dsroot);

      return StiNetCoreViewer.GetReportResult(this, report);
    }


    public IActionResult ViewerEvent()
    {
      return StiNetCoreViewer.ViewerEventResult(this);
    }


    public IActionResult Inspeccion(int id)
    {

      StiReport report = new StiReport();
      var result = _unitOfWork.InspeccionRepository.GetConduceById(id);

      var path = StiNetCoreHelper.MapPath(this, string.Format("Reportes/{0}", result.Inspeccion.TipoConsuce));
      report.Load(path);
      report.Dictionary.Databases.Clear();
      var dsroot = Stimulsoft.Base.StiJsonToDataSetConverter.GetDataSet(JsonConvert.SerializeObject(result));
      report.RegData("root", dsroot);

      // Some actions with report when exporting
      report.ReportName = CleanInput(result.Inspeccion.ClienteNombre) + "_Conduce_Numero_" + result.Inspeccion.Correlativo;
      report.ReportAlias = report.ReportName;
      report.Rendering += Report_Rendering;
      Response.ContentType = "application/pdf";
      var s = new StiPdfExportSettings();

      return StiNetCoreReportResponse.ResponseAsPdf(report, s, false);

    }

    private void Report_Rendering(object sender, EventArgs eventArgs)
    {
      var r = sender as StiReport;
      var c = r.GetComponents();

      foreach (var item in c)
      {
        var t = item as StiText;

        if (t != null && (t.Alias == "sello"))
        {

          t.GetValue += (o, e) => { e.Value = string.IsNullOrEmpty(e.Value) ? "" : e.Value.Replace("'", ""); };

        }
      }
    }


    public IActionResult Precarga(int id)
    {

      StiReport report = new StiReport();
      var result = _unitOfWork.PrecargaRepository.GetReportById(id);

      var path = StiNetCoreHelper.MapPath(this, "Reportes/precarga.mrt");
      report.Load(path);
      report.Dictionary.Databases.Clear();
      var dsroot = Stimulsoft.Base.StiJsonToDataSetConverter.GetDataSet(JsonConvert.SerializeObject(result));
      report.RegData("root", dsroot);

      // Some actions with report when exporting
      report.ReportName = CleanInput(result.Precarga.ClienteNombre) + "_Precarga_Numero_" + result.Precarga.Correlativo;
      report.ReportAlias = report.ReportName;
      Response.ContentType = "application/pdf";
      var s = new StiPdfExportSettings();

      return StiNetCoreReportResponse.ResponseAsPdf(report, s, false);

    }



    public IActionResult Planificacion(int id)
    {

      var planificacion = _unitOfWork.PlanificacionRepository.GetById(id);
      var detalle = _unitOfWork.PlanificacionRepository.GetDespachoDto(id);
      var result = new PlanificacionDto { Planificacion = planificacion, PlanificacionDespacho = detalle };
      var destinos = _unitOfWork.TerminalRepository.GetByClienteDestinos(CurAppSessionData.ClienteId).Select(s => new KeyValue(s.Id, s.Nombre));
      var terminales = _unitOfWork.TerminalRepository.GetByCliente(CurAppSessionData.ClienteId).Select(s => new KeyValue(s.Id, s.Nombre));
      var cliente = _unitOfWork.ClienteRepository.GetById(planificacion.ClienteId ?? 0);
      var empresa = _unitOfWork.EmpresaRepository.GetById(cliente.EmpresaId.Value);
      ViewData["destinos"] = destinos;
      ViewData["terminales"] = terminales;
      ViewData["cliente"] = cliente.Nombre;
      ViewData["empresa"] = empresa.Nombre;
      return View("testMail", result);
    }


    protected string GetReportDirectory()
    {
      var curDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "Reportes";
      return curDirectory;
    }

  }
}