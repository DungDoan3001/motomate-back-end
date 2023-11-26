namespace Application.Web.Service.Helpers
{
	public static class Constants
	{
		public static int[] validStatusNumbers = { 0, 1, 2 };

		public static string PENDING = "Pending";

		public static string APPROVED = "Approved";

		public static string DENIED = "Denied";

		public static string CANCELED = "Canceled";

		public static string COMPLETED = "Completed";

		public static string ONGOING = "On Going";

		public static Dictionary<int, string> statusValues = new()
		{
			{ 0, PENDING },
			{ 1, APPROVED },
			{ 2, DENIED }
		};

		public static List<string> AVAILABLE_UPDATE_TRIP_STATUS = new List<string>
		{
			APPROVED, CANCELED, COMPLETED
		};

		public static List<int> ALLOW_RATING_INPUT = new List<int> { 1,2,3,4,5 };
	}
}
