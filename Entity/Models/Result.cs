namespace Entity.Models
{
    public class Result
    {
        public int objectId { get; set; }
        public string globalId { get; set; }
        public bool success { get; set; }
        public Error error { get; set; }
    }
}