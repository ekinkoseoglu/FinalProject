using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey) // Ben sana string tipinde bir securitykey vereyim(appsettings.json da koyduğum isim) sen de bana onun "SecurityKey" karşılığını ver.
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));// string'i byte[] şeklinde göndermemiz gerekiyor.
        }


    }
}
