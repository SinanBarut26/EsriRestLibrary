using Entity.Models;
using EsriRestLibrary.Core.Helpers;
using EsriRestLibrary.Core.Interfaces;

namespace EsriRestLibrary.Core.Services
{
    public class PolygonService<TEntity> : EsriGeoRepository<TEntity, EsriPolygon>, IPolygonService<TEntity>
        where TEntity : class, new()
    {
      
    }
}