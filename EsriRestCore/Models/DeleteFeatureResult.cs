using System.Collections.Generic;

namespace EsriRestLibrary.Core.Models
{
    internal class DeleteFeatureResult
    {
        public IList<DeleteResult> deleteResults { get; set; }
        public Error error { get; set; }
    }
}