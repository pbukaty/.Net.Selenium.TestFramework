using System;
using System.IO;
using Flexecash.Authorization.Framework.Models;
using Newtonsoft.Json;

namespace Flexecash.Authorization.Framework.Providers
{
    public class JsonDataReader
    {
        public static RequestParameters LoadRequestParameters(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("Value cannot be null or empty.", nameof(fileName));

            return Load<RequestParameters>(LocationProvider.GetFile(fileName));
        }

        private static T Load<T>(string jsonLocation)
        {
            if (string.IsNullOrEmpty(jsonLocation)) throw new ArgumentException("Value cannot be null or empty.", nameof(jsonLocation));

            string json;
            using (var reader = new StreamReader(jsonLocation))
            {
                json = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}