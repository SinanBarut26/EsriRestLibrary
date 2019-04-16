using System.Collections.Generic;
using EsriRestLibrary.Core.Models;

namespace EsriRestLibrary.Core.Helpers
{
    public interface IGeoRepository<TEntity, TGeometry> where TEntity : class, new() where TGeometry : class, new()
    {
        List<Feature<TEntity, TGeometry>> GetList(string criteria, string orderBy, string sortDirection,
            int pageStart = 0, int pageSize = 10, SpatialReference spatialReference = null);

        Feature<TEntity, TGeometry> Find(int id, SpatialReference spatialReference = null);

        Feature<TEntity, TGeometry> Find(decimal id, SpatialReference spatialReference = null);

        Feature<TEntity, TGeometry> Find(string criteria, SpatialReference spatialReference = null);

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