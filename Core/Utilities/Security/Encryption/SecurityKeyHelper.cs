using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey) // Ben sana string tipinde bir sevuritykey vereyim(appsettings.json da koyduğum isim) sen de bana onun "SecurityKey" karşılığını ver.
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));// string'i byte[] şeklinde göndermemiz gerekiyor.
        }


    }
}
