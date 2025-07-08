using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MSESG.CargoCare.Core.EFServices{
    public static class AppSessionExtensions
    {
        public const string? SESSION_KEY_USER_DATA = "S_USER_DATA";
        public static void Set<T>(this ISession session, string? key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string? key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }


    }
}
