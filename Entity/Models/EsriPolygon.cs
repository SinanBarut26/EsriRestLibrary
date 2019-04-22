using System;
using System.Collections.Generic;
using System.Linq;

namespace Entity.Models
{
    public class EsriPolygon
    {
        public EsriPolygon(SpatialReference _spatialReference = null)
        {
            if (_spatialReference != null) spatialReference = _spatialReference;
            rings = new List<List<List<double>>>();
            rings.Add(new List<List<double>>());
        }

        public EsriPolygon()
        {
        }

        public List<List<List<double>>> rings { get; set; }

        public SpatialReference spatialReference { get; set; } = new SpatialReference
        {
            wkt =
                "PROJCS[\"ITRF_96_UTM_Zone_35N\",GEOGCS[\"GCS_GRS_1980\",DATUM[\"D_ITRF_1996\",SPHEROID[\"GRS_1980\",6378137.0,298.257222101]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",30.0],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0]]"
        };

        public void addPoint(double x, double y)
        {
            var singleRing = new List<double>();
            singleRing.Add(x);
            singleRing.Add(y);
            rings.FirstOrDefault().Add(singleRing);
        }

        public void addDynamic(dynamic geo)
        {
            foreach (var item in geo)
            {
                string x = item.GetType().GetProperty("X").GetValue(item, null);
                string y = item.GetType().GetProperty("Y").GetValue(item, null);
                if (x != null)
                    addPoint(Convert.ToDouble(x.Replace(".", ",")), Convert.ToDouble(y.Replace(".", ",")));
            }
        }
    }
}