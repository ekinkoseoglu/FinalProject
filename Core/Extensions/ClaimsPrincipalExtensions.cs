using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions // User'in JWT deki Claimlerine ulaşmak için .Net'de olan "ClaimsPrincipal" class'ını extend edmek için kullanıyoruz. 
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType) // İlgili ClaimType'e göre getiriyor
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal) // Yukarıda extend ettiğimiz Claims methodu kullanarak bu sefer de direkt Roller'i döndürecek method yarat.
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
    }
}
