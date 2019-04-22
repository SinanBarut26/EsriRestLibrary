namespace Entity.Models
{
    public class Feature<TEntity, TGeometry>
    {
        public TEntity Attributes { get; set; }
        public TGeometry Geometry { get; set; }
    }
}