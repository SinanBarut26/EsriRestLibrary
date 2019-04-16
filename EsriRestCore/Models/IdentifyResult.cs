using System.Collections.Generic;

namespace EsriRestLibrary.Core.Models
{
    public class IdentifyResult<Geo, Attr>
    {
        public List<Graphic<Geo, Attr>> results { get; set; }
        public Error error { get; set; }
    }
}