using System;
using System.Collections.Generic;
using System.Linq;

namespace EsriRestLibrary.Core.Models
{
    public class EsriPolyLine
    {
        public EsriPolyLine(SpatialReference _spatialReference = null)
        {
            if (_spatialReference != null) spatialReference = _spatialReference;
            paths = new List<List<List<string>>>();
            //paths.Add(new List<List<string>>());
        }

        public EsriPolyLine()
        {
        }

        public List<List<List<string>>> paths { get; set; }

        public SpatialReference spatialReference { get; set; } = new SpatialReference
        {
            wkt =
                "PROJCS[\"ITRF_96_UTM_Zone_35N\",GEOGCS[\"GCS_GRS_1980\",DATUM[\"D_ITRF_1996\",SPHEROID[\"GRS_1980\",6378137.0,298.257222101]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",30.0],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0]]"
        };

        public void addPoint(double x, double y)
        {
            var singleRing = new List<string>();
            singleRing.Add(x.ToString());
            singleRing.Add(y.ToString());
            if (!paths.Any()) paths.Add(new List<List<string>>());
            paths.FirstOrDefault().Add(singleRing);
        }

        public void addDynamic(dynamic geo)
        {
            foreach (var item in geo)
            {
                var x = "";
                var y = "";
                if (item.GetType().GetProperty("X") != null)
                {
                    x = item.GetType().GetProperty("X").GetValue(item, null);
                    y = item.GetType().GetProperty("Y").GetValue(item, null);
                }
                else if (item.GetType().GetProperty("x") != null)
                {
                    x = Convert.ToString(item.GetType().GetProperty("x").GetValue(item, null));
                    y = Convert.ToString(item.GetType().GetProperty("y").GetValue(item, null));
                }
                else if (item.GetType() == typeof(List<double>))
                {
                    x = item[0].ToString();
                    y = item[1].ToString();
                }

                if (x != "")
                    addPoint(Convert.ToDouble(x.Replace(".", ",")), Convert.ToDouble(y.Replace(".", ",")));
            }
        }
    }
}