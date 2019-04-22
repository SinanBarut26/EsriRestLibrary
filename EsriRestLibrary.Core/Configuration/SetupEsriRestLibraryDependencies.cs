using EsriRestLibrary.Core.Interfaces;
using EsriRestLibrary.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EsriRestLibrary.Core.Configuration
{
    public static class SetupEsriRestLibraryDependencies
    {
        public static IServiceCollection UseEsriRestLibraryDependencies(this IServiceCollection serviceCollection)
        {
            //serviceCollection.AddScoped(typeof(IGeoRepository<,>), typeof(EsriGeoRepository<,>));
            serviceCollection.AddScoped(typeof(IPointService<>), typeof(PointService<>));
            serviceCollection.AddScoped(typeof(IPolyLineService<>), typeof(PolyLineService<>));
            serviceCollection.AddScoped(typeof(IPolygonService<>), typeof(PolygonService<>));

            return serviceCollection;
        }
    }
}