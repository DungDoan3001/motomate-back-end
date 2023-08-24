using Application.Web.Database.Models;

namespace Application.Web.Database.Queries.Interface
{
    public interface IColorQueries
    {
        Task<List<Color>> GetAllColorsAsync();
        Task<bool> CheckIfColorExisted(string color);
    }
}
