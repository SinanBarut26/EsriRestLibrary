using System.Collections.Generic;

namespace Entity.Models
{
    internal class AddFeatureResult
    {
        public IList<AddResult> addResults { get; set; }
        public Error error { get; set; }
    }
}