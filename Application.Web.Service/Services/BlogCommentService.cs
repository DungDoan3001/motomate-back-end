using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Web.Service.Services
{
	public class BlogCommentService : IBlogCommentService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<BlogComment> _blogCommentRepo;
		private readonly IUserQueries _userQueries;
		private readonly IBlogQueries _blogQueries;
		private readonly IBlogCommentQueries _blogCommentQueries;

		public BlogCommentService(IUnitOfWork unitOfWork, IBlogCommentQueries blogCommentQueries, IBlogQueries blogQueries, IUserQueries userQueries)
		{
			_unitOfWork = unitOfWork;
			_blogCommentRepo = unitOfWork.GetBaseRepo<BlogComment>();

			_userQueries = userQueries;
			_blogQueries = blogQueries;
			_blogCommentQueries = blogCommentQueries;
		}

		public async Task<(IEnumerable<BlogComment>, PaginationMetadata)> GetAllBlogCommentByBlogIdAsync(PaginationRequestModel pagination, Guid blogId)
		{
			var comments = await _blogCommentQueries.GetAllBlogCommentsByBlogId(blogId);

			return Helpers.Helpers.GetPaginationModel<BlogComment>(comments, pagination);
		}

		public async Task<BlogComment> CreateBlogCommentAsync(BlogCommentRequestModel requestModel, Guid blogId)
		{
			await HandleBlogCommentRequestValidation(requestModel, blogId);

			var blogComment = new BlogComment
			{
				UserId = requestModel.UserId,
				Comment = requestModel.Comment,
				BlogId = blogId
			};

			_blogCommentRepo.Add(blogComment);

			await _unitOfWork.CompleteAsync();

			_unitOfWork.Detach(blogComment);

			return await _blogCommentQueries.GetBlogCommentById(blogComment.Id);

		}

		public async Task<BlogComment> UpdateBlogCommentAsync(BlogCommentRequestModel requestModel, Guid blogCommentId)
		{
			await HandleBlogCommentRequestValidation(requestModel);

			var blogComment = await _blogCommentQueries.GetBlogCommentById(blogCommentId) ?? throw new StatusCodeException(message: "Comment not found.", statusCode: StatusCodes.Status404NotFound);

			if (!blogComment.UserId.Equals(requestModel.UserId))
				throw new StatusCodeException(message: "User does not match.", statusCode: StatusCodes.Status409Conflict);

			blogComment.Comment = requestModel.Comment;
			await _unitOfWork.CompleteAsync();
			_unitOfWork.Detach(blogComment);

			return await _blogCommentQueries.GetBlogCommentById(blogCommentId);
		}

		public async Task<bool> DeleteBlogCommentAsync(Guid blogCommentId)
		{
			var blogComment = await _blogCommentRepo.GetById(blogCommentId);

			if (blogComment == null)
				throw new StatusCodeException(message: "Comment not found.", statusCode: StatusCodes.Status404NotFound);

			_blogCommentRepo.Delete(blogComment.Id);

			await _unitOfWork.CompleteAsync();

			return true;
		}

		private async Task HandleBlogCommentRequestValidation(BlogCommentRequestModel requestModel, Guid blogId)
		{
			var isBlogExist = await _blogQueries.CheckIfBlogExist(blogId);
			var isUserExist = await _userQueries.CheckIfUserExisted(requestModel.UserId);

			if (!isBlogExist)
				throw new StatusCodeException(message: "Blog does not exist.", statusCode: StatusCodes.Status404NotFound);

			if (!isUserExist)
				throw new StatusCodeException(message: "User does not exist.", statusCode: StatusCodes.Status404NotFound);
		}

		private async Task HandleBlogCommentRequestValidation(BlogCommentRequestModel requestModel)
		{
			var isUserExist = await _userQueries.CheckIfUserExisted(requestModel.UserId);

			if (!isUserExist)
				throw new StatusCodeException(message: "User does not exist.", statusCode: StatusCodes.Status404NotFound);
		}
	}
}
