namespace CMS_API.Models
{
    public class ResponseModel
    {
        public string errorMessage { get; set; }
        public bool error { get; set; }
        public dynamic results { get; set; }
    }
}