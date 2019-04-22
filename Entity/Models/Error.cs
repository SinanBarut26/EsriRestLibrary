using System.Collections.Generic;

namespace Entity.Models
{
    public class Error
    {
        public int code { get; set; }
        public string message { get; set; }
        public string description { get; set; }
        public IList<string> details { get; set; }
    }
}