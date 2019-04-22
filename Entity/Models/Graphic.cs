namespace Entity.Models
{
    public class Graphic<Geo, Attr>
    {
        public int layerId { get; set; }
        public string layerName { get; set; }
        public string displayFieldName { get; set; }
        public string value { get; set; }
        public Attr attributes { get; set; }
        public string geometryType { get; set; }
        public Geo geometry { get; set; }
    }
}