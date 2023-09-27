namespace Application.Web.Service.Helpers
{
	public static class Constants
	{
		public static int[] validStatusNumbers = { 0, 1, 2 };
		public static Dictionary<int, string> statusValues = new()
		{
			{ 0, "Pending" },
			{ 1, "Approved" },
			{ 2, "Denied" }
		};
	}
}
