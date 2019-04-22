namespace EsriRestLibrary.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string ReplaceCoordinateForCommaToDot(this string coordinateWithComma)
        {
            return coordinateWithComma.Replace(",", ".");
        }
    }
}
