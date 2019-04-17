using System.Collections.Generic;

namespace EsriRestLibrary.Core.Models
{
    internal class ApplyEditsResult
    {
        public IEnumerable<Result> addResults { get; set; }
        public IEnumerable<Result> updateResults { get; set; }
        public IEnumerable<Result> deleteResults { get; set; }
        public Error error { get; set; }
    }
}