using Entity.Models;
using EsriRestLibrary.Core.ExamplesApi.Models;
using EsriRestLibrary.Core.Interfaces;
using EsriRestLibrary.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace EsriRestLibrary.Core.ExamplesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //private readonly IPointService<PointEntity> _pointService;

        private readonly SpatialReference _srNad1983 = new SpatialReference
        {
            wkt = "PROJCS[\"NAD_1983_HARN_StatePlane_Illinois_East_FIPS_1201\",GEOGCS[\"GCS_North_American_1983_HARN\",DATUM[\"D_North_American_1983_HARN\",SPHEROID[\"GRS_1980\",6378137.0,298.257222101]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",984250.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",-88.33333333333333],PARAMETER[\"Scale_Factor\",0.999975],PARAMETER[\"Latitude_Of_Origin\",36.66666666666666],UNIT[\"Foot_US\",0.3048006096012192]]"
        };

        //public ValuesController(IPointService<PointEntity> pointService)
        //{
        //    _pointService = pointService;
        //}
        private readonly ServicesAccess _servicesAccess = new ServicesAccess
        {
            url = "https://sampleserver6.arcgisonline.com/arcgis/rest/services/CommercialDamageAssessment/FeatureServer",
            token = null
        };

        private readonly IPointService<PointEntity> _pointService;
        public ValuesController(IPointService<PointEntity> pointService)
        {
            _pointService = new PointService<PointEntity>();
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var response = _pointService.GetList(_servicesAccess, 0, "1=1", _srNad1983);
            // return all objectid
            return response.Select(x => x.Attributes.objectid.ToString()).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            //var response = _pointService.Find(0, 561067, _srNad1983);
            var response = _pointService.Find(_servicesAccess, 0, id, _srNad1983);

            return JsonConvert.SerializeObject(response);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            var newFeature = new List<ApplyEditsFeature<PointEntity, EsriPoint>>
            {
                new ApplyEditsFeature<PointEntity, EsriPoint>
                {
                    adds = new List<Feature<PointEntity, EsriPoint>>
                    {
                        new Feature<PointEntity, EsriPoint>
                        {
                            Attributes = new PointEntity
                            {
                                incidentid = 1,
                                inspectid = 2,
                                siteid = 7,
                                typdamage = "Inaccessible"
                            },
                            Geometry = new EsriPoint
                            {
                                x = "1036556.7442350946",
                                y = "1860503.3023153618",
                                spatialReference = _srNad1983
                            }

                        }
                    }
                }
            };

            var response = _pointService.ApplyEdits(_servicesAccess, newFeature);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            var newFeature = new List<ApplyEditsFeature<PointEntity, EsriPoint>>
            {
                new ApplyEditsFeature<PointEntity, EsriPoint>
                {
                    updates = new List<Feature<PointEntity, EsriPoint>>
                    {
                        new Feature<PointEntity, EsriPoint>
                        {
                            Attributes = new PointEntity
                            {
                                //objectid = 561074,
                                objectid = id,
                                incidentid = 1,
                                inspectid = 2,
                                siteid = 7,
                                typdamage = "Destroyed"
                            },
                            Geometry = new EsriPoint
                            {
                                x = "1036556.7442350946",
                                y = "1860303.3023153618",
                                spatialReference = _srNad1983
                            }

                        }
                    }
                }
            };
            var response = _pointService.ApplyEdits(_servicesAccess, newFeature);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var newFeature = new List<ApplyEditsFeature<PointEntity, EsriPoint>>
            {
                new ApplyEditsFeature<PointEntity, EsriPoint>
                {
                    deletes = new List<int>
                    {
                        //561075,
                        id
                    }
                }
            };
            var response = _pointService.ApplyEdits(_servicesAccess, newFeature);
        }
    }
}
