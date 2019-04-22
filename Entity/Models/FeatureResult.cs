using System.Collections.Generic;

namespace Entity.Models
{
    internal class FeatureResult
    {
        public FeatureResult()
        {
            addResults = new List<Result>();
            updateResults = new List<Result>();
            deleteResults = new List<Result>();
        }

        public int id { get; set; }
        public IEnumerable<Result> addResults { get; set; }
        public IEnumerable<Result> updateResults { get; set; }
        public IEnumerable<Result> deleteResults { get; set; }
        public Error error { get; set; }
    }
}