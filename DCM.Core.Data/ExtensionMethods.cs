using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCM.Core.Data
{
  public static class ExtensionMethods
  {
    public static string JoinAsString<T>(this IEnumerable<T> objectList, string separator = ",", string enclouser = "")
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (T obj in objectList)
      {
        stringBuilder.AppendFormat("{1}{0}{1}", (object) obj, (object) enclouser);
        stringBuilder.Append(enclouser);
        stringBuilder.Append(separator);
      }
      if (separator.Length > 0)
        stringBuilder.Remove(stringBuilder.Length - separator.Length, separator.Length);
      return stringBuilder.ToString();
    }

    public static string GetNameStringSplit(this List<ColumnModel> objectList)
    {
      return objectList.Select<ColumnModel, string>((Func<ColumnModel, string>) (s => s.Nombre)).JoinAsString<string>(",", "");
    }

    public static string GetFiltersInList(this List<ColumnFilter> filterList, Compare compare = Compare.And)
    {
      if (compare != Compare.And && compare != Compare.Or)
        throw new ArgumentOutOfRangeException("Solo usar enumerador and y/o Or");
      string str = compare == Compare.And ? "and" : "or";
      StringBuilder stringBuilder = new StringBuilder();
      foreach (ColumnFilter filter in filterList)
      {
        stringBuilder.AppendFormat("({0})", (object) filter.GetAsString());
        stringBuilder.Append(str);
      }
      stringBuilder.Remove(stringBuilder.Length - str.Length, str.Length);
      return stringBuilder.ToString();
    }
  }
}
