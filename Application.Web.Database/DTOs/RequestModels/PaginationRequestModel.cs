namespace Application.Web.Database.DTOs.RequestModels
{
    public class PaginationRequestModel
    {
        public int pageNumber { get; set; } =  1;
        public int pageSize { get; set; } = 10;
    }
}
