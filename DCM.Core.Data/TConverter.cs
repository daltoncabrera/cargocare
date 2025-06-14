using System;
using System.ComponentModel;

namespace DCM.Core.Data
{
  public static class TConverter
  {
    public static T ChangeType<T>(object value)
    {
      return (T) TConverter.ChangeType(typeof (T), value);
    }

    public static object ChangeType(Type t, object value)
    {
      return TypeDescriptor.GetConverter(t).ConvertFrom(value);
    }

    public static void RegisterTypeConverter<T, TC>() where TC : TypeConverter
    {
      TypeDescriptor.AddAttributes(typeof (T), new Attribute[1]
      {
        (Attribute) new TypeConverterAttribute(typeof (TC))
      });
    }
  }
}
