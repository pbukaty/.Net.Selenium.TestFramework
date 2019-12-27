using System;
using System.IO;
using System.Reflection;

namespace TestFramework.Providers
{
    public static class LocationProvider
    {
        private static readonly string AssemblyLocation;

        public static string GetFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("Value cannot be null or empty.", nameof(fileName));

            return Path.Combine(AssemblyLocation, $@"Pages\{fileName}");
        }

        static LocationProvider()
        {
            var codeBase = Assembly.GetCallingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            AssemblyLocation = Path.GetDirectoryName(path);
        }
    }
}