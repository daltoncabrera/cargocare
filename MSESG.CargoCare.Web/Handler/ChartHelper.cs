using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using MSESG.CargoCare.Core.EFServices.Dto;

namespace MSESG.CargoCare.Web.Handler
{
  public class ChartHelper
  {
    private string _path = "";
    private int _counter = 20;
    private XElement _nodeClone;
    private XElement _document;
    private XElement _serieList;

    public ChartHelper(string path)
    {
      _path = path;
      _document = XElement.Load(path);
    }

    private void CleanDocument()
    {
      var chart = _document.Descendants().Where(s => s.Name.LocalName == "Page1").Descendants()
        .Where(s => s.Name.LocalName == "Chart1");

      _serieList = chart.Descendants().FirstOrDefault(s => s.Name.LocalName == "Series");
      _nodeClone = _serieList.Descendants().FirstOrDefault();
      _serieList.RemoveNodes();
    }

    private int GetSeed()
    {
      return ++_counter;
    }

    private XElement CloneElement(XElement xElement, string label, string arguments, string values)
    {
      var list = xElement.Descendants().Where(s => s.FirstAttribute != null && s.FirstAttribute.Name == "Ref");
      xElement.FirstAttribute.Value = $"{GetSeed()}{xElement.FirstAttribute.Value}";

      foreach (var refNode in list)
      {
        refNode.FirstAttribute.Value = $"{GetSeed()}{refNode.FirstAttribute.Value}";
      }

      var argumentNode = xElement.Descendants().FirstOrDefault(s => s.Name.LocalName == "ListOfArguments");
      var valueNode = xElement.Descendants().FirstOrDefault(s => s.Name.LocalName == "ListOfValues");
      var labelNode = xElement.Descendants().FirstOrDefault(s => s.Name.LocalName == "Title");

      labelNode.Value = label;
      argumentNode.Value = arguments;
      valueNode.Value = values;

      return new XElement(xElement);
    }

    public string GetReport(List<ChartReportDto> dtoList)
    {
      var result = "";
      CleanDocument();
      foreach (var data in dtoList)
      {
        var arguments = string.Join(";", data.Arguments);
        var values = string.Join(";", data.Values);
        var node = CloneElement(_nodeClone, data.Label, arguments, values);
        _serieList.Add(node);
      }

      return "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" + _document.ToString();
    }
  }
}
