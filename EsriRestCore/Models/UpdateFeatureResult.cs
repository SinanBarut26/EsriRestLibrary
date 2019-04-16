using System.Collections.Generic;

namespace EsriRestLibrary.Core.Models
{
    internal class UpdateFeatureResult
    {
        public IList<UpdateResult> updateResults { get; set; }
        public Error error { get; set; }
    }
}