using System.Collections.Generic;

namespace Entity.Models
{
    internal class DeleteFeatureResult
    {
        public IList<DeleteResult> deleteResults { get; set; }
        public Error error { get; set; }
    }
}