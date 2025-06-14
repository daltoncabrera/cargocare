using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MSESG.CargoCare.Core
{
    public static class CoreUtils
    {
        public static string CleanInput(string strIn, bool toUpper = false)
        {
            var result = String.Join("", strIn.ToCharArray()
                    .Select(chr => Char.IsLetter(chr) || Char.IsDigit(chr)
                        ? chr.ToString()      // valid symbol
                        : "") // numeric code for invalid symbol
            );

            return toUpper ? result.ToUpper() : result;
        }


        public static IEnumerable<T> GetEnumList<T>()
        {
            List<T> result = new List<T>();
            foreach (T item in Enum.GetValues(typeof(T)))
            {
                result.Add(item);
            }

            return result;
        }

        public static IEnumerable<KeyValue> GetEnumKeyValueList<T>()
        {
            var list = GetEnumList<T>();
            return list.Select(s => new KeyValue(s, GetEnumDescription<T>(s)));
        }

        public static string GetEnumDescription<T>(T value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

   
  }
}
