using EsriRestLibrary.Core.Enums;
using EsriRestLibrary.Core.Models;
using EsriRestLibrary.Core.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EsriRestLibrary.Core.Helpers
{
    public abstract class EsriGeoRepository<TEntity, TGeometry> : IGeoRepository<TEntity, TGeometry>
        where TEntity : class, new()
        where TGeometry : class, new()
    {
        //ITRF96
        private readonly string _url;
        private readonly string _token;

        public EsriGeoRepository(string serviceUrl, string token = null, string proxyUrl = null)
        {
            _url = proxyUrl == null ? serviceUrl : proxyUrl + "?" + serviceUrl;
            _token = token;
        }

        public virtual Feature<TEntity, TGeometry> Add(TEntity entity, TGeometry geometry)
        {
            var features = new List<Feature<TEntity, TGeometry>>
            {
                new Feature<TEntity, TGeometry> {Attributes = entity, Geometry = geometry}
            };

            if (_token != null)
            {
                var request = new FeatureRequest
                {
                    f = "pjson",
                    adds = JsonConvert.SerializeObject(features)
                };
                var response = GeometryManager.FeatureTask(_url, request, ApplyEditsTypes.Add, _token);
                dynamic geo = geometry;
                return Find("OBJECTID=" + response.objectId, geo.spatialReference);
            }
            else
            {
                var request = new AddFeatureRequest
                {
                    f = "json",
                    features = JsonConvert.SerializeObject(features)
                };
                var response = GeometryManager.AddFeatureTask(_url, request);
                dynamic geo = geometry;
                return Find("OBJECTID=" + response.addResults.FirstOrDefault()?.objectId, geo.spatialReference);
            }


        }

        public virtual int Count(string criteria)
        {
            var request = new QueryRequest
            {
                @where = criteria,
                returnCountOnly = true
            };
            var task = new EsriQueryTask<TGeometry, TEntity>(_url, _token);
            var response = task.Execute(request);
            return response.count;
        }

        public virtual void Delete(int id)
        {
            Delete(id.ToString());
        }

        public virtual void Delete(params int[] ids)
        {
            foreach (var id in ids) Delete(id.ToString());
        }

        public virtual void Delete(params decimal[] ids)
        {
            foreach (var id in ids) Delete(id.ToString(CultureInfo.InvariantCulture));
        }

        public virtual void Delete(decimal id)
        {
            Delete(id.ToString(CultureInfo.InvariantCulture));
        }

        public virtual void Delete(string criteria)
        {

            if (_token != null)
            {
                var request = new FeatureRequest
                {
                    f = "json",
                    deletes = criteria
                };
                GeometryManager.FeatureTask(_url, request, ApplyEditsTypes.Delete, _token);
            }
            else
            {
                var request = new DeleteFeatureRequest
                {
                    f = "json",
                    objectIds = criteria
                };
                GeometryManager.DeleteFeatureTask(_url, request);
            }
        }

        public virtual Feature<TEntity, TGeometry> Edit(TEntity entity, TGeometry geometry)
        {
            var features = new List<Feature<TEntity, TGeometry>>
            {
                new Feature<TEntity, TGeometry> {Attributes = entity, Geometry = geometry}
            };


            if (_token != null)
            {
                var request = new FeatureRequest
                {
                    f = "pjson",
                    updates = JsonConvert.SerializeObject(features)
                };
                var response = GeometryManager.FeatureTask(_url, request, ApplyEditsTypes.Update, _token);
                dynamic geo = geometry;
                return Find($"OBJECTID={response.objectId}", geo.spatialReference);
            }
            else
            {
                var request = new UpdateFeatureRequest
                {
                    f = "json",
                    features = JsonConvert.SerializeObject(features)
                };
                var response = GeometryManager.UpdateFeatureTask(_url, request);
                dynamic geo = geometry;
                return Find($"OBJECTID={response.updateResults.FirstOrDefault()?.objectId}", geo.spatialReference);
            }

        }

        public virtual Feature<TEntity, TGeometry> Find(string criteria, SpatialReference spatialReference)
        {
            var feature = new Feature<TEntity, TGeometry>();
            var request = new QueryRequest
            {
                @where = criteria,
                //resultOffset = 0,
                //resultRecordCount = 1,
                outFields = "*",
                inSR = JsonConvert.SerializeObject(spatialReference),
                outSR = JsonConvert.SerializeObject(spatialReference)
            };
            var response = new EsriQueryTask<TGeometry, TEntity>(_url, _token);
            var exe = response.Execute(request);

            if (exe.features == null) return feature;

            dynamic geometry = exe.features.SingleOrDefault()?.geometry;
            if (geometry == null) return feature;
            geometry.spatialReference = exe.spatialReference;

            feature.Attributes = exe.features.SingleOrDefault()?.attributes;
            feature.Geometry = geometry;

            return feature;
        }

        public virtual Feature<TEntity, TGeometry> Find(int id, SpatialReference spatialReference = null)
        {
            return Find($"OBJECTID={id}", spatialReference);
        }

        public Feature<TEntity, TGeometry> Find(decimal id, SpatialReference spatialReference = null)
        {
            return Find($"OBJECTID={id}", spatialReference);
        }

        public virtual List<Feature<TEntity, TGeometry>> GetList(string criteria, SpatialReference spatialReference, string orderBy, string sortDirection,
            int pageStart = 0, int pageSize = 10)
        {
            var featureList = new List<Feature<TEntity, TGeometry>>();
            var request = new QueryRequest
            {
                @where = criteria,
                //resultOffset = pageStart,
                //resultRecordCount = pageSize,
                orderByFields = orderBy + " " + sortDirection,
                outFields = "*",
                inSR = JsonConvert.SerializeObject(spatialReference),
                outSR = JsonConvert.SerializeObject(spatialReference)
            };
            var response = new EsriQueryTask<TGeometry, TEntity>(_url, _token).Execute(request);

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

        public virtual List<Feature<TEntity, TGeometry>> GetList(string criteria, SpatialReference spatialReference)
        {
            var featureList = new List<Feature<TEntity, TGeometry>>();
            var request = new QueryRequest
            {
                @where = criteria,
                //resultOffset = 0,
                //resultRecordCount = pageSize,
                //orderByFields = orderBy + " " + sortDirection,
                outFields = "*",
                inSR = JsonConvert.SerializeObject(spatialReference),
                outSR = JsonConvert.SerializeObject(spatialReference)
            };
            var response = new EsriQueryTask<TGeometry, TEntity>(_url, _token).Execute(request);
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