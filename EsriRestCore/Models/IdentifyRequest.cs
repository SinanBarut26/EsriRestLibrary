namespace EsriRestLibrary.Core.Models
{
    public class IdentifyRequest
    {
        public IdentifyRequest()
        {
            geometryType = "esriGeometryPolygon";
            sr = "";
            layers = "4";
            layerDefs = "";
            time = "";
            layerTimeOptions = "";
            tolerance = "0";
            imageDisplay = "600;550;96";
            returnGeometry = "true";
            maxAllowableOffset = "";
            geometryPrecision = "";
            dynamicLayers = "";
            returnZ = "false";
            returnM = "false";
            gdbVersion = "";
            f = "json";
        }

        public string geometry { get; set; }
        public string geometryType { get; set; }
        public string sr { get; set; }
        public string layers { get; set; }
        public string layerDefs { get; set; }
        public string time { get; set; }
        public string layerTimeOptions { get; set; }
        public string tolerance { get; set; }
        public string mapExtent { get; set; }
        public string imageDisplay { get; set; }
        public string returnGeometry { get; set; }
        public string maxAllowableOffset { get; set; }
        public string geometryPrecision { get; set; }
        public string dynamicLayers { get; set; }
        public string returnZ { get; set; }
        public string returnM { get; set; }
        public string gdbVersion { get; set; }
        public string f { get; set; }
    }
}