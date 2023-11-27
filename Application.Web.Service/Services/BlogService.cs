using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using AutoMapper;
using LazyCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Web.Service.Services
{
	public class BlogService : IBlogService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<Blog> _blogRepo;
		private readonly IGenericRepository<Image> _imageRepo;
		private readonly IAppCache _cache;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		private readonly CacheKeyConstants _cacheKeyConstants;
		private readonly IBlogQueries _blogQueries;
		private readonly IBlogCategoryQueries _blogCategoryQueries;

		public BlogService(IMapper mapper, 
						   IAppCache cache, 
						   CacheKeyConstants cacheKeyConstants ,
						   IUnitOfWork unitOfWork, 
						   IBlogQueries blogQueries, 
						   UserManager<User> userManager,
						   IBlogCategoryQueries blogCategoryQueries)
		{
			_unitOfWork = unitOfWork;
			_blogRepo = unitOfWork.GetBaseRepo<Blog>();
			_imageRepo = unitOfWork.GetBaseRepo<Image>();
			_cache = cache;
			_mapper = mapper;
			_userManager = userManager;
			_cacheKeyConstants = cacheKeyConstants;
			_blogQueries = blogQueries;
			_blogCategoryQueries = blogCategoryQueries;
		}

		public async Task<(IEnumerable<Blog>, PaginationMetadata)> GetAllBlogsAsync(PaginationRequestModel pagination)
		{
			var key = $"{_cacheKeyConstants.BlogCacheKey}-All";

			var blogs = await _cache.GetOrAddAsync(
				key,
				async () => await _blogQueries.GetAllBlogsAsync(),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			//var totalItemCount = blogs.Count;

			//var paginationMetadata = new PaginationMetadata(totalItemCount, pagination.pageSize, pagination.pageNumber);

			//var blogsToReturn = blogs
			//	.Skip(pagination.pageSize * (pagination.pageNumber - 1))
			//	.Take(pagination.pageSize)
			//	.ToList();

			return Helpers.Helpers.GetPaginationModel<Blog>(blogs, pagination);
		}

		public async Task<IEnumerable<Blog>> GetRelatedBlogAsync(Guid blogId)
		{
			var key = $"{_cacheKeyConstants.BlogCacheKey}-All";

			var blogs = await _cache.GetOrAddAsync(
				key,
				async () => await _blogQueries.GetAllBlogsAsync(),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			var blog = blogs.FirstOrDefault(x => x.Id.Equals(blogId));

			blogs.Remove(blog);

			var relatedBlogs = blogs.Where(x => x.CategoryId.Equals(blog.CategoryId)).OrderByDescending(x => x.Created_At).Take(10);

			return relatedBlogs;
		}

		public async Task<Blog> GetBlogByIdAsync(Guid blogId)
		{
			var key = $"{_cacheKeyConstants.BlogCacheKey}-{blogId}";

			var blog = await _cache.GetOrAddAsync(
				key,
				async () => await _blogQueries.GetBlogById(blogId),
				TimeSpan.FromHours(_cacheKeyConstants.ExpirationHours));

			_cacheKeyConstants.AddKeyToList(key);

			return blog;
		}

		public async Task<Blog> CreateBlogAsync(BlogRequestModel requestModel)
		{
			var category = await _blogCategoryQueries.GetByIdAsync(requestModel.CategoryId) ?? throw new StatusCodeException(message: "Category not found.", statusCode: StatusCodes.Status404NotFound);
			var user = await _userManager.FindByIdAsync(requestModel.AuthorId.ToString()) ?? throw new StatusCodeException(message: "User not found.", statusCode: StatusCodes.Status404NotFound);
			
			var newBlog = _mapper.Map<Blog>(requestModel);

			var blogImage = new Image
			{
				ImageUrl = requestModel.ImageUrl,
				PublicId = requestModel.PublicId,
			};

			newBlog.ImageId = blogImage.Id;

			_imageRepo.Add(blogImage);
			_blogRepo.Add(newBlog);

			await _unitOfWork.CompleteAsync();

			_unitOfWork.Detach(blogImage);
			_unitOfWork.Detach(newBlog);

			await Task.Run(() =>
			{
				foreach (var key in _cacheKeyConstants.CacheKeyList)
				{
					_cache.Remove(key);
				}

				_cacheKeyConstants.CacheKeyList = new List<string>();
			});

			return await GetBlogByIdAsync(newBlog.Id);
		}

		public async Task<Blog> UpdateBlogAsync(BlogRequestModel requestModel, Guid blogId)
		{
			var category = await _blogCategoryQueries.GetByIdAsync(requestModel.CategoryId) ?? throw new StatusCodeException(message: "Category not found.", statusCode: StatusCodes.Status404NotFound);
			var user = await _userManager.FindByIdAsync(requestModel.AuthorId.ToString()) ?? throw new StatusCodeException(message: "User not found.", statusCode: StatusCodes.Status404NotFound);
			var blog = await _blogQueries.GetBlogById(blogId) ?? throw new StatusCodeException(message: "Blog not found.", statusCode: StatusCodes.Status404NotFound); ;
		
			var blogToUpdate = _mapper.Map<BlogRequestModel, Blog>(requestModel, blog);

			_imageRepo.Delete(blogToUpdate.Image.Id);

			var blogImage = new Image
			{
				ImageUrl = requestModel.ImageUrl,
				PublicId = requestModel.PublicId,
			};

			blogToUpdate.ImageId = blogImage.Id;

			_imageRepo.Add(blogImage);
			_blogRepo.Update(blogToUpdate);

			await _unitOfWork.CompleteAsync();

			_unitOfWork.Detach(blogImage);
			_unitOfWork.Detach(blogToUpdate);

			await Task.Run(() =>
			{
				foreach (var key in _cacheKeyConstants.CacheKeyList)
				{
					_cache.Remove(key);
				}

				_cacheKeyConstants.CacheKeyList = new List<string>();
			});

			return await GetBlogByIdAsync(blogToUpdate.Id);
		}

		public async Task<bool> DeleteBlogAsync(Guid blogId)
		{
			var blog = await _blogRepo.GetById(blogId) ?? throw new StatusCodeException(message: "Blog not found.", statusCode: StatusCodes.Status404NotFound); ;

			_blogRepo.Delete(blogId);

			await _unitOfWork.CompleteAsync();

			await Task.Run(() =>
			{
				foreach (var key in _cacheKeyConstants.CacheKeyList)
				{
					_cache.Remove(key);
				}

				_cacheKeyConstants.CacheKeyList = new List<string>();
			});

			return true;
		}
	}
}
