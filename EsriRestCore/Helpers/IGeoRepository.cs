using EsriRestLibrary.Core.Models;
using System.Collections.Generic;

namespace EsriRestLibrary.Core.Helpers
{
    public interface IGeoRepository<TEntity, TGeometry> where TEntity : class, new() where TGeometry : class, new()
    {
        List<Feature<TEntity, TGeometry>> GetList(string criteria, SpatialReference spatialReference, string orderBy, string sortDirection,
            int pageStart = 0, int pageSize = 10);

        Feature<TEntity, TGeometry> Find(int id, SpatialReference spatialReference);

        Feature<TEntity, TGeometry> Find(decimal id, SpatialReference spatialReference);

        Feature<TEntity, TGeometry> Find(string criteria, SpatialReference spatialReference);

        Feature<TEntity, TGeometry> Add(TEntity entity, TGeometry geometry);

        Feature<TEntity, TGeometry> Edit(TEntity entity, TGeometry geometry);

        void Delete(string criteria);

        void Delete(params int[] ids);

        void Delete(int id);

        void Delete(params decimal[] ids);

        void Delete(decimal id);

        int Count(string criteria);
    }
}