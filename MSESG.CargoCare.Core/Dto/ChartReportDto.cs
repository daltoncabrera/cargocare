using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSESG.CargoCare.Core.EFServices.Dto
{
    public class ChartReportDto
    {
      public ChartReportDto()
      {
        Arguments = new List<string>();
        Values = new List<string>();
      }

      public string Label { get; set; }
      public List<string> Arguments { get; set; }
      public List<string> Values { get; set; }

    }
}
