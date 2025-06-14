using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core
{
  public static class DateExtensions
  {

    public static DateTime FirstDayOfMonth(this DateTime value)
    {
      return new DateTime(value.Year, value.Month, 1);
    }

    public static int DaysInMonth(this DateTime value)
    {
      return DateTime.DaysInMonth(value.Year, value.Month);
    }

    public static DateTime LastDayOfMonth(this DateTime value)
    {
      return new DateTime(value.Year, value.Month, value.DaysInMonth());
    }
  }
}
