using System.Collections.Generic;

namespace Entity.Models
{
    public class QueryResult<Geo, Attr>
    {
        public string f { get; set; }

        public int count { get; set; }
        public string displayFieldName { get; set; }
        public Dictionary<string, string> fieldAliases { get; set; }
        public string geometryType { get; set; }
        public SpatialReference spatialReference { get; set; }
        public IList<EsriField> fields { get; set; }
        public IList<EsriFeature<Geo, Attr>> features { get; set; }
        public Error error { get; set; }
    }
}