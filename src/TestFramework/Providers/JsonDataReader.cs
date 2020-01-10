using System;
using System.IO;
using Newtonsoft.Json;
using TestFramework.Models;

namespace TestFramework.Providers
{
    public class JsonDataReader
    {
        public static Page LoadElements(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("Value cannot be null or empty.", nameof(fileName));

            return Load<Page>(LocationProvider.GetFile(fileName));
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