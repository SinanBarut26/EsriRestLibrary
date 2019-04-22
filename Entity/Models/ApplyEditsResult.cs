using System.Collections.Generic;

namespace Entity.Models
{
    public class ApplyEditsResult
    {
        public int id { get; set; }
        public IEnumerable<Result> addResults { get; set; }
        public IEnumerable<Result> updateResults { get; set; }
        public IEnumerable<Result> deleteResults { get; set; }
    }
}