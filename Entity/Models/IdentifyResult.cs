using System.Collections.Generic;

namespace Entity.Models
{
    public class IdentifyResult<Geo, Attr>
    {
        public List<Graphic<Geo, Attr>> results { get; set; }
        public Error error { get; set; }
    }
}