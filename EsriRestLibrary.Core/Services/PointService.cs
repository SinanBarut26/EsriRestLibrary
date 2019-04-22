using Entity.Models;
using EsriRestLibrary.Core.Helpers;
using EsriRestLibrary.Core.Interfaces;

namespace EsriRestLibrary.Core.Services
{
    public class PointService<TEntity> : EsriGeoRepository<TEntity, EsriPoint>, IPointService<TEntity>
        where TEntity : class, new()
    {
    }
}