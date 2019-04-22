using Entity.Models;
using EsriRestLibrary.Core.Helpers;
using EsriRestLibrary.Core.Interfaces;

namespace EsriRestLibrary.Core.Services
{
    public class PolyLineService<TEntity> : EsriGeoRepository<TEntity, EsriPolyLine>, IPolyLineService<TEntity>
        where TEntity : class, new()
    {
    }
}