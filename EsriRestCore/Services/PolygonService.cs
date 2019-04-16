using EsriRestLibrary.Core.Helpers;
using EsriRestLibrary.Core.Models;

namespace EsriRestLibrary.Core.Services
{
    public class PolygonService<TEntity> : EsriGeoRepository<TEntity, EsriPolygon>
        where TEntity : class, new()
    {
        public PolygonService(string serviceUrl, string proxyUrl = null) : base(serviceUrl, proxyUrl)
        {
        }
    }
}