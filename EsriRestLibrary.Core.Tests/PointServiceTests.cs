using Entity.Models;
using EsriRestLibrary.Core.Interfaces;
using EsriRestLibrary.Core.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EsriRestLibrary.Core.Tests
{
    public class PointEntity
    {
        public int objectid { get; set; }
        public int incidentid { get; set; }
        public int inspectid { get; set; }
        public int siteid { get; set; }
        public string typdamage { get; set; }

    }


    /// <summary>
    /// Esri example service,
    /// It is public service, so we don't need token
    /// It can be change of data because this service is public so people can change anything
    /// This is
    /// https://sampleserver6.arcgisonline.com/arcgis/rest/services/CommercialDamageAssessment/FeatureServer
    /// </summary>
    public class PointServiceTests
    {
        private readonly IPointService<PointEntity> _pointService;

        private readonly ServicesAccess _servicesAccess = new ServicesAccess
        {
            url = "https://sampleserver6.arcgisonline.com/arcgis/rest/services/CommercialDamageAssessment/FeatureServer",
            token = null
        };
        public PointServiceTests()
        {
            _pointService = new PointService<PointEntity>();
        }

        //You can find in your service for your own spatial reference
        private readonly SpatialReference _srNad1983 = new SpatialReference
        {
            wkt = "PROJCS[\"NAD_1983_HARN_StatePlane_Illinois_East_FIPS_1201\",GEOGCS[\"GCS_North_American_1983_HARN\",DATUM[\"D_North_American_1983_HARN\",SPHEROID[\"GRS_1980\",6378137.0,298.257222101]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",984250.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",-88.33333333333333],PARAMETER[\"Scale_Factor\",0.999975],PARAMETER[\"Latitude_Of_Origin\",36.66666666666666],UNIT[\"Foot_US\",0.3048006096012192]]"
        };

        [Fact]
        public void GetList()
        {
            var response = _pointService.GetList(_servicesAccess,0, "1=1", _srNad1983);
            Assert.Equal(7, response.Count());
        }

        [Fact]
        public void GetList_with_criteria()
        {
            var response = _pointService.GetList(_servicesAccess,0, "typdamage = 'Inaccessible'", _srNad1983);
            Assert.NotNull(response);
        }

        [Fact]
        public void Find_with_objectId()
        {
            var response = _pointService.Find(_servicesAccess,0, 561067, _srNad1983);

            Assert.NotNull(response);
        }

        [Fact]
        public void ApplyEdits_Add_Feature()
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
            var response = _pointService.ApplyEdits(_servicesAccess,newFeature);
            Assert.NotNull(response);
        }

        [Fact]
        public void ApplyEdits_Update_Feature()
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
                                objectid = 561074,
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
            var response = _pointService.ApplyEdits(_servicesAccess,newFeature);
            Assert.NotNull(response);
        }

        [Fact]
        public void ApplyEdits_Delete_Feature()
        {
            var newFeature = new List<ApplyEditsFeature<PointEntity, EsriPoint>>
            {
                new ApplyEditsFeature<PointEntity, EsriPoint>
                {
                    deletes = new List<int>
                    {
                        561075
                    }
                }
            };
            var response = _pointService.ApplyEdits(_servicesAccess,newFeature);
            Assert.NotNull(response);
        }
    }
}
