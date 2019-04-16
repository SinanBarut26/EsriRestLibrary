# EsriRestLibrary
This project created for easyly access to Esri Rest service in .Net Core 2

# Usage
There no dependecy injection for now

So you can start to use direct in your code

GeoContext.cs
```C#
public class GeoContext
{
  public string Proxy { get; set; }
  public string Url { get; set; }
  public string Token { get; set; }
}
```
EsriPointDTO.cs //It will use in your controller or bussiness logic layer
```C#
  public class EsriPointDTO
  {
    public decimal OBJECTID { get; set; }
    public string Name { get; set; }
  }
```

appsettings.json
```json
  ..
  ..
  "GeoContext": {
    "Url": "Your Feature service url",
    "Token": "if you use token based request, you can add your token",
    "Proxy": "This is your Esri Proxy url, it could be use but my recommendation is token"
  }
  ..
  ..
```

Startup.cs
```C#
public void ConfigureServices(IServiceCollection services)
{
  ..
  ..
  services.Configure<GeoContext>(Configuration.GetSection("GeoContext"));
  ..
  ..
}

```

Usage
```C#
private readonly PointService<EsriPointDTO> _pointDal;

public SampleController(IOptionsSnapshot<GeoContext> settings){
   _pointDal = new PointService<EsriPointDTO>(geoContext.Url, geoContext.Token);
}

public IActionResult Create(GeoCustomModel model){

 var addedGeo = _pointDal.Add(new EsriPointDTO { Name = model.Name },
                new EsriPoint(entity.PointX.ReplaceCommaAndDotForCoordinate(),
                    entity.PointY.ReplaceCommaAndDotForCoordinate(), new SpatialReference { wkid = 4326 }));
return View();
}
```

It is extension for replace comma to dot
```C#
public static string ReplaceCommaAndDotForCoordinate(this string coordinateWithComma)
{
  return coordinateWithComma.Replace(",", ".");
}
```
