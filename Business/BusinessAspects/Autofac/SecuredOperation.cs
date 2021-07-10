using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Business.BusinessAspects.Autofac
{
    // JWT için
    public class SecuredOperation : MethodInterception // Yetki kontrolü yapacak olan Aspect
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(','); // Bir metni benim belirttiğim karaktere göre ayırıp array'e atıyor
                _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>(); // .Net'in bizim Autofac'le oluturduğumuz kendice "Service" mimarimize ulaş ve hangi Service'yiseçtiysen onun referanslarına ulaşmasına izin ver

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles(); // Kullanıcının rollerini bul 
            foreach (var role in _roles)// Kullanıcının rollerini gez
            {
                if (roleClaims.Contains(role))// Claimlerinin içinde ilgili rol varsa
                {
                    return; // Methodu çalıştırmaya devam et (hata verme)
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
