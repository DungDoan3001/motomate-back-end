namespace Application.Web.Database.DTOs.ResponseModels
{
    public class ErrorResponseModel
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }
    }
}
