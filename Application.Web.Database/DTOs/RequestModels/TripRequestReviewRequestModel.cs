namespace Application.Web.Database.DTOs.RequestModels
{
	public class TripRequestReviewRequestModel
	{
		public Guid RequestId { get; set; }

		public Guid UserId { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public int Rating { get; set; }

		public List<TripRequestReviewImageRequest> Images { get; set; }
	}

	public class TripRequestReviewImageRequest
	{
		public string Image { get; set; }
		public string PublicId { get; set; }
	}
}
