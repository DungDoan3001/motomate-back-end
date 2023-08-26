using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
    public interface IColorService
    {
        Task<List<Color>> GetColorsAsync();
        Task<Color> GetColorByIdAsync(Guid colorId);
        Task<Color> CreateColorAsync(ColorRequestModel requestModel);
        Task<Color> UpdateColorAsync(ColorRequestModel requestModel, Guid colorId);
        Task<bool> DeleteColorAsync(Guid colorId);
        Task<(IEnumerable<Color>, IEnumerable<string>, IEnumerable<string>)> CreateBulkColorsAsync(List<ColorRequestModel> requestModels);
    }
}
