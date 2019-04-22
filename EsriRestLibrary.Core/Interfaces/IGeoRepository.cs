using Entity.Models;
using System.Collections.Generic;

namespace EsriRestLibrary.Core.Interfaces
{
    public interface IGeoRepository<TEntity, TGeometry> where TEntity : class, new() where TGeometry : class, new()
    {
        IEnumerable<Feature<TEntity, TGeometry>> GetList(ServicesAccess servicesAccess, int layerId, string criteria, SpatialReference spatialReference, string orderBy, string sortDirection,
            int pageStart = 0, int pageSize = 10);

        IEnumerable<Feature<TEntity, TGeometry>> GetList(ServicesAccess servicesAccess, int layerId, string criteria, SpatialReference spatialReference);

        Feature<TEntity, TGeometry> Find(ServicesAccess servicesAccess, int layerId, int objectId, SpatialReference spatialReference);

        Feature<TEntity, TGeometry> Find(ServicesAccess servicesAccess, int layerId, decimal objectId, SpatialReference spatialReference);

        Feature<TEntity, TGeometry> Find(ServicesAccess servicesAccess, int layerId, string criteria, SpatialReference spatialReference);

        IEnumerable<ApplyEditsResult> ApplyEdits(ServicesAccess servicesAccess, IEnumerable<ApplyEditsFeature<TEntity, TGeometry>> features);

        int Count(ServicesAccess servicesAccess, string criteria);
    }
}