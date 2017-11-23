using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace VPNGET.Common
{
    public partial class Common
    {
        /// <summary>
        /// json 解析到具体对象（强类型，需要指定正确的目标类型）
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="obj">json 字符串</param>
        /// <returns></returns>
        public T DeSerializationJson<T>(String obj) where T : class, new()
        {
            DataContractJsonSerializer sz = new DataContractJsonSerializer(typeof(T));
            Stream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(obj));
            return (T)sz.ReadObject(stream);

        }

        /// <summary>
        /// json 解析无指定正确的类型
        /// </summary>
        /// <param name="obj">数据</param>
        /// <returns></returns>
        public Windows.Data.Json.JsonObject DeSerializationJson(String obj)
        {
            Windows.Data.Json.JsonObject ob = null;
            Windows.Data.Json.JsonObject.TryParse(obj, out ob);
            return ob;

        }

        public string GetVersionDescription()
        {
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{package.DisplayName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}
