using System;

namespace DCM.Core.Data
{
  public static class MarhexTypes
  {
    public static bool IsNumeric(Type type)
    {
      if (!type.Equals(typeof (int)) && !type.Equals(typeof (Decimal)) && (!type.Equals(typeof (double)) && !type.Equals(typeof (int?))) && !type.Equals(typeof (Decimal?)))
        return type.Equals(typeof (double?));
      return true;
    }

    public static bool IsBool(Type type)
    {
      return type.Equals(typeof (bool));
    }

    public static bool IsDateTime(Type type)
    {
      return type.Equals(typeof (DateTime));
    }

    public static bool IsInt(Type type)
    {
      return type.Equals(typeof (int));
    }

    public static bool IsDecimal(Type type)
    {
      return type.Equals(typeof (Decimal));
    }

    public static bool IsString(Type type)
    {
      return type.Equals(typeof (string));
    }

    public static string GetComparisonOperator(Compare comparison)
    {
      string empty = string.Empty;
      switch (comparison)
      {
        case Compare.Equal:
          return "=";
        case Compare.GreaterThan:
          return ">";
        case Compare.GreaterThanOrEqual:
          return ">=";
        case Compare.LessThan:
          return "<";
        case Compare.LessThanOrEqual:
          return "<=";
        case Compare.NotEqual:
          return "!=";
        default:
          throw new ArgumentException("Operador invalido");
      }
    }
  }
}
