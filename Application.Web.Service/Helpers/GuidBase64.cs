namespace Application.Web.Service.Helpers
{
    public static class GuidBase64
    {
        public static string Base64Encode(Guid guid)
        {
            string encoded = Convert.ToBase64String(guid.ToByteArray());
            encoded = encoded.Replace("/", "_").Replace("+", "-");
            return encoded.Substring(0, 22);
        }

        public static Guid Base64Decode(string value)
        {
            value = value.Replace("_", "/").Replace("-", "+");
            byte[] buffer = Convert.FromBase64String(value + "==");
            return new Guid(buffer);
        }

        public static bool IsBase64(string value)
        {
            value = value.Replace("_", "/").Replace("-", "+");
            Span<byte> buffer = new Span<byte>(new byte[value.Length]);
            return Convert.TryFromBase64String(value, buffer, out int bytesParsed);
        }
    }
}
