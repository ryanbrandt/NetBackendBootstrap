using System;
using System.ComponentModel;

namespace NetBackendBootstrap.Utils
{
    /// <summary>
    /// Utility class for enum extension methods
    /// </summary>
    public static class EnumExtensions
    {
        public static string ExtractDescription<T>(T en) where T : IComparable, IFormattable, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Argument must be an enum type");
            }
            DescriptionAttribute[] attributes = (DescriptionAttribute[])en
               .GetType()
               .GetField(en.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
