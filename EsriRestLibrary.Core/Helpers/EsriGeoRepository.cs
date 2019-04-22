using System.Collections.Generic;
using System.Linq;
using Entity.Models;
using EsriRestLibrary.Core.Interfaces;
using EsriRestLibrary.Core.Tasks;
using Newtonsoft.Json;

namespace EsriRestLibrary.Core.Helpers
{
    public abstract class EsriGeoRepository<TEntity, TGeometry> : IGeoRepository<TEntity, TGeometry>
        where TEntity : class, new()
        where TGeometry : class, new()
    {
        public virtual IEnumerable<ApplyEditsResult> ApplyEdits(ServicesAccess servicesAccess,
            IEnumerable<ApplyEditsFeature<TEntity, TGeometry>> features)
        {
            var request = new FeatureRequest
            {
                f = "pjson",
                edits = JsonConvert.SerializeObject(features)
            };
            var response = GeometryManager.FeatureTask(servicesAccess.url, request, servicesAccess.token);
            return response;
        }

        public virtual int Count(ServicesAccess servicesAccess, string criteria)
        {
            var request = new QueryRequest
            {
                where = criteria,
                returnCountOnly = true
            };
            var task = new EsriQueryTask<TGeometry, TEntity>(servicesAccess.url, servicesAccess.token);
            var response = task.Execute(request);
            return response.count;
        }

        public virtual Feature<TEntity, TGeometry> Find(ServicesAccess servicesAccess, int layerId, string criteria,
            SpatialReference spatialReference)
        {
            var feature = new Feature<TEntity, TGeometry>();
            var request = new QueryRequest
            {
                where = criteria,
                //resultOffset = 0,
                //resultRecordCount = 1,
                outFields = "*",
                inSR = JsonConvert.SerializeObject(spatialReference),
                outSR = JsonConvert.SerializeObject(spatialReference)
            };

            var response =
                new EsriQueryTask<TGeometry, TEntity>($"{servicesAccess.url}/{layerId}", servicesAccess.token);
            var exe = response.Execute(request);

            if (exe.features == null) return feature;

            dynamic geometry = exe.features.SingleOrDefault()?.geometry;
            if (geometry == null) return feature;
            geometry.spatialReference = exe.spatialReference;

            feature.Attributes = exe.features.SingleOrDefault()?.attributes;
            feature.Geometry = geometry;

            return feature;
        }

        public virtual Feature<TEntity, TGeometry> Find(ServicesAccess servicesAccess, int layerId, int objectId,
            SpatialReference spatialReference)
        {
            return Find(servicesAccess, layerId, $"OBJECTID={objectId}", spatialReference);
        }

        public Feature<TEntity, TGeometry> Find(ServicesAccess servicesAccess, int layerId, decimal objectId,
            SpatialReference spatialReference)
        {
            return Find(servicesAccess, layerId, $"OBJECTID={objectId}", spatialReference);
        }

        public virtual IEnumerable<Feature<TEntity, TGeometry>> GetList(ServicesAccess servicesAccess, int layerId,
            string criteria, SpatialReference spatialReference, string orderBy, string sortDirection,
            int pageStart = 0, int pageSize = 10)
        {
            var featureList = new List<Feature<TEntity, TGeometry>>();
            var request = new QueryRequest
            {
                where = criteria,
                //resultOffset = pageStart,
                //resultRecordCount = pageSize,
                orderByFields = orderBy + " " + sortDirection,
                outFields = "*",
                inSR = JsonConvert.SerializeObject(spatialReference),
                outSR = JsonConvert.SerializeObject(spatialReference)
            };
            var response =
                new EsriQueryTask<TGeometry, TEntity>($"{servicesAccess.url}/{layerId}", servicesAccess.token)
                    .Execute(request);

            if (response.features == null) return featureList;

            foreach (var feature in response.features)
            {
                dynamic geometry = feature.geometry;
                geometry.spatialReference = response.spatialReference;

                featureList.Add(new Feature<TEntity, TGeometry>
                {
                    Attributes = feature.attributes,
                    Geometry = geometry
                });
            }

            return featureList;
        }

        public virtual IEnumerable<Feature<TEntity, TGeometry>> GetList(ServicesAccess servicesAccess, int layerId,
            string criteria, SpatialReference spatialReference)
        {
            var featureList = new List<Feature<TEntity, TGeometry>>();
            var request = new QueryRequest
            {
                where = criteria,
                //resultOffset = 0,
                //resultRecordCount = pageSize,
                //orderByFields = orderBy + " " + sortDirection,
                outFields = "*",
                inSR = JsonConvert.SerializeObject(spatialReference),
                outSR = JsonConvert.SerializeObject(spatialReference)
            };
            var response =
                new EsriQueryTask<TGeometry, TEntity>($"{servicesAccess.url}/{layerId}", servicesAccess.token)
                    .Execute(request);
            if (response.features == null) return featureList;

            foreach (var feature in response.features)
            {
                dynamic geometry = feature.geometry;
                geometry.spatialReference = response.spatialReference;

                featureList.Add(new Feature<TEntity, TGeometry>
                {
                    Attributes = feature.attributes,
                    Geometry = geometry
                });
            }

            return featureList;
        }
    }
}