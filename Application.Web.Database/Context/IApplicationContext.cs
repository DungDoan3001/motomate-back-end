using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Context
{
    public interface IApplicationContext
    {
		DbSet<Brand> Brands { get; set; }
		DbSet<BrandImage> BrandImages { get; set; }
		DbSet<Collection> Collections { get; set; }
		DbSet<Color> Colors { get; set; }
		DbSet<Image> Images { get; set; }
		DbSet<Model> Models { get; set; }
		DbSet<ModelColor> ModelColors { get; set; }
		DbSet<ResetPassword> ResetPasswords { get; set; }
		DbSet<Vehicle> Vehicles { get; set; }
		DbSet<VehicleImage> VehicleImages { get; set; }
		DbSet<TripRequest> TripRequests { get; set; }
		DbSet<View> Views { get; set; }

		DbSet<T> GetSet<T>() where T : class;
    }
}