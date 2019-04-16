using System.Collections.Generic;

namespace EsriRestLibrary.Core.Models
{
    internal class AddFeatureResult
    {
        public IList<AddResult> addResults { get; set; }
        public Error error { get; set; }
    }
}