using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using AutoMapper;
using LazyCache;
using Microsoft.AspNetCore.Http;

namespace Application.Web.Service.Services
{
	public class BlogCategoryService : IBlogCategoryService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<BlogCategory> _blogCategoryRepo;
		private readonly CacheKeyConstants _cacheKeyConstants;
		private readonly IAppCache _cache;
		private readonly IMapper _mapper;

		public BlogCategoryService(IMapper mapper, IAppCache cache, IUnitOfWork unitOfWork, CacheKeyConstants cacheKeyConstants)
		{
			_unitOfWork = unitOfWork;
			_blogCategoryRepo = unitOfWork.GetBaseRepo<BlogCategory>();
			_cacheKeyConstants = cacheKeyConstants;
			_cache = cache;
			_mapper = mapper;
		}
			
		public async Task<IEnumerable<BlogCategory>> GetAllCategoryAsync()
		{
			var key = $"{_cacheKeyConstants.BlogCategoryCacheKey}-All";

			var categories = await _cache.GetOrAddAsync(
				key,
				async () => await _blogCategoryRepo.All(),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			return categories;
		}

		public async Task<BlogCategory> CreateBlogCategoryAsync(BlogCategoryRequestModel requestModel)
		{
			var category = await _blogCategoryRepo.FindOne(x => x.Name
																.Trim()
																.ToUpper()
																.Equals(requestModel.Name
																					.Trim()
																					.ToUpper()));
			if(category != null)
			{
				throw new StatusCodeException(message: "Name already taken.", statusCode: StatusCodes.Status409Conflict);
			}

			var newCategory = _mapper.Map<BlogCategory>(requestModel);

			_blogCategoryRepo.Add(newCategory);

			await _unitOfWork.CompleteAsync();

			_unitOfWork.Detach(newCategory);

			await Task.Run(() =>
			{
				foreach (var key in _cacheKeyConstants.CacheKeyList)
				{
					_cache.Remove(key);
				}

				_cacheKeyConstants.CacheKeyList = new List<string>();
			});

			return newCategory;
		}
	}
}
