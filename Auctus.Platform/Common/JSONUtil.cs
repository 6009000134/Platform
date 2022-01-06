using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;

namespace MyPlatform.Common
{
    /// <summary>
    /// JSON/对象帮助类
    /// </summary>
    public static class JSONUtil
    {
        public static T ParseFromJson<T>(string json)
        {
            T result = Activator.CreateInstance<T>();
            result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            return result;
        }

        public static string GetJson<T>(T obj)
        {
            IsoDateTimeConverter isoDateTimeConverter = new IsoDateTimeConverter();
            isoDateTimeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonConverter[]
            {
                isoDateTimeConverter
            });
        }

        public static string GetJson(DataTable obj)
        {
            IsoDateTimeConverter isoDateTimeConverter = new IsoDateTimeConverter();
            isoDateTimeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonConverter[]
            {
                isoDateTimeConverter
            });
        }


    }
}
