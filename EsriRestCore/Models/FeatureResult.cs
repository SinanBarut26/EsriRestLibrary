using System.Collections.Generic;

namespace EsriRestLibrary.Core.Models
{
    internal class FeatureResult
    {
        public IList<Result> addResults { get; set; }
        public Error error { get; set; }
    }
}