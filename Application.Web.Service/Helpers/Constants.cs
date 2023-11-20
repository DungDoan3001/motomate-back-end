namespace Application.Web.Service.Helpers
{
	public static class Constants
	{
		public static int[] validStatusNumbers = { 0, 1, 2 };

		public static string PENDING = "Pending";

		public static string APPROVED = "Approved";

		public static string DENIED = "Denied";

		public static Dictionary<int, string> statusValues = new()
		{
			{ 0, PENDING },
			{ 1, APPROVED },
			{ 2, DENIED }
		};
	}
}
