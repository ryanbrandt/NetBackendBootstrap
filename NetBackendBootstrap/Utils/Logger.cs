using System;
using System.Globalization;

namespace NetBackendBootstrap.Utils
{
    /// <summary>
    /// Simple logger utility class
    /// </summary>
    public static class Logger
    {
        public static void Log(string source, string message)
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd H:mm:ss.fff", CultureInfo.InvariantCulture)} [{source.ToUpper()}]: {message}");
        }
    }
}
