using EsriRestLibrary.Core.Helpers;
using EsriRestLibrary.Core.Models;

namespace EsriRestLibrary.Core.Services
{
    public class PointService<TEntity> : EsriGeoRepository<TEntity, EsriPoint>
        where TEntity : class, new()
    {
        public PointService(string serviceUrl, string token = null, string proxyUrl = null) : base(serviceUrl, token,
            proxyUrl)
        {
        }
    }
}