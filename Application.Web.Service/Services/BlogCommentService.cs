using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Interfaces;

namespace Application.Web.Service.Services
{
	public class BlogCommentService : IBlogCommentService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<BlogComment> _blogCommentRepo;

		private readonly IBlogCommentQueries _blogCommentQueries;

		public BlogCommentService(IUnitOfWork unitOfWork,IBlogCommentQueries blogCommentQueries)
		{
			_unitOfWork = unitOfWork;
			_blogCommentRepo = unitOfWork.GetBaseRepo<BlogComment>();

			_blogCommentQueries = blogCommentQueries;
		}

		public async Task<(IEnumerable<BlogComment>, PaginationMetadata)> GetAllBlogCommentByBlogIdAsync(PaginationRequestModel pagination, Guid blogId)
		{
			var comments = await _blogCommentQueries.GetAllBlogCommentsByBlogId(blogId);

			return Helpers.Helpers.GetPaginationModel<BlogComment>(comments, pagination);
		}
	}
}
