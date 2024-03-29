﻿using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions; // IConfiguration sayesinde okuduğun değerleri bir nesneye atacağım ki kullanabileyim
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration) // Sen beni enjekte et ben WebAPI'nin konfigurasyon nesnesini enjekte ederim diyor
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims) // Bana User bilgisini ve Claimleri ver ben ona göre bir Token oluşturayım
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);


            /* var securityKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey)); yerine aşağıdakini yazıyorum çünkü SecurityKey'i her seferinde böyle yaratmak yerine bir kere "Encryption" klasörüne Yazıp sürekli oradan çekiyorum. */
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey); // SecurityKey'e ihtiyacını giderdim
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey); // Algoritma'ya ve Hangi anahtarı kullanacağı ihtiyacını giderdim
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims); // TokenOption'ları kullanarak, İlgili User için, İlgili Credentialleri kullanarak, Bu User'a atanacak Claimleri içeren bir tane method yazdık
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler(); //Bunu nesneyi kullanarak yarattığım elimdeki token nesnesini string'e çevirdik "WriteToken" ile
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken( // JwySecurity Token oluşturmaya arıyor
                issuer: tokenOptions.Issuer, // Issuer bilgisini
                audience: tokenOptions.Audience, // Audience Bilgisini
                expires: _accessTokenExpiration, // Expire Bilgisini
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims), //Claim bilgilerini
                signingCredentials: signingCredentials // SigningCredential Bilgilerini
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims) // Claimleri set edebileceğimiz operasyon
        {

            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString()); // "AddNameIdentifier" parametresi string olduğu için user.Id.ToString()
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray()); //"AddRoles" parametresi Array olduğu için operationClaims.Select(c => c.Name).ToArray()

            return claims;
        }


    }
}
