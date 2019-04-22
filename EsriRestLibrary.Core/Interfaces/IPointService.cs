using Entity.Models;

namespace EsriRestLibrary.Core.Interfaces
{
    public interface IPointService<TEntity> : IGeoRepository<TEntity, EsriPoint>
        where TEntity : class, new()
    {
    }
}
