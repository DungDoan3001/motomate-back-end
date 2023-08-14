using System.Runtime.CompilerServices;
using Microsoft.IdentityModel.Tokens;

namespace Application.Web.Service.Helpers
{
    public static class Helpers
    {
        public static string GetCallerName([CallerMemberName] string caller = null)
        {
            return caller;
        }

        public static string ExtractEmailAddress(string email)
        {
            if(email.IsNullOrEmpty())
                return null;
            int index = email.IndexOf("@");
            if (index >= 0)
                return email.Substring(0, index);
            else return null;
        }
    }
}
