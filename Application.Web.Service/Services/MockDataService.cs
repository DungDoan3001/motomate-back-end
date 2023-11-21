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

		private static Dictionary<string, Dictionary<Model, List<Color>>> listModels = new Dictionary<string, Dictionary<Model, List<Color>>>
		{
			{
				"WAVE", new Dictionary<Model, List<Color>>
				{
					{
						new Model {Name = "ALPHA", Year = "2020", Capacity = "2", CollectionId = Guid.Empty },
						new List<Color>
						{
							new Color {Name = "BLACK", HexCode = "#000000"},
							new Color {Name = "RED", HexCode = "#FF0000"},
							new Color {Name = "BLUE", HexCode = "#0000FF"},
							new Color {Name = "WHITE", HexCode = "#FFFFFF"},
						}
					},
					{
						new Model {Name = "ALPHA", Year = "2021", Capacity = "2", CollectionId = Guid.Empty },
						new List<Color>
						{
							new Color {Name = "BLACK", HexCode = "#000000"},
							new Color {Name = "RED", HexCode = "#FF0000"},
							new Color {Name = "BLUE", HexCode = "#0000FF"},
							new Color {Name = "WHITE", HexCode = "#FFFFFF"},
						}
					},
					{
						new Model {Name = "RXS", Year = "2019", Capacity = "2", CollectionId = Guid.Empty },
						new List<Color> 
						{
							new Color {Name = "BLACK", HexCode = "#000000"},
							new Color {Name = "RED", HexCode = "#FF0000"},
							new Color {Name = "BLUE", HexCode = "#0000FF"},
							new Color {Name = "WHITE", HexCode = "#FFFFFF"},
						}
					},
					{
						new Model {Name = "RXS", Year = "2021", Capacity = "2", CollectionId = Guid.Empty },
						new List<Color>
						{
							new Color {Name = "BLACK", HexCode = "#000000"},
							new Color {Name = "RED", HexCode = "#FF0000"},
							new Color {Name = "BLUE", HexCode = "#0000FF"},
							new Color {Name = "WHITE", HexCode = "#FFFFFF"},
						}
					}
				}
			},
			{
				"WINNER", new Dictionary<Model, List<Color>>
				{
					{
						new Model {Name = "X", Year = "2019", Capacity = "2", CollectionId = Guid.Empty },
						new List<Color>
						{
							new Color {Name = "BLACK", HexCode = "#000000"},
							new Color {Name = "SILVER", HexCode = "#C0C0C0"},
							new Color {Name = "WHITE", HexCode = "#FFFFFF"},
						}
					},
					{
						new Model {Name = "X", Year = "2020", Capacity = "2", CollectionId = Guid.Empty },
						new List<Color>
						{
							new Color {Name = "BLACK", HexCode = "#000000"},
							new Color {Name = "SILVER", HexCode = "#C0C0C0"},
							new Color {Name = "WHITE", HexCode = "#FFFFFF"},
						}
					}
				}
			},
			{
				"BLADE", new Dictionary<Model, List<Color>>
				{
					{
						new Model {Name = "BLADE", Year = "2019", Capacity = "2", CollectionId = Guid.Empty },
						new List<Color>
						{
							new Color {Name = "BLACK", HexCode = "#000000"},
							new Color {Name = "RED", HexCode = "#FF0000"},
							new Color {Name = "BLUE", HexCode = "#0000FF"},
							new Color {Name = "WHITE", HexCode = "#FFFFFF"},
						}
					},
					{
						new Model {Name = "BLADE", Year = "2018", Capacity = "2", CollectionId = Guid.Empty },
						new List<Color>
						{
							new Color {Name = "BLACK", HexCode = "#000000"},
							new Color {Name = "RED", HexCode = "#FF0000"},
							new Color {Name = "BLUE", HexCode = "#0000FF"},
							new Color {Name = "WHITE", HexCode = "#FFFFFF"},
						}
					}
				}
			},
			{
				"EXCITER", new Dictionary<Model, List<Color>>
				{
					{
						new Model {Name = "150", Year = "2020", Capacity = "2", CollectionId = Guid.Empty },
						new List<Color>
						{
							new Color {Name = "GREY", HexCode = "#808080"},
							new Color {Name = "GREEN", HexCode = "#008000"},
							new Color {Name = "BLUE", HexCode = "#0000FF"},
							new Color {Name = "SILVER", HexCode = "#C0C0C0"},
						}
					},
					{
						new Model {Name = "155 VVA", Year = "2021", Capacity = "2", CollectionId = Guid.Empty },
						new List<Color>
						{
							new Color {Name = "GREY", HexCode = "#808080"},
							new Color {Name = "GREEN", HexCode = "#008000"},
							new Color {Name = "BLUE", HexCode = "#0000FF"},
							new Color {Name = "SILVER", HexCode = "#C0C0C0"},
						}
					}
				}
			},
			{
				"FZ SERIES", new Dictionary<Model, List<Color>>
				{
					{
						new Model {Name = "FZ16", Year = "2020", Capacity = "2", CollectionId = Guid.Empty },
						new List<Color>
						{
							new Color {Name = "BLACK", HexCode = "#000000"},
							new Color {Name = "RED", HexCode = "#FF0000"},
						}
					}
				}
			},
			{
				"RAIDER", new Dictionary<Model, List<Color>>
				{
					{
						new Model {Name = "150CC", Year = "2020", Capacity = "2", CollectionId = Guid.Empty },
						new List<Color>
						{
							new Color {Name = "GREY", HexCode = "#808080"},
							new Color {Name = "BLUE", HexCode = "#0000FF"},
							new Color {Name = "BLACK", HexCode = "#000000"},
							new Color {Name = "RED", HexCode = "#FF0000"},
						}
					}
				}
			},
			{
				"VESPA", new Dictionary<Model, List<Color>>
				{
					{
						new Model {Name = "SPRINT S", Year = "2019", Capacity = "2", CollectionId = Guid.Empty },
						new List<Color>
						{
							new Color {Name = "YELLOW", HexCode = "#FFFF00"},
							new Color {Name = "SILVER", HexCode = "#C0C0C0"},
							new Color {Name = "BLUE", HexCode = "#0000FF"},
							new Color {Name = "RED", HexCode = "#FF0000"},
						}
					},
					{
						new Model {Name = "SPRINT JUSTIN BIEBER", Year = "2020", Capacity = "2", CollectionId = Guid.Empty },
						new List<Color>
						{
							new Color {Name = "YELLOW", HexCode = "#FFFF00"},
							new Color {Name = "SILVER", HexCode = "#C0C0C0"},
							new Color {Name = "BLUE", HexCode = "#0000FF"},
							new Color {Name = "RED", HexCode = "#FF0000"},
						}
					},
					{
						new Model {Name = "GTS", Year = "2020", Capacity = "2", CollectionId = Guid.Empty },
						new List<Color>
						{
							new Color {Name = "YELLOW", HexCode = "#FFFF00"},
							new Color {Name = "SILVER", HexCode = "#C0C0C0"},
							new Color {Name = "BLUE", HexCode = "#0000FF"},
							new Color {Name = "RED", HexCode = "#FF0000"},
						}
					}
				}
			},
			{
				"ATTILA", new Dictionary<Model, List<Color>>
				{
					{
						new Model {Name = "50", Year = "2022", Capacity = "2", CollectionId = Guid.Empty },
						new List<Color>
						{
							new Color {Name = "BLACK", HexCode = "#000000"},
							new Color {Name = "RED", HexCode = "#FF0000"},
							new Color {Name = "WHITE", HexCode = "#FFFFFF"},
						}
					}
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

			var models = await HandleMockModelsAsync(collections);

			return true;
		}

		private async Task<List<Model>> HandleMockModelsAsync(List<Collection> collections)
		{
			foreach (var collection in collections)
			{
				var modelDictionary = listModels.Where(x => x.Key.ToUpper().Trim().Equals(collection.Name.ToUpper().Trim())).FirstOrDefault().Value;

				if(modelDictionary != null)
				{
					foreach (var model in modelDictionary)
					{
						var dbModel = await _context.Models.FirstOrDefaultAsync(x => x.Name.ToUpper().Trim().Equals(model.Key.Name.ToUpper().Trim()));

						if (dbModel == null)
						{
							var dbCollection = await _context.Collections.FirstOrDefaultAsync(x => x.Name.ToUpper().Trim().Equals(collection.Name.ToUpper().Trim())); 
							
							model.Key.CollectionId = dbCollection.Id;
							
							_context.Models.Add(model.Key);

							var modelColors = model.Value;

							foreach (var modelColor in modelColors)
							{
								var color = await _context.Colors.FirstOrDefaultAsync(x => x.Name.ToUpper().Trim().Equals(modelColor.Name.ToUpper().Trim()));

								if (color != null)
								{
									var newModelColor = new ModelColor
									{
										ModelId = model.Key.Id,
										ColorId = color.Id,
									};

									_context.ModelColors.Add(newModelColor);
								}
								else
								{
									_context.Colors.Add(modelColor);

									var newModelColor = new ModelColor
									{
										ModelId = model.Key.Id,
										ColorId = modelColor.Id,
									};

									_context.ModelColors.Add(newModelColor);
								}
							}
						}
						else
						{
							var modelColors = model.Value;

							foreach (var modelColor in modelColors)
							{
								var color = await _context.Colors.FirstOrDefaultAsync(x => x.Name.ToUpper().Trim().Equals(modelColor.Name.ToUpper().Trim()));

								if (color != null)
								{
									var isModelColorCreated = await _context.ModelColors.AnyAsync(x => x.ColorId.Equals(color.Id) && x.ModelId.Equals(dbModel.Id));

									if (!isModelColorCreated)
									{
										var newModelColor = new ModelColor
										{
											ModelId = dbModel.Id,
											ColorId = color.Id,
										};

										_context.ModelColors.Add(newModelColor);
									}
								}
								else
								{
									var newModelColor = new ModelColor
									{
										ModelId = dbModel.Id,
										ColorId = modelColor.Id,
									};

									_context.Colors.Add(modelColor);
									_context.ModelColors.Add(newModelColor);
								}
							}
						}
					}

					await _context.SaveChangesAsync();
				}
			}

			return await _context.Models.ToListAsync();
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
