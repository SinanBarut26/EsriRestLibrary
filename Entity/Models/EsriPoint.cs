namespace Entity.Models
{
    public class EsriPoint
    {
        public string type = "point";

        public EsriPoint(string x, string y, SpatialReference spatialReference)
        {
            this.x = x;
            this.y = y;
            this.spatialReference = spatialReference;
        }

        public EsriPoint()
        {
        }

        //  public SpatialReference spatialReference { get; set; }
        public string x { get; set; }

        public string y { get; set; }

        public SpatialReference spatialReference { get; set; }
    }
}