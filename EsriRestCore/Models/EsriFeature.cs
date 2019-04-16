namespace EsriRestLibrary.Core.Models
{
    public class EsriFeature<geo, attr>
    {
        public geo geometry { get; set; }
        public attr attributes { get; set; }
        public Error error { get; set; }
    }
}