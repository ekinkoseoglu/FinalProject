using System;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken // Bizim JWT değerimizin ta kendisi
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
