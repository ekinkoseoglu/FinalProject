﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper // Asp.net'i (WebAPI)'in gelen JWToken ı doğrulaması gerekiyor o yüzden  
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey) // JWT servislerinin WebAPI de kullanılabilmesi için JWToken oluşturulabilmesi için Kimlik bilgilerini "SecurityKey" versiyonunda verececğiz o da bize onun İmzalama nesnesini gönderiyor olacak.
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature); // Bu doğrulama için de hangi anahtarı ve hangi algoritma kullanması gerektiğini söylüyoruz.
        }
    }
}
