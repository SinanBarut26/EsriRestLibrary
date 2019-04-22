using System.Collections.Generic;

namespace Entity.Models
{
    public class ApplyEditsFeature<TEntity, TGeometry>
    {
        public int  id { get; set; }
        public IEnumerable<Feature<TEntity, TGeometry>> adds { get; set; }
        public IEnumerable<Feature<TEntity, TGeometry>> updates { get; set; }
        public IEnumerable<int> deletes { get; set; }
    }
}