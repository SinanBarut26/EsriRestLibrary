namespace Entity.Models
{
    internal class AddResult
    {
        public int objectId { get; set; }
        public string globalId { get; set; }
        public bool success { get; set; }
        public Error error { get; set; }
    }
}