using EsriRestLibrary.Core.Helpers;
using EsriRestLibrary.Core.Models;

namespace EsriRestLibrary.Core.Services
{
    public class PolyLineService<TEntity> : EsriGeoRepository<TEntity, EsriPolyLine>
        where TEntity : class, new()
    {
        public PolyLineService(string serviceUrl, string proxyUrl = null) : base(serviceUrl, proxyUrl)
        {
        }
    }
}