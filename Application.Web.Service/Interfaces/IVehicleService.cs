using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
    public interface IVehicleService
    {
        Task<(IEnumerable<Vehicle>, PaginationMetadata)> GetVehiclesAsync(PaginationRequestModel pagination);
    }
}
