using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Application.Web.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Service.Services
{
	public class MockDataService : IMockDataService
	{
		private readonly ApplicationContext _context;

		private static List<Brand> listBrands = new List<Brand>
												{
													new Brand { Name = "HONDA" },
													new Brand { Name = "YAMAHA" },
													new Brand { Name = "SUZUKI" },
													new Brand { Name = "PIAGGO" },
													new Brand { Name = "SYM" },
													new Brand { Name = "KAWASAKI" },
													new Brand { Name = "DUCATI" },
													new Brand { Name = "HARLEY DAVISION" },
													new Brand { Name = "BENNELLI" },
													new Brand { Name = "KTM" },
												};

		private static Dictionary<string, List<Collection>> listCollections = new Dictionary<string, List<Collection>>
		{
			{ 
				"HONDA", new List<Collection>
				{
					new Collection { Name = "WAVE", BrandId = Guid.Empty },
					new Collection { Name = "WINNER", BrandId = Guid.Empty },
					new Collection { Name = "BLADE", BrandId = Guid.Empty },
					new Collection { Name = "CRF SERIES", BrandId = Guid.Empty }
				}
			},
			{
				"YAMAHA", new List<Collection>
				{
					new Collection { Name = "EXCITER", BrandId = Guid.Empty },
					new Collection { Name = "FZ SERIES", BrandId = Guid.Empty },
					new Collection { Name = "R SERIES", BrandId = Guid.Empty },
					new Collection { Name = "GRANDE", BrandId = Guid.Empty }
				}
			},
			{
				"SUZUKI", new List<Collection>
				{
					new Collection { Name = "RAIDER", BrandId = Guid.Empty },
					new Collection { Name = "GSX SERIES", BrandId = Guid.Empty },
					new Collection { Name = "HAYATE", BrandId = Guid.Empty }
				}
			},
			{
				"PIAGGO", new List<Collection>
				{
					new Collection { Name = "VESPA", BrandId = Guid.Empty }
				}
			},
			{
				"SYM", new List<Collection>
				{
					new Collection { Name = "ATTILA", BrandId = Guid.Empty },
					new Collection { Name = "SHARK", BrandId = Guid.Empty },
					new Collection { Name = "SYMPHONY", BrandId = Guid.Empty }
				}
			},
			{
				"KAWASAKI", new List<Collection>
				{
					new Collection { Name = "Z SERIES", BrandId = Guid.Empty },
					new Collection { Name = "NINJA SERIES", BrandId = Guid.Empty },
					new Collection { Name = "W SERIES", BrandId = Guid.Empty }
				}
			},
			{
				"DUCATI", new List<Collection>
				{
					new Collection { Name = "SCRAMBLER", BrandId = Guid.Empty },
					new Collection { Name = "MONSTER", BrandId = Guid.Empty }
				}
			},
			{
				"HARLEY DAVISION", new List<Collection>
				{
					new Collection { Name = "STREET SERIES", BrandId = Guid.Empty },
					new Collection { Name = "SPORTSTER SERIES", BrandId = Guid.Empty }
				}
			},
			{
				"BENELLI", new List<Collection>
				{
					new Collection { Name = "TNT SERIES", BrandId = Guid.Empty },
					new Collection { Name = "LEONCIO", BrandId = Guid.Empty }
				}
			},
			{
				"KTM", new List<Collection>
				{
					new Collection { Name = "DUKE SERIES", BrandId = Guid.Empty },
					new Collection { Name = "RC SERIES", BrandId = Guid.Empty }
				}
			}
		};

		private class BrandComparer : IEqualityComparer<Brand>
		{
			public bool Equals(Brand x, Brand y)
			{
				return x.Name == y.Name;
			}

			public int GetHashCode(Brand obj)
			{
				return obj.Name.GetHashCode();

			}
		}

		public MockDataService(ApplicationContext context)
		{
			_context = context;
		}

		public async Task<bool> MockDataAsync()
		{
			var brands = await HandleMockBrandsAsync();

			var collections = await HandleMockCollectionsAsync(brands);

			return true;
		}

		private async Task<List<Collection>> HandleMockCollectionsAsync(List<Brand> brands)
		{
			foreach (var brand in brands)
			{
				if (listCollections.Select(c => c.Key).Any(x => x.Equals(brand.Name)))
				{
					var collections = listCollections
						.Where(c => c.Key.Equals(brand.Name))
						.Select(c => c.Value)
						.FirstOrDefault();

					foreach (var collection in collections)
					{
						collection.BrandId = brand.Id;

						var isCollectionCreated = await _context.Collections
							.AnyAsync(c => c.Name
											.ToUpper()
											.Trim()
											.Equals(collection.Name
																.ToUpper()
																.Trim()));

						if (!isCollectionCreated)
						{
							_context.Collections.Add(collection);
						}
					}
				}
			}

			await _context.SaveChangesAsync();

			return await _context.Collections.ToListAsync();
		}

		private async Task<List<Brand>> HandleMockBrandsAsync()
		{
			var currentBrands = await _context.Brands.ToListAsync();

			if (currentBrands != null)
			{
				var notYetCreateBrands = listBrands.Except(currentBrands, new BrandComparer());

				if (notYetCreateBrands.Any())
				{
					await _context.Brands.AddRangeAsync(notYetCreateBrands);

					await _context.SaveChangesAsync();
				}
			}
			else
			{
				await _context.AddRangeAsync(listBrands);

				await _context.SaveChangesAsync();
			}

			return await _context.Brands.ToListAsync();
		}
	}
}
