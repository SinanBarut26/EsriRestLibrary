﻿namespace EsriRestLibrary.Core.Models
{
    public class EsriPoint
    {
        public string type = "point";

        public EsriPoint(string _x, string _y, SpatialReference _spatialReference = null)
        {
            x = _x;
            y = _y;
            if (_spatialReference != null)
                spatialReference = _spatialReference;
        }

        public EsriPoint()
        {
        }

        //  public SpatialReference spatialReference { get; set; }
        public string x { get; set; }

        public string y { get; set; }

        public SpatialReference spatialReference { get; set; } = new SpatialReference
        {
            wkt =
                "PROJCS[\"ITRF_96_UTM_Zone_35N\",GEOGCS[\"GCS_GRS_1980\",DATUM[\"D_ITRF_1996\",SPHEROID[\"GRS_1980\",6378137.0,298.257222101]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",30.0],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0]]"
        };
    }
}