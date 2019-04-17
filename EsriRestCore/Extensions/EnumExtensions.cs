using System;
using System.ComponentModel;
using System.Linq;

namespace EsriRestLibrary.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            // get attributes  
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            // return description
            return attributes.Any()
                ? ((DescriptionAttribute)attributes.ElementAt(0)).Description
                : "Description Not Found";
        }

        public static string ReplaceCoordinateForCommaToDot(this string coordinateWithComma)
        {
            return coordinateWithComma.Replace(",", ".");
        }
    }
}
