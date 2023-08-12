using System.Runtime.CompilerServices;

namespace Application.Web.Service.Helpers
{
    public static class Helpers
    {
        public static string GetCallerName([CallerMemberName] string caller = null)
        {
            return caller;
        }
    }
}
