namespace Application.Web.Service.Helpers
{
	public static class GuidBase64
	{
		private const char _Value62Encoding = '+';
		private const char _Value63Encoding = '/';
		private const char _Value62Replacement = '_';
		private const char _Value63Replacement = '$';

		public static string Base64StringEncode(string stringValue)
		{
			var encodedBytes = System.Text.Encoding.UTF8.GetBytes(stringValue);

			return System.Convert.ToBase64String(encodedBytes);
		}

		public static string Base64StringDecode(string originalValue)
		{
			var decodedValue = System.Convert.FromBase64String(originalValue);

			return System.Text.Encoding.UTF8.GetString(decodedValue);
		}

		public static bool IsBase64Format(string encodedValue)
		{
			var base64Text = encodedValue
							.Replace(_Value62Replacement, _Value62Encoding)
							.Replace(_Value63Replacement, _Value63Encoding)
							+ "==";

			Span<byte> buffer = new Span<byte>(new byte[base64Text.Length]);

			return Convert.TryFromBase64String(base64Text, buffer, out int bytesParsed);
		}

		public static string GetEncodedGuid(Guid guid)
		{
			var bytes = guid.ToByteArray();

			var cellValue = Convert.ToBase64String(bytes);

			cellValue = cellValue.Substring(0, 22); // Remove "=" characters added by Base64 encoding for padding

			cellValue = cellValue
						.Replace(_Value62Encoding, _Value62Replacement)
						.Replace(_Value63Encoding, _Value63Replacement);

			return cellValue;
		}

		public static Guid GetDecodedGuid(string encodedGuid)
		{
			var base64Text = encodedGuid
							.Replace(_Value62Replacement, _Value62Encoding)
							.Replace(_Value63Replacement, _Value63Encoding)
							 + "==";

			var bytes = Convert.FromBase64String(base64Text);

			return new Guid(bytes);
		}
	}
}
