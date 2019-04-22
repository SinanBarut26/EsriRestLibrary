# EsriRestLibrary
This project created for easyly access to Esri Rest service in .Net Core 2

# Usage
EsriPointDTO.cs //It will use in your controller or bussiness logic layer
```C#
  public class PointEntity
  {
    public int objectid { get; set; }
    public int incidentid { get; set; }
    public int inspectid { get; set; }
    public int siteid { get; set; }
    public string typdamage { get; set; }
  }
```

Startup.cs
```C#
public void ConfigureServices(IServiceCollection services)
{
  ..
  ..
  services.UseEsriRestLibraryDependencies();
  ..
  ..
}

```

Example Usage of WebApi
```C#
  public class ValuesController : ControllerBase
  {
    private readonly SpatialReference _srNad1983 = new SpatialReference
    {
      wkt = "PROJCS[\"NAD_1983_HARN_StatePlane_Illinois_East_FIPS_1201\",GEOGCS[\"GCS_North_American_1983_HARN\",DATUM[\"D_North_American_1983_HARN\",SPHEROID[\"GRS_1980\",6378137.0,298.257222101]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",984250.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",-88.33333333333333],PARAMETER[\"Scale_Factor\",0.999975],PARAMETER[\"Latitude_Of_Origin\",36.66666666666666],UNIT[\"Foot_US\",0.3048006096012192]]"
    };

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
  }
```

Note: If your coordinates have comma, you must to change with dot. For example: if your coordinates like x=27,1231 y=54,1312, change it like x=27.1231 y=54.1312. You can use our 'ReplaceCoordinateForCommaToDot' extension.

Usage example
```x.ReplaceCoordinateForCommaToDot()```