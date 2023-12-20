using Application.Web.Database.Models;

namespace Application.Web.Database.Queries.Interface
{
	public interface IColorQueries
	{
		Task<List<Color>> GetAllColorsAsync();
		Task<bool> CheckIfColorExistedByColorNameAsync(string color);
		Task<bool> CheckIfColorExistedByColorIdAsync(Guid colorId);
		Task<Guid> GetColorIdByColorNameAsync(string color);
	}
}
