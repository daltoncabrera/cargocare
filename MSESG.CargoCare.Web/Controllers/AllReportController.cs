using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MSESG.CargoCare.Core.EFServices;
using Newtonsoft.Json;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using MSESG.CargoCare.Web.Handler;
using RazorLight.Extensions;
using Stimulsoft.Report.Chart;

namespace MSESG.CargoCare.Web.Controllers
{
  [AllowAnonymous]
  public class AllReport : BaseController
  {

    private readonly IHostingEnvironment _hostEnvironment;
    public AllReport(UnitOfWork uow, IHostingEnvironment hostEnvironment) : base(uow)
    {
      _hostEnvironment = hostEnvironment;
    }

    public IActionResult Test()
    {
      var path = StiNetCoreHelper.MapPath(this, "Reportes//prueba.mrt");


      var result = _unitOfWork.ProductoRepository.GetReport(1, DateTime.Now.AddYears(-5), DateTime.Now);
      return Ok(result);
    }

    public IActionResult General(string name, DateTime? dateInit = null, DateTime? dateEnd = null, string tipo = null, string periodo = null, string grupo = null)
    {
      ViewData["name"] = name;
      return View();
    }


    public IActionResult GetProductsReport(string name, DateTime? dateInit = null, DateTime? dateEnd = null, string tipo = null, string periodo = null, string grupo = null)
    {
      StiReport report = new StiReport();
      var path = "";
      if (tipo == "listado")
      {

        #region  Por Dia
        if (periodo == "dia")
        {
          string title = $"Movimiento de productos por Día: desde {dateInit:dd/MM/yyy} hasta {dateEnd:dd/MM/yyy}, agrupado por {grupo}";
          var result = _unitOfWork.ProductoRepository.GetReport(CurAppSessionData.ClienteId, dateInit, dateEnd, title);
          if (grupo == "fecha")
          {

            path = StiNetCoreHelper.MapPath(this, "Reportes//productos//productosDiaPorFecha.mrt");
            report.Load(path);
            report.Dictionary.Databases.Clear();
            var dsroot = Stimulsoft.Base.StiJsonToDataSetConverter.GetDataSet(JsonConvert.SerializeObject(result));
            report.RegData("root", dsroot);
            return StiNetCoreViewer.GetReportResult(this, report);

          }

          if (grupo == "destino")
          {
            path = StiNetCoreHelper.MapPath(this, "Reportes//productos//productosDiaPorDestino.mrt");

            report.Load(path);
            report.Dictionary.Databases.Clear();
            var dsroot = Stimulsoft.Base.StiJsonToDataSetConverter.GetDataSet(JsonConvert.SerializeObject(result));
            report.RegData("root", dsroot);
            return StiNetCoreViewer.GetReportResult(this, report);
          }

          if (grupo == "producto")
          {
            path = StiNetCoreHelper.MapPath(this, "Reportes//productos//productosDiaPorProducto.mrt");
            report.Load(path);
            report.Dictionary.Databases.Clear();
            var dsroot = Stimulsoft.Base.StiJsonToDataSetConverter.GetDataSet(JsonConvert.SerializeObject(result));
            report.RegData("root", dsroot);
            return StiNetCoreViewer.GetReportResult(this, report);
          }

        }

        #endregion

        #region  Por Mes
        if (periodo == "mes")
        {
          string title = $"Movimiento de productos por Mes: desde {dateInit:dd/MM/yyy} hasta {dateEnd:dd/MM/yyy}, agrupado por {grupo}";
          var result = _unitOfWork.ProductoRepository.GetReportMes(CurAppSessionData.ClienteId, dateInit, dateEnd, title);
          if (grupo == "fecha")
          {
            path = StiNetCoreHelper.MapPath(this, "Reportes//productos//productosMesPorFecha.mrt");
            report.Load(path);
            report.Dictionary.Databases.Clear();
            var dsroot = Stimulsoft.Base.StiJsonToDataSetConverter.GetDataSet(JsonConvert.SerializeObject(result));
            report.RegData("root", dsroot);
            return StiNetCoreViewer.GetReportResult(this, report);
          }

          if (grupo == "destino")
          {
            path = StiNetCoreHelper.MapPath(this, "Reportes//productos//productosMesPorDestino.mrt");
            report.Load(path);
            report.Dictionary.Databases.Clear();
            var dsroot = Stimulsoft.Base.StiJsonToDataSetConverter.GetDataSet(JsonConvert.SerializeObject(result));
            report.RegData("root", dsroot);
            return StiNetCoreViewer.GetReportResult(this, report);
          }

          if (grupo == "producto")
          {
            path = StiNetCoreHelper.MapPath(this, "Reportes//productos//productosMesPorProducto.mrt");
            report.Load(path);
            report.Dictionary.Databases.Clear();
            var dsroot = Stimulsoft.Base.StiJsonToDataSetConverter.GetDataSet(JsonConvert.SerializeObject(result));
            report.RegData("root", dsroot);
            return StiNetCoreViewer.GetReportResult(this, report);
          }

        }

        #endregion

        #region  Por Anio
        if (periodo == "anio")
        {
          string title = $"Movimiento de productos por Año: desde {dateInit:dd/MM/yyy} hasta {dateEnd:dd/MM/yyy}, agrupado por {grupo}";
          var result = _unitOfWork.ProductoRepository.GetReportAnio(CurAppSessionData.ClienteId, dateInit, dateEnd, title);
          if (grupo == "fecha")
          {
            path = StiNetCoreHelper.MapPath(this, "Reportes//productos//productosAnioPorFecha.mrt");
            report.Load(path);
            report.Dictionary.Databases.Clear();
            var dsroot = Stimulsoft.Base.StiJsonToDataSetConverter.GetDataSet(JsonConvert.SerializeObject(result));
            report.RegData("root", dsroot);
            return StiNetCoreViewer.GetReportResult(this, report);
          }

          if (grupo == "destino")
          {
            path = StiNetCoreHelper.MapPath(this, "Reportes//productos//productosAnioPorDestino.mrt");
            report.Load(path);
            report.Dictionary.Databases.Clear();
            var dsroot = Stimulsoft.Base.StiJsonToDataSetConverter.GetDataSet(JsonConvert.SerializeObject(result));
            report.RegData("root", dsroot);
            return StiNetCoreViewer.GetReportResult(this, report);
          }

          if (grupo == "producto")
          {
            path = StiNetCoreHelper.MapPath(this, "Reportes//productos//productosAnioPorProducto.mrt");
            report.Load(path);
            report.Dictionary.Databases.Clear();
            var dsroot = Stimulsoft.Base.StiJsonToDataSetConverter.GetDataSet(JsonConvert.SerializeObject(result));
            report.RegData("root", dsroot);
            return StiNetCoreViewer.GetReportResult(this, report);
          }

        }

        #endregion

      }
      else if (tipo == "grafico")
      {

        #region  Por Dia
        if (periodo == "dia")
        {
          string title = $"Movimiento de productos por Día: desde {dateInit:dd/MM/yyy} hasta {dateEnd:dd/MM/yyy}, agrupado por {grupo}";
          var result = _unitOfWork.ProductoRepository.GetReportGrafico(CurAppSessionData.ClienteId, dateInit, dateEnd);
          if (grupo == "fecha")
          {

            path = StiNetCoreHelper.MapPath(this, "Reportes//prueba.mrt");
            var charHelper = new ChartHelper(path);
            var xmlReport = charHelper.GetReport(result);
            report.LoadFromString(xmlReport);
            return StiNetCoreViewer.GetReportResult(this, report);

          }

          if (grupo == "destino")
          {

            path = StiNetCoreHelper.MapPath(this, "Reportes//prueba.mrt");
            var charHelper = new ChartHelper(path);
            var xmlReport = charHelper.GetReport(result);
            report.LoadFromString(xmlReport);
            return StiNetCoreViewer.GetReportResult(this, report);
          }

          if (grupo == "producto")
          {

            path = StiNetCoreHelper.MapPath(this, "Reportes//prueba.mrt");
            var charHelper = new ChartHelper(path);
            var xmlReport = charHelper.GetReport(result);
            report.LoadFromString(xmlReport);
            return StiNetCoreViewer.GetReportResult(this, report);
          }

        }

        #endregion

        #region  Por Mes
        if (periodo == "mes")
        {
          string title = $"Movimiento de productos por Día: desde {dateInit:dd/MM/yyy} hasta {dateEnd:dd/MM/yyy}, agrupado por {grupo}";
          var result = _unitOfWork.ProductoRepository.GetReportGraficoMes(CurAppSessionData.ClienteId, dateInit, dateEnd);
          if (grupo == "fecha")
          {

            path = StiNetCoreHelper.MapPath(this, "Reportes//prueba.mrt");
            var charHelper = new ChartHelper(path);
            var xmlReport = charHelper.GetReport(result);
            report.LoadFromString(xmlReport);
            return StiNetCoreViewer.GetReportResult(this, report);

          }

          if (grupo == "destino")
          {

            path = StiNetCoreHelper.MapPath(this, "Reportes//prueba.mrt");
            var charHelper = new ChartHelper(path);
            var xmlReport = charHelper.GetReport(result);
            report.LoadFromString(xmlReport);
            return StiNetCoreViewer.GetReportResult(this, report);
          }

          if (grupo == "producto")
          {

            path = StiNetCoreHelper.MapPath(this, "Reportes//prueba.mrt");
            var charHelper = new ChartHelper(path);
            var xmlReport = charHelper.GetReport(result);
            report.LoadFromString(xmlReport);
            return StiNetCoreViewer.GetReportResult(this, report);
          }

        }

        #endregion

        #region  Por Anio
        if (periodo == "anio")
        {
          string title = $"Movimiento de productos por Día: desde {dateInit:dd/MM/yyy} hasta {dateEnd:dd/MM/yyy}, agrupado por {grupo}";
          var result = _unitOfWork.ProductoRepository.GetReportGraficoAnio(CurAppSessionData.ClienteId, dateInit, dateEnd);
          if (grupo == "fecha")
          {

            path = StiNetCoreHelper.MapPath(this, "Reportes//prueba.mrt");
            var charHelper = new ChartHelper(path);
            var xmlReport = charHelper.GetReport(result);
            report.LoadFromString(xmlReport);
            return StiNetCoreViewer.GetReportResult(this, report);

          }

          if (grupo == "destino")
          {

            path = StiNetCoreHelper.MapPath(this, "Reportes//prueba.mrt");
            var charHelper = new ChartHelper(path);
            var xmlReport = charHelper.GetReport(result);
            report.LoadFromString(xmlReport);
            return StiNetCoreViewer.GetReportResult(this, report);
          }

          if (grupo == "producto")
          {

            path = StiNetCoreHelper.MapPath(this, "Reportes//prueba.mrt");
            var charHelper = new ChartHelper(path);
            var xmlReport = charHelper.GetReport(result);
            report.LoadFromString(xmlReport);
            return StiNetCoreViewer.GetReportResult(this, report);
          }
        }

        #endregion
      }


      return null;
    }


    public IActionResult ViewerEvent()
    {
      return StiNetCoreViewer.ViewerEventResult(this);
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








    protected string GetReportDirectory()
    {
      var curDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + "Reportes";
      return curDirectory;
    }

  }
}