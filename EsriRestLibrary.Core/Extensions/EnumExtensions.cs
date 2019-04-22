namespace EsriRestLibrary.Core.Extensions
{
    public static class Extensions
    {
        public static string ReplaceCoordinateForCommaToDot(this string coordinateWithComma)
        {
            return coordinateWithComma.Replace(",", ".");
        }
    }
}
