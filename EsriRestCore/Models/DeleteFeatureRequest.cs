namespace EsriRestLibrary.Core.Models
{
    internal class DeleteFeatureRequest
    {
        public DeleteFeatureRequest()
        {
            objectIds = "";
        }

        public string objectIds { get; set; }
        public string where { get; set; }
        public string f { get; set; }
    }
}