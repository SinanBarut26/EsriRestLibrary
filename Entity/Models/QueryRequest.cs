namespace Entity.Models
{
    public class QueryRequest
    {
        public QueryRequest()
        {
            returnGeometry = "true";
            f = "json";
            spatialRel = "esriSpatialRelIntersects";
            geometryType = "esriGeometryEnvelope";
            outFields = "*";
            //resultOffset = 1;
            //resultRecordCount = 1000;
            returnCountOnly = false;
        }

        public string f { get; set; }
        public string text { get; set; }
        public string geometry { get; set; }
        public string geometryType { get; set; }
        public string inSR { get; set; }
        public string outSR { get; set; }
        public string spatialRel { get; set; }
        public string where { get; set; }
        public string outFields { get; set; }
        public string returnGeometry { get; set; }

        public string orderByFields { get; set; }

        //public int resultOffset { get; set; }
        //public int resultRecordCount { get; set; }

        public bool returnCountOnly { get; set; }
    }
}